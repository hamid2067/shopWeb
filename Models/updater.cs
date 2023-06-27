using Entities;
using Microsoft.AspNetCore.Identity;

namespace shopWeb.Models
{
    public  class updater
    {

        public  async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<User>>();

            var roleCheck3 = await RoleManager.RoleExistsAsync("PreAdmin");
            if (!roleCheck3)
            {
                //create the roles and seed them to the database
                var role = new Role() { Name = "PreAdmin", NormalizedName = "PreAdmin", Description = "..." };
                var roleresult = await RoleManager.CreateAsync(role);
                //  await RoleManager.CreateAsync(new Role("Admin"));
            }

            var roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck)
            {
                //create the roles and seed them to the database
                var role = new Role() { Name = "Admin", NormalizedName = "Admin", Description = "..." };
                var roleresult = await RoleManager.CreateAsync(role);
                //  await RoleManager.CreateAsync(new Role("Admin"));
            }

            var roleCheck2 = await RoleManager.RoleExistsAsync("Operator");
            if (!roleCheck2)
            {
                //create the roles and seed them to the database
                var role = new Role() { Name = "Operator", NormalizedName = "Operator", Description = "..." };
                var roleresult = await RoleManager.CreateAsync(role);
            }

            //  var roleCheck3 = await UserManager.FindByNameAsync("admin@gmail.com");
            var findSupplierUser = await UserManager.FindByNameAsync("admin2");
            if (findSupplierUser == null)
            {
                var supplierUser = new User()
                {
                    FullName = "admin2",
                    UserName = "admin2",
                  
                    NatinalCode = "2093609293",
                    PhoneNumber = "09384075832",
                    Email = "admin2@admin2.com",

                };
                var supplierResult = await UserManager.CreateAsync(supplierUser, "Hb661989@");
                await UserManager.AddToRoleAsync(supplierUser, "Admin");
            }
            else
            {
                await UserManager.AddToRoleAsync(findSupplierUser, "Admin");
            }



            var preuser = await UserManager.FindByNameAsync("user2");
            if (preuser == null)
            {
                var supplierUser = new User()
                {
                    FullName = "user2",
                    UserName = "user2",

                    NatinalCode = "2093609296",
                    PhoneNumber = "09384075836",
                    Email = "admin4@admin.com",
                };
                var supplierResult = await UserManager.CreateAsync(supplierUser, "tamir12@");
                await UserManager.AddToRoleAsync(supplierUser, "PreAdmin");
            }
            else
            {
                await UserManager.AddToRoleAsync(preuser, "PreAdmin");
            }




        }


    }
}
