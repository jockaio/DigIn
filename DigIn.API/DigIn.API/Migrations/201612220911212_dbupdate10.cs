namespace DigIn.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbupdate10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SkillsCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Category = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Skills", "SkillsCategory_ID", c => c.Int());
            CreateIndex("dbo.Skills", "SkillsCategory_ID");
            AddForeignKey("dbo.Skills", "SkillsCategory_ID", "dbo.SkillsCategories", "ID");
            DropColumn("dbo.Skills", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Skills", "Name", c => c.String());
            DropForeignKey("dbo.Skills", "SkillsCategory_ID", "dbo.SkillsCategories");
            DropIndex("dbo.Skills", new[] { "SkillsCategory_ID" });
            DropColumn("dbo.Skills", "SkillsCategory_ID");
            DropTable("dbo.SkillsCategories");
        }
    }
}
