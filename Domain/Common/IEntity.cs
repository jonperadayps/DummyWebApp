namespace Domain.Common;

public interface IEntity
{
    IEnumerable<INotification> Notifications { get; }

    void ClearNotifications();
}