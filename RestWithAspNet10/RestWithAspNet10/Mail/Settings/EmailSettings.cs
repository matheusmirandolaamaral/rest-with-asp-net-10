namespace RestWithAspNet10.Mail.Settings
{
    public class EmailSettings
    {
        public string Host { get; set; } = string.Empty;
        public string Port { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string From { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool Ssl { get; set; }

        public MailSettings Properties {  get; set; } = new();
    }
}
