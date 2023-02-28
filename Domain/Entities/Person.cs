using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities;

public class Person : IEntity
{
    private readonly List<INotification> _notifications = new();

    [Key] public int Id { get; set; }

    public string Name { get; set; }

    [NotMapped] public IEnumerable<INotification> Notifications => _notifications.AsReadOnly();

    public void ClearNotifications()
    {
        _notifications.Clear();
    }

    public void PublishNotification(INotification notification)
    {
        _notifications.Add(notification);
    }
}