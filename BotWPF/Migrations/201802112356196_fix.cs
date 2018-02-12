namespace BotWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.VideoCategories", name: "Video_Id", newName: "VideoRefId");
            RenameColumn(table: "dbo.VideoCategories", name: "Category_Id", newName: "CategoryRefId");
            RenameIndex(table: "dbo.VideoCategories", name: "IX_Video_Id", newName: "IX_VideoRefId");
            RenameIndex(table: "dbo.VideoCategories", name: "IX_Category_Id", newName: "IX_CategoryRefId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.VideoCategories", name: "IX_CategoryRefId", newName: "IX_Category_Id");
            RenameIndex(table: "dbo.VideoCategories", name: "IX_VideoRefId", newName: "IX_Video_Id");
            RenameColumn(table: "dbo.VideoCategories", name: "CategoryRefId", newName: "Category_Id");
            RenameColumn(table: "dbo.VideoCategories", name: "VideoRefId", newName: "Video_Id");
        }
    }
}
