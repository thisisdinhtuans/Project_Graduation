
using Domain.Models.Dto.Order;
using Infrastructure.Services.OrderService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project_Graduation.Lip;
using System.Security.Claims;

namespace Project_Graduation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VnPayController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IOrderService _orderService;

        public VnPayController(IConfiguration configuration, IOrderService orderService)
        {
            _configuration = configuration;
            _orderService = orderService;

        }
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    return Ok();
        //}
        private string ip()
        {
            string ipAddress;
            try
            {
                ipAddress = HttpContext.Request.HttpContext.Request.Headers["HTTP_X_FORWARDED_FOR"].ToString();

                if (string.IsNullOrEmpty(ipAddress) || ipAddress.ToLower() == "unknown")
                    ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            }
            catch (Exception ex)
            {
                ipAddress = "Invalid IP:" + ex.Message;
            }
            return ipAddress;
        }
        //var createdBy = HttpContext.User.Identity.Name;
        [HttpPost]
        public async Task<IActionResult> Payment([FromBody] OrderDto order)
        {
            HttpContext.Session.SetString("Order", JsonConvert.SerializeObject(order));

            // Nếu HttpContext.User.Identity.Name là null, lấy từ order.UserName
            var createdBy = HttpContext.User.Identity.Name ?? order.UserName;
            HttpContext.Session.SetString("CreatedBy", createdBy);

            // Gán UserId, nếu không có thì mặc định là "0"
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0";
            HttpContext.Session.SetString("UserId", userId);

            //HttpContext.Session.SetString("Order", JsonConvert.SerializeObject(order));
            //HttpContext.Session.SetString("CreatedBy", HttpContext.User.Identity.Name);
            //HttpContext.Session.SetString("UserId", HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);


            //await HttpContext.Session.CommitAsync(); // Lưu session

            //HttpContext.Session.SetString("Order", JsonConvert.SerializeObject(order));
            //TempData["Order"] = JsonConvert.SerializeObject(order);
            var request = HttpContext.Request;
            var hostAddress = request.Host.Value;
            string url = _configuration["VNPAY:Url"];
            string returnUrl = request.Scheme + "://" + hostAddress + "/" + _configuration["VNPAY:ReturnUrl"];
            string tmnCode = _configuration["VNPAY:TmnCode"];
            string hashSecret = _configuration["VNPAY:HashSecret"];
            var IpAdress = ip();
            PayLib pay = new PayLib();
            pay.AddRequestData("vnp_Version", "2.1.0"); //Phiên bản api mà merchant kết nối. Phiên bản hiện tại là 2.0.0
            pay.AddRequestData("vnp_Command", "pay"); //Mã API sử dụng, mã cho giao dịch thanh toán là 'pay'
            pay.AddRequestData("vnp_TmnCode", tmnCode); //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
            pay.AddRequestData("vnp_Amount", Convert.ToString(order.PriceTotal * 100 * 0.3)); //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
            pay.AddRequestData("vnp_BankCode", ""); //Mã Ngân hàng thanh toán (tham khảo: https://sandbox.vnpayment.vn/apis/danh-sach-ngan-hang/), có thể để trống, người dùng có thể chọn trên cổng thanh toán VNPAY
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); //ngày thanh toán theo định dạng yyyyMMddHHmmss
            pay.AddRequestData("vnp_CurrCode", "VND"); //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
            pay.AddRequestData("vnp_IpAddr", IpAdress /*Util.GetIpAddress()*/); //Địa chỉ IP của khách hàng thực hiện giao dịch
            pay.AddRequestData("vnp_Locale", "vn"); //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
            pay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang"); //Thông tin mô tả nội dung thanh toán
            pay.AddRequestData("vnp_OrderType", "other"); //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
            pay.AddRequestData("vnp_ReturnUrl", returnUrl); //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
            pay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString()); //mã hóa đơn
            if (string.IsNullOrEmpty(hashSecret))
            {
                throw new ArgumentException("Hash secret cannot be null or empty");
            }
            string paymentUrl = pay.CreateRequestUrlWithHmacSHA512(url, hashSecret);

            return Ok(paymentUrl);
        }
        [HttpGet("PaymentConfirm")]
        public async Task<IActionResult> PaymentConfirm()
        {
            try
            {
                var orderJson = HttpContext.Session.GetString("Order");
                var userName = HttpContext.Session.GetString("CreatedBy");
                var userId = HttpContext.Session.GetString("UserId");

                if (string.IsNullOrEmpty(orderJson))
                {
                    Console.WriteLine("Session is empty or order data is missing.");
                    return Redirect($"https://gocque.vercel.app/payment-result?status=error&message=OrderNotFound");

                }

                var deserializedObject = JsonConvert.DeserializeObject<OrderDto>(orderJson);
                if (deserializedObject == null)
                {
                    return Redirect($"https://gocque.vercel.app/payment-result?status=error&message=DeserializationFailed");
                }
                Console.WriteLine("Order data retrieved from session: " + JsonConvert.SerializeObject(deserializedObject));
                Console.WriteLine($"Session ID in PaymentConfirm: {HttpContext.Session.Id}");

                if (Request.Query.Count > 0)
                {
                    string hashSecret = _configuration["VNPAY:HashSecret"];
                    var vnpayData = Request.Query.AsEnumerable();
                    PayLib pay = new PayLib();

                    foreach (var s in vnpayData)
                    {
                        pay.AddResponseData(s.Key, s.Value);
                    }

                    long vnpayTranId = Convert.ToInt64(pay.GetResponseData("vnp_TransactionNo"));
                    string vnp_ResponseCode = pay.GetResponseData("vnp_ResponseCode");
                    string vnp_SecureHash = Request.Query["vnp_SecureHash"];

                    bool checkSignature = pay.ValidateSignatureHmacSHA512(vnp_SecureHash, hashSecret);

                    if (checkSignature)
                    {
                        if (vnp_ResponseCode == "00")
                        {
                            var updateResult = await _orderService.AddOrder(deserializedObject, userName, userId);

                            if (updateResult.IsSuccessed)
                            {
                                return Redirect($"https://gocque.vercel.app/payment-result?status=success");
                            }
                            return Redirect($"https://gocque.vercel.app/payment-result?status=error&message=OrderCreationFailed");
                        }
                        else
                        {
                            return Redirect($"https://gocque.vercel.app/payment-result?status=failed&error={vnp_ResponseCode}");
                        }
                    }
                    else
                    {
                        return Redirect($"https://gocque.vercel.app/payment-result?status=invalid-signature");
                    }
                }
                return Redirect($"https://gocque.vercel.app/payment-result?status=error&message=NoQueryData");
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in PaymentConfirm: {ex.Message}");
                // You might want to log the full exception details in a production environment
                return Redirect($"https://gocque.vercel.app/payment-result?status=error&message={Uri.EscapeDataString(ex.Message)}");
            }
        }
    }
}
