using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HoneyBunny.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class AppDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // создаем две роли
            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole { Name = "user" };

            // добавляем роли в бд
            roleManager.Create(role1);
            roleManager.Create(role2);

            // создаем пользователей
            var admin = new ApplicationUser { Email = "roland_top@ukr.net", UserName = "roland_top@ukr.net" };
            string password = "123456";
            var result = userManager.Create(admin, password);

            // если создание пользователя прошло успешно
            if (result.Succeeded)
            {
                // добавляем для пользователя роль
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(admin.Id, role2.Name);
            }

            context.Categories.Add(new Category("Для лица", "Для лица", "Средства для лица", 0));
            context.Categories.Add(new Category("Для тела", "Для тела", "Средства для тела", 0));
            context.Categories.Add(new Category("Для ног", "Для ног", "Средства для ног", 0));

            context.Categories.Add(new Category("Крема", "Для лица/Крема", "Крема для лица", 1));
            context.Categories.Add(new Category("Муссы", "Для лица/Муссы", "Муссы для лица", 1));
            context.Categories.Add(new Category("Лосьоны", "Для лица/Лосьоны", "Лосьоны для лица", 1));

            context.Categories.Add(new Category("Крема", "Для тела/Крема", "Крема для тела", 2));
            context.Categories.Add(new Category("Муссы", "Для тела/Муссы", "Муссы для тела", 2));
            context.Categories.Add(new Category("Лосьоны", "Для тела/Лосьоны", "Лосьоны для тела", 2));

            context.Categories.Add(new Category("Крема", "Для ног/Крема", "Крема для ног", 3));
            context.Categories.Add(new Category("Муссы", "Для ног/Муссы", "Муссы для ног", 3));
            context.Categories.Add(new Category("Лосьоны", "Для ног/Лосьоны", "Лосьоны для ног", 3));

            base.Seed(context);
        }
    }
}