namespace DigIn.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbupdate5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserProfileModels", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserProfileModels", new[] { "UserId" });
            AddColumn("dbo.AspNetUsers", "UserProfile_ID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "UserProfile_ID");
            AddForeignKey("dbo.AspNetUsers", "UserProfile_ID", "dbo.UserProfileModels", "ID");
            DropColumn("dbo.UserProfileModels", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProfileModels", "UserId", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.AspNetUsers", "UserProfile_ID", "dbo.UserProfileModels");
            DropIndex("dbo.AspNetUsers", new[] { "UserProfile_ID" });
            DropColumn("dbo.AspNetUsers", "UserProfile_ID");
            CreateIndex("dbo.UserProfileModels", "UserId");
            AddForeignKey("dbo.UserProfileModels", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
