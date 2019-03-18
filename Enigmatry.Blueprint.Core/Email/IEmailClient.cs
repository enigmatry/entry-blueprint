namespace Enigmatry.Blueprint.Core.Email
{
    public interface IEmailClient
    {
        void Send(EmailMessage email);
    }
}
