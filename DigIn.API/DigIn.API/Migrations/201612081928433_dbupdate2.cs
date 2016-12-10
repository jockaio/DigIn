namespace DigIn.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbupdate2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfileModels", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfileModels", "Email");
        }
    }
}
