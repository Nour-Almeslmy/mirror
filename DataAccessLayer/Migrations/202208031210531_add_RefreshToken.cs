namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_RefreshToken : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "RefreshToken", c => c.Guid(nullable: false));
            AddColumn("dbo.Users", "RefreshTokenExpiryDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "FirstName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Users", "LastName", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "LastName", c => c.String());
            AlterColumn("dbo.Users", "FirstName", c => c.String());
            DropColumn("dbo.Users", "RefreshTokenExpiryDate");
            DropColumn("dbo.Users", "RefreshToken");
        }
    }
}
