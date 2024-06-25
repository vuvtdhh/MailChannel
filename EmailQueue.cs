using System.Threading.Channels;

namespace MailChannel
{
    public class EmailQueue
    {
        private readonly Channel<string> _channel;

        public EmailQueue()
        {
            _channel = Channel.CreateUnbounded<string>();
        }

        public async Task EnqueueAsync(string email)
        {
            await _channel.Writer.WriteAsync(email);
        }

        public async Task<string> DequeueAsync(CancellationToken cancellationToken)
        {
            return await _channel.Reader.ReadAsync(cancellationToken);
        }
    }
}
