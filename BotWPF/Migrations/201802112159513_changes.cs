namespace BotWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Videos", "Img", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Videos", "Img");
        }
    }
}
