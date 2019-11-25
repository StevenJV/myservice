namespace sjv.artoo
{
    internal class SendNotification : ISendNotification
    {
        public string EmailAddress { get; set; }
        public string Body { get; set; }
    }
}