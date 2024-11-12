using System;
using Domain.Models.Common;
using Domain.Models.Common.ApiResult;
using Domain.Models.Dto.Order;
using Infrastructure.Services.OrderService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project_Graduation.Lip;

namespace Project_Graduation.Controllers;

public class VnPayController : BaseApiController
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
    [HttpPost]
    public IActionResult Payment([FromBody] OrderDto order)
    {
        HttpContext.Session.SetString("Order", JsonConvert.SerializeObject(order));
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
        var deserializedObject = JsonConvert.DeserializeObject<OrderDto>(HttpContext.Session.GetString("Order"));
        if (Request.Query.Count > 0)
        {
            string hashSecret = _configuration["VNPAY:HashSecret"]; //Chuỗi bí mật
            var vnpayData = Request.Query.AsEnumerable();
            PayLib pay = new PayLib();

            //lấy toàn bộ dữ liệu được trả về
            foreach (var s in vnpayData)
            {
                pay.AddResponseData(s.Key, s.Value);
            }

            long orderId = Convert.ToInt64(pay.GetResponseData("vnp_TxnRef")); //mã hóa đơn
            long vnpayTranId = Convert.ToInt64(pay.GetResponseData("vnp_TransactionNo")); //mã giao dịch tại hệ thống VNPAY
            string vnp_ResponseCode = pay.GetResponseData("vnp_ResponseCode"); //response code: 00 - thành công, khác 00 - xem thêm https://sandbox.vnpayment.vn/apis/docs/bang-ma-loi/
            string vnp_SecureHash = Request.Query["vnp_SecureHash"]; //hash của dữ liệu trả về

            bool checkSignature = pay.ValidateSignatureHmacSHA512(vnp_SecureHash, hashSecret); //check chữ ký đúng hay không?

            if (checkSignature)
            {
                if (vnp_ResponseCode == "00")
                {
                    //Thanh toán thành công
                    //ViewBag.Message = "Thanh toán thành công hóa đơn " + orderId + " | Mã giao dịch: " + vnpayTranId;
                    if (deserializedObject != null)
                    {
                        var updatess = await _orderService.CreateOrder(deserializedObject);
                        return Ok(updatess.IsSuccessed
                            ? new ApiSuccessResult<string>("Thêm thành công")
                            : new ApiErrorResult<string>("Có lỗi"));
                    }
                    else
                    {
                        return Ok(new ApiSuccessResult<string>("deserializedObject bị NULL"));
                    }
                }
                else
                {
                    //Thanh toán không thành công. Mã lỗi: vnp_ResponseCode
                    return Ok(new ApiSuccessResult<string>("vnp_ResponseCode"));
                    //ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý hóa đơn " + orderId + " | Mã giao dịch: " + vnpayTranId + " | Mã lỗi: " + vnp_ResponseCode;
                }
            }
            else
            {
                return Ok(new ApiSuccessResult<string>("vnp_ResponseCode"));
                //ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý";
            }
        }

        //return RedirectToAction("CheckoutSuccess", "Paypal");
        return Ok("Lỗi tạo");
    }
}