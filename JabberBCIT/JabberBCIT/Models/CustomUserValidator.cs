using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace JabberBCIT.Models
{
    public class CustomUserValidator<T> : UserValidator<User> {

        public CustomUserValidator(UserManager manager) : base (manager)
        {

        }

        public override async Task<IdentityResult> ValidateAsync(User user)
        {
            IdentityResult result = await base.ValidateAsync(user);

            if (!user.Email.ToLower().EndsWith("@my.bcit.ca") && !user.Email.ToLower().EndsWith("@bcit.ca"))
            {
                var errors = result.Errors.ToList();
                errors.Add("You can only register with a BCIT email address");
                result = new IdentityResult(errors);
            }

            return result;
        }
    }
}
