namespace BotWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedVideoPreview : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Videos", "Preview", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Videos", "Preview");
        }
    }
}
