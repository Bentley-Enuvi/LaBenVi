namespace LaBenVi_AuthService.Models
{
    public class EmailLogger
    {
        public EmailLogger(string subject, IList<string> to, string body, string from)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(subject))
                throw new ArgumentException("Subject cannot be null or empty.", nameof(subject));

            if (to == null || !to.Any())
                throw new ArgumentException("Recipient list cannot be null or empty.", nameof(to));

            if (string.IsNullOrWhiteSpace(body))
                throw new ArgumentException("Body cannot be null or empty.", nameof(body));

            if (string.IsNullOrWhiteSpace(from))
                throw new ArgumentException("Sender email address cannot be null or empty.", nameof(from));

            Subject = subject;
            To = to;
            Body = body;
            From = from;
        }

        public string Subject { get; }
        public IList<string> To { get; }
        public string Body { get; }
        public string From { get; }
    }

}
