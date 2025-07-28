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

namespace Infrastucture.Persistence

{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Devices> devices { get; set; }
        public DbSet<FeedBack> feedback { get; set; }
        public DbSet<MeintenancePlan> meintenancePlan { get; set; }
        public DbSet<MeintenanceRecord> meintenanceRecord { get; set; }
        public DbSet<NotificationLog> notificationLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>()
                .HasMany(x => x.NotificationLog)
                .WithMany(y => y.AppUsers)
                .UsingEntity(j => j.ToTable("UserNotification"));
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
