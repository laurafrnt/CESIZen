using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Api.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendConfirmationMail(string receiverMail, string username, string token)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_config["SmtpSettings:SenderName"], _config["SmtpSettings:SenderEmail"]));
            email.To.Add(new MailboxAddress(username, receiverMail));
            email.Subject = "CESIZen - Confirmez votre inscription !";

            var builder = new BodyBuilder();
            string confirmationLink = $"https://localhost:7044/api/auth/verify?token={token}";

            builder.HtmlBody = $@"
                <div style='padding: 20px; text-align: center;'>
                    <h2 style='font-family: The Seasons, sans-serif; color: #10b981;'>Bonjour {username}, bienvenue sur CESIZen, la plateforme pour apprendre à gérer votre stress !</h2>
                    <p style = 'font-family: Arial, sans-serif;'>Merci de nous rejoindre. Pour activer votre compte, cliquez sur le bouton ci-dessous :</p>
                    <a href='{confirmationLink}' style='font-family: Arial, sans-serif; display: inline-block; background-color: #10b981; color: white; padding: 12px 24px; text-decoration: none; border-radius: 5px; margin-top: 20px;'>Activer mon compte</a>
                </div>";

            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            try
            {
                await smtp.ConnectAsync(_config["SmtpSettings:Server"], int.Parse(_config["SmtpSettings:Port"]), SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_config["SmtpSettings:Username"], _config["SmtpSettings:Password"]);
                await smtp.SendAsync(email);
            }
            finally
            {
                await smtp.DisconnectAsync(true);
            }
        }

        public async Task SendMailUsed(string receiverMail, string username)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_config["SmtpSettings:SenderName"], _config["SmtpSettings:SenderEmail"]));
            email.To.Add(new MailboxAddress(username, receiverMail));
            email.Subject = "CESIZen - Tentative d'utilisation de l'adresse email";

            var builder = new BodyBuilder();
            string loginLink = $"https://localhost:7044/api/auth/login";

            builder.HtmlBody = $@"
                <div style='font-family: The Seasons, sans-serif; padding: 20px; text-align: center;'>
                    <h2 style='color: #10b981;'>Bonjour {username}, Une personne essaye de créer un compte avec votre adresse mail.</h2>
                    <p style='font-family: Arial, sans-serif;'>Si ce n'était pas vous, veuillez ignorer cet email. Sinon, vous pouvez vous connecter en cliquant sur le bouton ci-dessous :</p>
                    <a href='{loginLink}' style='font-family: Arial, sans-serif; display: inline-block; background-color: #10b981; color: white; padding: 12px 24px; text-decoration: none; border-radius: 5px; margin-top: 20px;'>Se connecter</a>
                </div>";

            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            try
            {
                await smtp.ConnectAsync(_config["SmtpSettings:Server"], int.Parse(_config["SmtpSettings:Port"]), SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_config["SmtpSettings:Username"], _config["SmtpSettings:Password"]);
                await smtp.SendAsync(email);
            }
            finally
            {
                await smtp.DisconnectAsync(true);
            }
        }
    }
}