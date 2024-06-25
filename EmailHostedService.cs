namespace MailChannel
{
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class EmailHostedService : BackgroundService
    {
        private readonly EmailQueue _emailQueue;
        private readonly EmailSender _emailSender;

        public EmailHostedService(EmailQueue emailQueue, EmailSender emailSender)
        {
            _emailQueue = emailQueue ?? throw new ArgumentNullException(nameof(emailQueue));
            _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var email = await _emailQueue.DequeueAsync(stoppingToken);
                    await _emailSender.SendEmailAsync(email);
                }
                catch (OperationCanceledException)
                {
                    // Ignore the exception when the operation is canceled
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    Console.WriteLine($"Error sending email: {ex.Message}");
                }
            }
        }
    }
}
