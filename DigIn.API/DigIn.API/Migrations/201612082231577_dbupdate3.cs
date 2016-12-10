namespace DigIn.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbupdate3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Skills", "UserProfileModel_ID", "dbo.UserProfileModels");
            DropIndex("dbo.Skills", new[] { "UserProfileModel_ID" });
            RenameColumn(table: "dbo.Skills", name: "UserProfileModel_ID", newName: "UserProfileModelID");
            AlterColumn("dbo.Skills", "UserProfileModelID", c => c.Int(nullable: false));
            CreateIndex("dbo.Skills", "UserProfileModelID");
            AddForeignKey("dbo.Skills", "UserProfileModelID", "dbo.UserProfileModels", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Skills", "UserProfileModelID", "dbo.UserProfileModels");
            DropIndex("dbo.Skills", new[] { "UserProfileModelID" });
            AlterColumn("dbo.Skills", "UserProfileModelID", c => c.Int());
            RenameColumn(table: "dbo.Skills", name: "UserProfileModelID", newName: "UserProfileModel_ID");
            CreateIndex("dbo.Skills", "UserProfileModel_ID");
            AddForeignKey("dbo.Skills", "UserProfileModel_ID", "dbo.UserProfileModels", "ID");
        }
    }
}
