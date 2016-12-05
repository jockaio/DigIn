namespace DigIn.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbupdate1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Experiences",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Project_ID = c.Int(),
                        UserProfileModel_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProjectModels", t => t.Project_ID)
                .ForeignKey("dbo.UserProfileModels", t => t.UserProfileModel_ID)
                .Index(t => t.Project_ID)
                .Index(t => t.UserProfileModel_ID);
            
            DropColumn("dbo.UserProfileModels", "test");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProfileModels", "test", c => c.Int(nullable: false));
            DropForeignKey("dbo.Experiences", "UserProfileModel_ID", "dbo.UserProfileModels");
            DropForeignKey("dbo.Experiences", "Project_ID", "dbo.ProjectModels");
            DropIndex("dbo.Experiences", new[] { "UserProfileModel_ID" });
            DropIndex("dbo.Experiences", new[] { "Project_ID" });
            DropTable("dbo.Experiences");
        }
    }
}
