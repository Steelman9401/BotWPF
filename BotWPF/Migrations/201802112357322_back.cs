namespace BotWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class back : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.VideoCategories", name: "VideoRefId", newName: "Video_Id");
            RenameColumn(table: "dbo.VideoCategories", name: "CategoryRefId", newName: "Category_Id");
            RenameIndex(table: "dbo.VideoCategories", name: "IX_VideoRefId", newName: "IX_Video_Id");
            RenameIndex(table: "dbo.VideoCategories", name: "IX_CategoryRefId", newName: "IX_Category_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.VideoCategories", name: "IX_Category_Id", newName: "IX_CategoryRefId");
            RenameIndex(table: "dbo.VideoCategories", name: "IX_Video_Id", newName: "IX_VideoRefId");
            RenameColumn(table: "dbo.VideoCategories", name: "Category_Id", newName: "CategoryRefId");
            RenameColumn(table: "dbo.VideoCategories", name: "Video_Id", newName: "VideoRefId");
        }
    }
}
