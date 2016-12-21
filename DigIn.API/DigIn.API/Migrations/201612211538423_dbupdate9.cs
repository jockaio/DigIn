namespace DigIn.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbupdate9 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProjectContributors", "User_Id", "dbo.UserProfileModels");
            DropIndex("dbo.ProjectContributors", new[] { "User_ID" });
            RenameColumn(table: "dbo.ProjectContributors", name: "User_Id", newName: "User_ID");
            AlterColumn("dbo.ProjectContributors", "User_ID", c => c.Int());
            CreateIndex("dbo.ProjectContributors", "User_ID");
            AddForeignKey("dbo.ProjectContributors", "User_ID", "dbo.UserProfileModels", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectContributors", "User_ID", "dbo.UserProfileModels");
            DropIndex("dbo.ProjectContributors", new[] { "User_ID" });
            AlterColumn("dbo.ProjectContributors", "User_ID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.ProjectContributors", name: "User_ID", newName: "UserID");
            CreateIndex("dbo.ProjectContributors", "UserID");
            AddForeignKey("dbo.ProjectContributors", "UserID", "dbo.UserProfileModels", "ID", cascadeDelete: true);
        }
    }
}
