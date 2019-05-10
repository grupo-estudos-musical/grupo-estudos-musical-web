namespace GrupoEstudosMusical.Email.Services.Generic
{
    public interface IEmailService
    {
        void SendEmailMessage(EmailMessage emailMessage);
    }
}
