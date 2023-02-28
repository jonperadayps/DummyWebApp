using Domain.Common;

namespace Application.Notifications;

public class NotificationWrapper<TNotification> : MediatR.INotification where TNotification : INotification
{
    public NotificationWrapper(TNotification notification)
    {
        Notification = notification;
    }

    public TNotification Notification { get; }
}