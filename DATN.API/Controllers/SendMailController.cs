using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DATN.Api.MailService;
using DATN.Core.ViewModels.SendMailVM;
using System.Net.Http.Headers;

namespace DATN.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SendMailController : ControllerBase
    {
        private readonly EmailService _emailService;
        public SendMailController(EmailService emailService)
        {
            _emailService = emailService;
        }
        [HttpPost]
        public async Task<IActionResult> SendMail([FromBody] SendMailVM request)
        {
            await _emailService.SendEmailAsync(request);
            return Ok();
        }

    }
}
