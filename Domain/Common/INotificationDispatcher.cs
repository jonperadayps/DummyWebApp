namespace Domain.Common;

public interface INotificationDispatcher
{
    Task Dispatch(INotification notification);
}