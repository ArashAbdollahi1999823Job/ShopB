using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Users;
using Application.Interfaces.Contexts;
using Domain.Attributes;

namespace Persistence.Context
{
    public class DataBaseContext : DbContext, IDataBaseContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {



            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.ClrType.GetCustomAttributes(typeof(AuditableAttribute), true).Length > 0)
                {
                    modelBuilder.Entity<User>().Property<DateTime>("InsertTime");
                    modelBuilder.Entity<User>().Property<DateTime?>("UpdateTime");
                    modelBuilder.Entity<User>().Property<DateTime?>("RemoveTime");

                    modelBuilder.Entity<User>().Property<bool>("IsRemoved");


                }
            }



            base.OnModelCreating(modelBuilder);

        }

        public override int SaveChanges()
        {
            var modifiedEntries =
                ChangeTracker
                    .Entries()
                    .Where(x =>
                        x.State == EntityState.Modified ||
                        x.State == EntityState.Added ||
                        x.State == EntityState.Deleted);


            foreach (var item in modifiedEntries)
            {
                var entityType = item.Context.Model.FindEntityType(item.Entity.GetType());

                var inserted = entityType.FindProperty("InsertTime");
                var updated = entityType.FindProperty("UpdateTime");
                var removed = entityType.FindProperty("RemoveTime");
                var isRemoved = entityType.FindProperty("IsRemoved");


                if (item.State == EntityState.Added && inserted != null)
                {
                    item.Property("InsertTime").CurrentValue = DateTime.Now;
                }
                if (item.State == EntityState.Modified && updated != null)
                {
                    item.Property("UpdateTime").CurrentValue = DateTime.Now;
                }
                if (item.State == EntityState.Deleted && removed != null && isRemoved !=null)
                {
                    item.Property("RemoveTime").CurrentValue = DateTime.Now;
                    item.Property("IsRemoved").CurrentValue = true;
                }
             

            }


            return base.SaveChanges();
        }
    }

}
