namespace DigIn.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbupdate11 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Skills", "SkillsCategory_ID", "dbo.SkillsCategories");
            DropIndex("dbo.Skills", new[] { "SkillsCategory_ID" });
            RenameColumn(table: "dbo.Skills", name: "SkillsCategory_ID", newName: "SkillsCategoryID");
            AlterColumn("dbo.Skills", "SkillsCategoryID", c => c.Int(nullable: false));
            CreateIndex("dbo.Skills", "SkillsCategoryID");
            AddForeignKey("dbo.Skills", "SkillsCategoryID", "dbo.SkillsCategories", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Skills", "SkillsCategoryID", "dbo.SkillsCategories");
            DropIndex("dbo.Skills", new[] { "SkillsCategoryID" });
            AlterColumn("dbo.Skills", "SkillsCategoryID", c => c.Int());
            RenameColumn(table: "dbo.Skills", name: "SkillsCategoryID", newName: "SkillsCategory_ID");
            CreateIndex("dbo.Skills", "SkillsCategory_ID");
            AddForeignKey("dbo.Skills", "SkillsCategory_ID", "dbo.SkillsCategories", "ID");
        }
    }
}
