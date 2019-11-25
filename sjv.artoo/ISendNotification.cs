namespace sjv.artoo
{
    public interface ISendNotification
    {
        string EmailAddress { get; }
        string Body { get; }
    }
}