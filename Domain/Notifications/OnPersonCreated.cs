using Domain.Common;

namespace Domain.Notifications;

public class OnPersonCreated : INotification
{
    public OnPersonCreated(string name)
    {
        Name = name;
    }
    
    public string Name { get; }
}