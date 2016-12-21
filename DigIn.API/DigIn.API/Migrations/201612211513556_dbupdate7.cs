namespace DigIn.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbupdate7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProjectModels", "ProjectOwnerID", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProjectContributors", "FK_dbo.ProjectContributors_dbo.AspNetUsers_User_Id");
            DropIndex("dbo.ProjectModels", new[] { "ProjectOwnerID" });
            DropIndex("dbo.ProjectContributors", new[] { "User_Id" });
            AlterColumn("dbo.ProjectModels", "ProjectOwnerID", c => c.Int(nullable: false));
            AlterColumn("dbo.ProjectContributors", "User_ID", c => c.Int());
            CreateIndex("dbo.ProjectModels", "ProjectOwnerID");
            CreateIndex("dbo.ProjectContributors", "User_ID");
            AddForeignKey("dbo.ProjectModels", "ProjectOwnerID", "dbo.UserProfileModels", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectModels", "ProjectOwnerID", "dbo.UserProfileModels");
            DropIndex("dbo.ProjectContributors", new[] { "User_ID" });
            DropIndex("dbo.ProjectModels", new[] { "ProjectOwnerID" });
            AlterColumn("dbo.ProjectContributors", "User_ID", c => c.String(maxLength: 128));
            AlterColumn("dbo.ProjectModels", "ProjectOwnerID", c => c.String(maxLength: 128));
            CreateIndex("dbo.ProjectContributors", "User_Id");
            CreateIndex("dbo.ProjectModels", "ProjectOwnerID");
            AddForeignKey("dbo.ProjectModels", "ProjectOwnerID", "dbo.AspNetUsers", "Id");
        }
    }
}
