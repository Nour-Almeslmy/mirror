namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nodateJoined : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "DateJoined");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "DateJoined", c => c.DateTime(nullable: false));
        }
    }
}
