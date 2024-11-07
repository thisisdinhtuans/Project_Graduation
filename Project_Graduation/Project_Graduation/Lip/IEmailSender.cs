using MailKit.Security;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Project_Graduation.Lip
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
        Task SendEmailAsync(Message message);
    }
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public async void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);

            await SendAsync(emailMessage);
        }

        public async Task SendEmailAsync(Message message)
        {
            var emailMessage = await CreateEmailMessageAsync(message);
            await SendAsync(emailMessage);
        }
        private async Task<MimeMessage> CreateEmailMessageAsync(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("", _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $@"
    <html>
    <head>
        <style>
            body {{
                font-family: Arial, sans-serif;
                color: #333;
                margin: 0;
                padding: 0;
                background-color: #f4f4f4;
            }}
            .container {{
                width: 100%;
                max-width: 600px;
                margin: 0 auto;
                padding: 20px;
                background-color: #ffffff;
                border-radius: 8px;
                box-shadow: 0 0 10px rgba(0,0,0,0.1);
            }}
            .header {{
                text-align: center;
                margin-bottom: 20px;
            }}
            .header h1 {{
                color: #007bff;
                font-size: 24px;
            }}
            .content {{
                margin-bottom: 20px;
            }}
            .content p {{
                font-size: 16px;
                line-height: 1.5;
            }}
            .button {{
                display: inline-block;
                padding: 10px 20px;
                font-size: 16px;
                color: #ffffff;
                background-color: #007bff;
                text-decoration: none;
                border-radius: 5px;
                text-align: center;
            }}
            .footer {{
                text-align: center;
                font-size: 12px;
                color: #888;
            }}
        </style>
    </head>
    <body>
        <div class='container'>
            <div class='header'>
                <h1>Yêu Cầu Đặt Lại Mật Khẩu</h1>
            </div>
            <div class='content'>
                <p>Chào bạn,</p>
                <p>Chúng tôi nhận được yêu cầu đặt lại mật khẩu của bạn. Vui lòng nhấp vào nút dưới đây để tạo mật khẩu mới:</p>
                <p>
                    <a href='{message.Content}' class='button'>Đặt lại mật khẩu</a>
                </p>
                <p>Nếu bạn không yêu cầu thay đổi mật khẩu này, vui lòng bỏ qua email này.</p>
            </div>
            <div class='footer'>
                <p>&copy; 2024 Công Ty Của Bạn. Tất cả quyền được bảo lưu.</p>
            </div>
        </div>
    </body>
    </html>"
            };

            await Task.CompletedTask;

            return emailMessage;
        }
        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("", _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Headers.Add(HeaderId.XMailer, "GocQue");
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = string.Format("<h2 style='color:red;'>{0}</h2>", message.Content) };

            return emailMessage;
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    // Kết nối đến Gmail với cổng 465 và sử dụng SSL
                    await client.ConnectAsync(_emailConfig.SmtpServer, 465, SecureSocketOptions.SslOnConnect);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    // Sử dụng mật khẩu ứng dụng nếu bạn đã bật 2FA
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);

                    // Gửi email
                    await client.SendAsync(mailMessage);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending email: {ex.Message}");
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }


    }
}