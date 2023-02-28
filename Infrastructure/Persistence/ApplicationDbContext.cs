using System.Reflection;
using Application.Contracts.Infrastructure.Persistence;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly INotificationDispatcher _notificationDispatcher;

    public ApplicationDbContext(DbContextOptions options, INotificationDispatcher notificationDispatcher) : base(options)
    {
        _notificationDispatcher = notificationDispatcher;
    }

    public DbSet<Person> People => Set<Person>();
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await DispatchNotifications();

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(builder);
    }

    private async Task DispatchNotifications()
    {
        var entities = ChangeTracker.Entries<IEntity>()
            .Select(po => po.Entity)
            .Where(po => po.Notifications.Any())
            .ToArray();

        foreach (var entity in entities)
        {
            foreach (var notification in entity.Notifications) await _notificationDispatcher.Dispatch(notification);

            entity.ClearNotifications();
        }
    }
}