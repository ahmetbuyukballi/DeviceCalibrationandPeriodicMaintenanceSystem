using Domain.Entites;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastucture.Audit
{
    public class BeforeSaveChanges
    {
        private readonly IHttpContextAccessor _httpContextAcessor;
        public BeforeSaveChanges(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAcessor = httpContextAccessor;
        }
        public List<Audits> PrepareAuditLogs(ChangeTracker changeTracker)
        {
            var user = _httpContextAcessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var userSafe = string.IsNullOrEmpty(user) ? "System" : user;
            var auditLog=new List<Audits>();
            var auditEntries=new List<AuditEntry>();
            foreach(var entry in changeTracker.Entries())
            {
                if (entry.Entity is Audits || entry.State == EntityState.Detached)
                    continue;
                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName=entry.Entity.GetType().Name;
                auditEntries.Add(auditEntry);

                switch (entry.State)
                {
                    case EntityState.Added:
                        auditEntry.AuditType = AuditType.Create;

                        foreach(var property in entry.Properties)
                        {
                            string propertyName = property.Metadata.Name;

                            if (property.Metadata.IsPrimaryKey())
                            {
                                auditEntry.KeyValues[propertyName] = property.CurrentValue;
                                continue;
                            }
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                        }
                        Console.WriteLine(auditEntry.TableName);
                        if (entry.Entity is IdentityUserRole<Guid>)
                        {
                            auditEntry.UserId = string.Empty;
                        }
                        else
                            auditEntry.UserId = userSafe;

                        break;

                    case EntityState.Deleted:
                        auditEntry.AuditType = AuditType.Delete;

                        foreach(var property in entry.Properties)
                        {
                            string propertyName = property.Metadata.Name;

                            if (property.Metadata.IsPrimaryKey()) continue;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                        }

                        auditEntry.UserId = userSafe;
                        break;

                    case EntityState.Modified:
                        foreach(var property in entry.Properties)
                        {
                            if(!property.IsModified) continue;

                            string propertyName = property.Metadata.Name;

                            auditEntry.ChangedColumns.Add(propertyName);
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                        }

                        auditEntry.AuditType = AuditType.Update;

                        auditEntry.UserId = userSafe;
                        break;

                }
            }
            foreach(var auditEntry in auditEntries)
            {
                auditLog.Add(auditEntry.ToAudit());
            }
            Console.WriteLine("Audit log sayısı: " + auditLog.Count);
            return auditLog;
        }
    }
}
