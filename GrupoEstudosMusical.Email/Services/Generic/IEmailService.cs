namespace GrupoEstudosMusical.Email.Services.Generic
{
    public interface IEmailService
    {
        bool SendEmailMessage(EmailMessage emailMessage);
    }
}
