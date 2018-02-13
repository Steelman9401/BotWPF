namespace BotWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedDateViews : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Videos", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Videos", "Views", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Videos", "Views");
            DropColumn("dbo.Videos", "Date");
        }
    }
}
