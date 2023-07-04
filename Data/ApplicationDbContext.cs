using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Common.Utility;
using Entities;
using Entities.Common;
using Entities.Menu;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        public ApplicationDbContext(DbContextOptions options)
           : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            var entitiesAssembly = typeof(IEntity).Assembly;

            modelBuilder.RegisterAllEntities<IEntity>(entitiesAssembly);
            modelBuilder.AddRestrictDeleteBehaviorConvention();

            modelBuilder.Entity<Menu>()
          .HasData(
           new Menu
           {
               Id = 1,
               NameMenu = "داشبورد",
               NameIcon = "icon-menu",
               PageAddress="/admin/dashbord/index",
               ParentId = 0,
           }
       );

            modelBuilder.Entity<Menu>()
       .HasData(
        new Menu
        {
            Id = 2,
            NameMenu = "محصولات",
            NameIcon = "icon-file-text",
            PageAddress="/admin/product",
            ParentId = 0,
        }
    );



            modelBuilder.Entity<Menu>()
   .HasData(
    new Menu
    {
        Id = 3,
        NameMenu = "افزودن محصولات",
        NameIcon = "icon-folder",
        PageAddress = "/admin/product",
        ParentId = 2,
    }
);

            // modelBuilder.Entity<User>().HasQueryFilter(p => p.FullName != null);



            //            modelBuilder.Entity<Role>()
            //             .HasData(
            //                 new Role
            //                 {
            //                     Id = 1,
            //                     Name = "Admin",
            //                     Description = "..."
            //                 }
            //             );

            //            modelBuilder.Entity<Role>()
            //            .HasData(
            //             new Role
            //             {
            //                 Id = 2,
            //                 Name = "User",
            //                 Description = "..."
            //             }
            //         );




            //            User user = new User()
            //            {
            //                Id = 1,
            //                FullName = "hamid motamedi",
            //                UserName = "modir",
            //                NatinalCode = "2093609293",
            //                PhoneNumber = "09384075832",
            //                Email = "admin2@admin2.com",
            //                LockoutEnabled = false,


            //            };

            //            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            //            passwordHasher.HashPassword(user, "12345");

            //            modelBuilder.Entity<User>().HasData(user);



            //            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
            //              new IdentityUserRole<int>() { RoleId = 1, UserId = 1 }
            //              );



            //            modelBuilder.Entity<Menu>()
            //          .HasData(
            //           new Menu
            //           {
            //               Id = 1,
            //               NameMenu="داشبورد",
            //               NameIcon= "icon-menu",
            //               ParentId=0,
            //           }
            //       );


            //            modelBuilder.Entity<Menu>()
            //       .HasData(
            //        new Menu
            //        {
            //            Id = 2,
            //            NameMenu = "محصولات",
            //            NameIcon = "icon-file-text",
            //            ParentId = 0,
            //        }
            //    );

            //            modelBuilder.Entity<Menu>()
            //   .HasData(
            //    new Menu
            //    {
            //        Id = 3,
            //        NameMenu = "افزودن محصولات",
            //        NameIcon = "icon-folder",
            //        ParentId = 2,
            //    }
            //);


        }

        public override int SaveChanges()
        {
            _cleanString();
            setShadowProperties();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            _cleanString();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            _cleanString();
            setShadowProperties();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private static int? getUserId(HttpContext httpContext)
        {
            int? userId = null;
            var userIdValue = httpContext?.User?.Identity?.GetUserId();
            if (!string.IsNullOrWhiteSpace(userIdValue))
            {
                userId = int.Parse(userIdValue);
            }
            else
            {

            }
            return userId;
        }

        public static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
        private void setShadowProperties()
        {
            var httpContextAccessor = this.GetService<IHttpContextAccessor>();
            var httpContext = httpContextAccessor?.HttpContext;
            var userAgent = httpContext?.Request?.Headers["User-Agent"].ToString();
            var userIp = httpContext?.Connection?.RemoteIpAddress?.ToString();
            var now = DateTime.UtcNow;
            var userId = getUserId(httpContext);



            var modifiedEntries = ChangeTracker.Entries<IEntity>()
                                               .Where(x => x.State == EntityState.Modified);
            foreach (var modifiedEntry in modifiedEntries)
            {
                if (modifiedEntry.Metadata.FindProperty("ModifiedDateTime") != null)
                {
                    modifiedEntry.Property("ModifiedDateTime").CurrentValue = now;
                    modifiedEntry.Property("ModifiedByUserId").CurrentValue = (userId != null ? userId.ToString() : "");
                }

                string hashing = "";
                foreach (var property in modifiedEntry.Properties)
                {
                    var columnName = property.Metadata.Name;
                    var columnValue = (property.OriginalValue != null ? property.OriginalValue.ToString() : "");
                    hashing += columnName + ":" + columnValue + ";";
                };

                if (modifiedEntry.Metadata.FindProperty("Hash") != null)
                {
                    modifiedEntry.Property("Hash").CurrentValue = GetHashString(hashing);
                }
            }

            var addedEntries = ChangeTracker.Entries<IEntity>()
                                            .Where(x => x.State == EntityState.Added);
            foreach (var addedEntry in addedEntries)
            {
                if (addedEntry.Metadata.FindProperty("CreatedDateTime") != null)
                {
                    addedEntry.Property("CreatedDateTime").CurrentValue = now;
                    addedEntry.Property("CreatedByUserId").CurrentValue = (userId != null ? userId.ToString() : "");
                }

            }



        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _cleanString();
            setShadowProperties();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void _cleanString()
        {
            var changedEntities = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
            foreach (var item in changedEntities)
            {
                if (item.Entity == null)
                    continue;

                var properties = item.Entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string));

                foreach (var property in properties)
                {
                    var propName = property.Name;
                    var val = (string)property.GetValue(item.Entity, null);

                    if (val.HasValue())
                    {
                        var newVal = val.Fa2En().FixPersianChars();
                        if (newVal == val)
                            continue;
                        property.SetValue(item.Entity, newVal, null);
                    }
                }
            }
        }

    }
}
