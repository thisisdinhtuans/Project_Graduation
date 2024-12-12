namespace Project_Graduation.Lip
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
        void SendEmailAsync(Message message);
    }
}
