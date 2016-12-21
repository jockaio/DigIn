namespace DigIn.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbupdate6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfileModels", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfileModels", "Description");
        }
    }
}
