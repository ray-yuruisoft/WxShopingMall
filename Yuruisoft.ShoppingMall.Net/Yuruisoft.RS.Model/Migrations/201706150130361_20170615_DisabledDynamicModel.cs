namespace Yuruisoft.RS.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20170615_DisabledDynamicModel : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.RuntimeModel_PersonInfo");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RuntimeModel_PersonInfo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Sex = c.String(nullable: false, maxLength: 5),
                        Age = c.Int(nullable: false),
                        Mark = c.String(maxLength: 100),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
