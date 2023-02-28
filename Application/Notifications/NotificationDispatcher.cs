using Domain.Common;
using MediatR;
using INotification = Domain.Common.INotification;

namespace Application.Notifications;

public class NotificationDispatcher: INotificationDispatcher
{
    private readonly IMediator _mediator;

    public NotificationDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Dispatch(INotification notification)
    {
        var mediatorNotification = CreateMediatorNotification(notification);

        await _mediator.Publish(mediatorNotification);
    }

    private static MediatR.INotification CreateMediatorNotification(INotification notification)
    {
        var genericDispatcherType = typeof(NotificationWrapper<>).MakeGenericType(notification.GetType());

        return (MediatR.INotification)Activator.CreateInstance(genericDispatcherType, notification)!;
    }
}