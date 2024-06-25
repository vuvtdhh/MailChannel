namespace MailChannel
{
    public class EmailSender
    {
        public async Task SendEmailAsync(string email)
        {
            await Task.Delay(500);
            Console.WriteLine($"Email sent to: {email}");
        }
    }
}
