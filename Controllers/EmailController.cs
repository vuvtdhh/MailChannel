using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MailChannel.Controllers
{
    public class EmailController : ControllerBase
    {
        private readonly EmailQueue _emailQueue;

        public EmailController(EmailQueue emailQueue)
        {
            _emailQueue = emailQueue;
        }

        public async Task<IActionResult> SendEmail(string email)
        {
            await _emailQueue.EnqueueAsync(email);
            return Ok("Email has been queued.");
        }
    }
}
