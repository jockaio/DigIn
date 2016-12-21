namespace DigIn.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbupdate5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "UserProfile_ID", "dbo.UserProfileModels");
            DropIndex("dbo.AspNetUsers", new[] { "UserProfile_ID" });
            AddColumn("dbo.UserProfileModels", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.UserProfileModels", "UserId");
            AddForeignKey("dbo.UserProfileModels", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.AspNetUsers", "UserProfile_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "UserProfile_ID", c => c.Int());
            DropForeignKey("dbo.UserProfileModels", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserProfileModels", new[] { "UserId" });
            DropColumn("dbo.UserProfileModels", "UserId");
            CreateIndex("dbo.AspNetUsers", "UserProfile_ID");
            AddForeignKey("dbo.AspNetUsers", "UserProfile_ID", "dbo.UserProfileModels", "ID");
        }
    }
}
