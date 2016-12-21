namespace DigIn.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbupdate8 : DbMigration
    {
        public override void Up()
        {
            AddForeignKey("dbo.ProjectContributors", "User_Id", "dbo.UserProfileModels", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
        }
    }
}
