using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Project_Graduation.Lip
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);

            Send(emailMessage);
        }

        public async void SendEmailAsync(Message message)
        {
            var emailMessage = await CreateEmailMessageAsync(message);

            Send(emailMessage);
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
            .highlight {{
                color: #007bff;
                font-weight: bold;
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
                <p>Chúng tôi là <span class='highlight'>Nhà hàng Góc Quê</span>, nơi mang đến cho bạn những trải nghiệm ẩm thực đậm chất truyền thống Việt Nam.</p>
                <p>Chúng tôi nhận được yêu cầu đặt lại mật khẩu của bạn. Vui lòng nhấp vào nút dưới đây để tạo mật khẩu mới:</p>
                <p>
                    <a href='{message.Content}' class='button'>Đặt lại mật khẩu</a>
                </p>
                <p>Nếu bạn không yêu cầu thay đổi mật khẩu này, vui lòng bỏ qua email này. Đội ngũ của chúng tôi luôn sẵn sàng hỗ trợ bạn khi cần thiết.</p>
            </div>
            <div class='footer'>
                <p>Trân trọng,</p>
                <p>Đội ngũ Nhà hàng Góc Quê</p>
                <p>&copy; 2024 Góc Quê. Tất cả quyền được bảo lưu.</p>
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
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = string.Format("<h2 style='color:red;'>{0}</h2>", message.Content) };

            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);

                    client.Send(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception or both.
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
