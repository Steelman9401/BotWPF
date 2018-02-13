namespace BotWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeDesc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Videos", "Description", c => c.String());
            DropColumn("dbo.Videos", "Desc");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Videos", "Desc", c => c.String());
            DropColumn("dbo.Videos", "Description");
        }
    }
}
