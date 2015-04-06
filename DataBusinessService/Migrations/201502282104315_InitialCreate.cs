using System.Data.Entity.Migrations;

namespace DataBusinessService.Migrations
{
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blogs",
                c => new
                     {
                         BlogId = c.Int(false, true),
                         Name = c.String()
                     })
                .PrimaryKey(t => t.BlogId);

            CreateTable(
                "dbo.Posts",
                c => new
                     {
                         PostId = c.Int(false, true),
                         Title = c.String(),
                         Content = c.String(),
                         BlogId = c.Int(false)
                     })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.Blogs", t => t.BlogId, true)
                .Index(t => t.BlogId);

            CreateTable(
                "dbo.MyEntities",
                c => new
                     {
                         Id = c.Int(false, true),
                         Name = c.String()
                     })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Posts", "BlogId", "dbo.Blogs");
            DropIndex("dbo.Posts", new[] {"BlogId"});
            DropTable("dbo.MyEntities");
            DropTable("dbo.Posts");
            DropTable("dbo.Blogs");
        }
    }
}