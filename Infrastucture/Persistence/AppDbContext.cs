using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Domain.Entites;
using System.Reflection;
using Infrastucture.Audit;
using System.Security.AccessControl;
using Microsoft.AspNetCore.Http;

namespace Infrastucture.Persistence

{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        private readonly BeforeSaveChanges _beforeSaveChanges;
        public AppDbContext(DbContextOptions options,BeforeSaveChanges beforeSaveChanges) : base(options)
        {
            _beforeSaveChanges = beforeSaveChanges;
        }
        public DbSet<Devices> devices { get; set; }
        public DbSet<FeedBack> feedback { get; set; }
        public DbSet<MeintenancePlan> meintenancePlan { get; set; }
        public DbSet<MeintenanceRecord> meintenanceRecord { get; set; }
        public DbSet<NotificationLog> notificationLogs { get; set; }
        public DbSet<Audits> AuditLogs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>()
                .HasMany(x => x.NotificationLog)
                .WithMany(y => y.AppUsers)
                .UsingEntity(j => j.ToTable("UserNotification"));
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<AppUser>().ToTable("User");
            modelBuilder.Entity<AppRole>().ToTable("Role");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("UserRole");
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogin");
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaim");
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaim");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("UserToken");
        }
        public override int SaveChanges()
        {
            // veya DI ile alırsın
            var auditLogs = _beforeSaveChanges.PrepareAuditLogs(ChangeTracker);
            AuditLogs.AddRange(auditLogs);

            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
           // veya DI ile alırsın
            var auditLogs = _beforeSaveChanges.PrepareAuditLogs(ChangeTracker);
            AuditLogs.AddRange(auditLogs);

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

    }
}
