

namespace LaBenVi_OrderAPI.Models
{
    public class EmailLogger
    {
        private string sender;
        private List<string> list;
        private string subject;

        public EmailLogger()
        {
        }

        public EmailLogger(string sender, List<string> list, string subject)
        {
            this.sender = sender;
            this.list = list;
            this.subject = subject;
        }

        public EmailLogger(string subject, IList<string> to, string body, string from)
        {
            // Validation (moved from constructor to separate validation method)
            ValidateArguments(subject, to, body, from);

            Subject = subject;
            To = to;
            Body = body;
            From = from;
        }

        public int Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string From { get; set; }
        public IList<string> To { get; set; }

        // Navigation property to related entities (if needed)
        
        // Validation method for constructor arguments
        private void ValidateArguments(string subject, IList<string> to, string body, string from)
        {
            if (string.IsNullOrWhiteSpace(subject))
                throw new ArgumentException("Subject cannot be null or empty.", nameof(subject));

            if (to == null || !to.Any())
                throw new ArgumentException("Recipient list cannot be null or empty.", nameof(to));

            if (string.IsNullOrWhiteSpace(body))
                throw new ArgumentException("Body cannot be null or empty.", nameof(body));

            if (string.IsNullOrWhiteSpace(from))
                throw new ArgumentException("Sender email address cannot be null or empty.", nameof(from));
        }
    }

}
