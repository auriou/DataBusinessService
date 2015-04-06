using System.Data.Entity.Migrations;

namespace DataBusinessService.Migrations
{
    public partial class deleteTableMyEntity : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.MyEntities");
        }

        public override void Down()
        {
            CreateTable(
                "dbo.MyEntities",
                c => new
                     {
                         Id = c.Int(false, true),
                         Name = c.String()
                     })
                .PrimaryKey(t => t.Id);
        }
    }
}