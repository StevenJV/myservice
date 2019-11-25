using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;

namespace sjv.artoo
{
    public class ScheduleNotificationConsumer :
        IConsumer<IScheduleNotification>
    {
        Uri _notificationService;

        public async Task Consume(ConsumeContext<IScheduleNotification> context)
        {
            await context.ScheduleSend(_notificationService,
                context.Message.DeliveryTime,
                new SendNotification
                {
                    EmailAddress = context.Message.EmailAddress,
                    Body = context.Message.Body
                });
        }


    }
}
