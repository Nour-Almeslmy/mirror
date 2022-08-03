namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changecolnamesback : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Roles", newName: "AspNetRoles");
            RenameTable(name: "dbo.UserRoles", newName: "AspNetUserRoles");
            RenameTable(name: "dbo.Users", newName: "AspNetUsers");
            RenameTable(name: "dbo.UserClaims", newName: "AspNetUserClaims");
            RenameTable(name: "dbo.UserLogins", newName: "AspNetUserLogins");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.AspNetUserLogins", newName: "UserLogins");
            RenameTable(name: "dbo.AspNetUserClaims", newName: "UserClaims");
            RenameTable(name: "dbo.AspNetUsers", newName: "Users");
            RenameTable(name: "dbo.AspNetUserRoles", newName: "UserRoles");
            RenameTable(name: "dbo.AspNetRoles", newName: "Roles");
        }
    }
}
