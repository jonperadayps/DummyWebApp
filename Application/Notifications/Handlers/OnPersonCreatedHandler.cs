using Domain.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Notifications.Handlers;

public class OnPersonCreatedHandler  : INotificationHandler<NotificationWrapper<OnPersonCreated>>
{
    private readonly ILogger<OnPersonCreatedHandler> _logger;
    
    public OnPersonCreatedHandler(ILogger<OnPersonCreatedHandler> logger)
    {
        _logger = logger;
    }
    
    public Task Handle(NotificationWrapper<OnPersonCreated> notificationWrapper, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Person: {Name} recorded", notificationWrapper.Notification.Name);
        
        return Task.CompletedTask;
    }
}