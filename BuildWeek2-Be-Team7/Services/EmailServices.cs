using BuildWeek2_Be_Team7.DTOs.Email;
using FluentEmail.Core;

namespace BuildWeek2_Be_Team7.Services
{
    public class EmailServices
    {
        private readonly IFluentEmail _email;
        public EmailServices(IFluentEmail email)
        {
            _email = email;
        }

        public async Task<bool> SendEmail(ConfirmVisita model)
        {
            var result = await _email
                .To(model.Email)
                .Subject("Conferma visita")
                .UsingTemplateFromFile("Template/EmailConfirmVisita.cshtml", model)
                .SendAsync();
            return result.Successful;
        }
    }
}
