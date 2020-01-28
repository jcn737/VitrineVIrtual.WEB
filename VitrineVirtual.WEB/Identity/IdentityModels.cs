using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;

namespace VitrineVirtual.WEB.Identity
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 5,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = true,
                RequireUppercase = false,
            };
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class VitrineVirtualDBContext : IdentityDbContext<ApplicationUser>
    {
        public VitrineVirtualDBContext()
            : base("VitrineVirtualDBContext", throwIfV1Schema: false)
        {
        }

        public static VitrineVirtualDBContext Create()
        {
            return new VitrineVirtualDBContext();
        }
    }
}