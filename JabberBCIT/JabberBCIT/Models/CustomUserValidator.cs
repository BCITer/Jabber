using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using JabberBCIT.Models;

namespace JabberBCIT.Models
{
    public class CustomUserValidator<T> : UserValidator<ApplicationUser> {

        public CustomUserValidator(ApplicationUserManager manager) : base (manager)
        {

        }

        public override async Task<IdentityResult> ValidateAsync(ApplicationUser user)
        {
            IdentityResult result = await base.ValidateAsync(user);

            if (!user.Email.ToLower().EndsWith("@my.bcit.ca"))
            {
                var errors = result.Errors.ToList();
                errors.Add("You can only register with a BCIT email address");
                result = new IdentityResult(errors);
            }

            return result;
        }
    }
}
