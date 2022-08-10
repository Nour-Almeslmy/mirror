using DataAccessLayer.Data.Context;
using DataAccessLayer.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace DataAccessLayer.Data
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        //public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store) {}

        public ApplicationUserManager(ApplicationDbContext context) : base(new UserStore<ApplicationUser>(context))
        {
            this.UserValidator = new UserValidator<ApplicationUser>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = false
            };

            this.UserLockoutEnabledByDefault = true;
            this.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(2);
            this.MaxFailedAccessAttemptsBeforeLockout = 3;
        }



        //public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        //{
        //    var appDbContext = context.Get<ApplicationDbContext>();
        //    var appUserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(appDbContext));

        //    //Configure Validation Logic for UserName 
        //    appUserManager.UserValidator = new UserValidator<ApplicationUser>(appUserManager)
        //    {
        //        AllowOnlyAlphanumericUserNames = false,
        //        RequireUniqueEmail = false
        //    };

        //    //Configure Validation Logic for Passwords
        //    appUserManager.PasswordValidator = new PasswordValidator
        //    {
        //        RequiredLength = 6,
        //        RequireNonLetterOrDigit = false,
        //        RequireDigit = true,
        //        RequireLowercase = true,
        //        RequireUppercase = false
        //    };

        //    //Configure user lockout defaults
        //    appUserManager.UserLockoutEnabledByDefault = true;
        //    appUserManager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(2);
        //    appUserManager.MaxFailedAccessAttemptsBeforeLockout = 3;

        //    return appUserManager;

        //}

    }
}
