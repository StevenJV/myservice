using System;

namespace sjv.artoo
{
    public interface IScheduleNotification
    {
        DateTime DeliveryTime { get; }
        string EmailAddress { get; }
        string Body { get; }
    }
}