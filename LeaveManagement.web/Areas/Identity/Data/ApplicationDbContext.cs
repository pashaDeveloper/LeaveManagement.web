using LeaveManagement.web.Areas.Identity.Data;
using LeaveManagement.web.Data.Attributes;
using LeaveManagement.web.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using LeaveManagement.web.ViewModels;

namespace LeaveManagement.web.Data;

public class IdentityDbContext : IdentityDbContext<Employee>
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
        : base(options)
    {
    }
    public DbSet<LeaveType> LeaveTypes { get; set; }
    public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (entityType.ClrType.GetCustomAttributes(typeof(AuditTableAttribute), true).Length > 0)
            {
                builder.Entity(entityType.Name).Property<DateTime>("InsertTime");
                builder.Entity(entityType.Name).Property<DateTime?>("UpdateTime");
                builder.Entity(entityType.Name).Property<DateTime?>("RemovedTime");
                builder.Entity(entityType.Name).Property<bool>("IsRemoved").HasDefaultValue(false);
            }
        }
        base.OnModelCreating(builder);

    }
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        var modifiedEntires = ChangeTracker.Entries()
           .Where(p => p.State == EntityState.Modified ||
           p.State == EntityState.Added ||
           p.State == EntityState.Deleted
           );
        foreach (var entire in modifiedEntires)
        {
            var findEntity = entire.Context.Model.FindEntityType(entire.Entity.GetType());
            var insertTime = findEntity.FindProperty("InsertTime");
            var updateTime = findEntity.FindProperty("UpdateTime");
            var removedTime = findEntity.FindProperty("RemovedTime");
            var isRemoved = findEntity.FindProperty("IsRemoved");
            if (entire.State == EntityState.Added && insertTime != null)
            {
                entire.Property("InsertTime").CurrentValue = DateTime.Now;
            }
            if (entire.State == EntityState.Modified && updateTime != null)
            {
                entire.Property("UpdateTime").CurrentValue = DateTime.Now;
            }
            if (entire.State == EntityState.Deleted && removedTime != null && isRemoved != null)
            {
                entire.Property("RemovedTime").CurrentValue = DateTime.Now;
                entire.Property("IsRemoved").CurrentValue = true;
                entire.State = EntityState.Modified;
            }
        }

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

}
