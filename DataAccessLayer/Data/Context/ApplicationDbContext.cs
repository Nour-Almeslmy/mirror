using DataAccessLayer.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace DataAccessLayer.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("name=BucketSubscribtionModel")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //public virtual DbSet<WSDLResponseLog> WSDLResponseLogs { get; set; }
        public virtual DbSet<WSDLResponseLog_ProfileStatus> ProfileStatusResponses { get; set; }
        public virtual DbSet<WSDLResponseLog_ServiceClassesMigration> ClassesMigrationResponses { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Change Identity Columns Names
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");

        }
    }
}