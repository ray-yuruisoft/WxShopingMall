namespace Yuruisoft.RS.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActionInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ActionInfoName = c.String(),
                        ControllerName = c.String(),
                        ActionMethodName = c.String(),
                        Url = c.String(),
                        ActionTypeEnum = c.Short(nullable: false),
                        MenuIcon = c.String(),
                        IconWidth = c.Int(nullable: false),
                        IconHeight = c.Int(nullable: false),
                        HttpMethod = c.String(),
                        SubTime = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        Remark = c.String(),
                        DelFlag = c.Short(nullable: false),
                        Sort = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DepName = c.String(),
                        ParentId = c.Int(nullable: false),
                        TreePath = c.String(),
                        Level = c.Int(nullable: false),
                        IsLeaf = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UName = c.String(nullable: false, maxLength: 25),
                        UPwd = c.String(nullable: false, maxLength: 12),
                        TUPwd = c.String(),
                        UEmail = c.String(nullable: false, maxLength: 255),
                        UPhoneNumber = c.Long(nullable: false),
                        SubTime = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        Remark = c.String(),
                        DelFlag = c.Short(nullable: false),
                        Sort = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.R_UserInfo_ActionInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserInfoID = c.Int(nullable: false),
                        ActionInfoID = c.Int(nullable: false),
                        IsPass = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ActionInfo", t => t.ActionInfoID, cascadeDelete: true)
                .ForeignKey("dbo.UserInfo", t => t.UserInfoID, cascadeDelete: true)
                .Index(t => t.UserInfoID)
                .Index(t => t.ActionInfoID);
            
            CreateTable(
                "dbo.RoleInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                        DelFlag = c.Short(nullable: false),
                        SubTime = c.DateTime(nullable: false),
                        Remark = c.String(),
                        ModifiedOn = c.DateTime(nullable: false),
                        Sort = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ExtensionAgents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        GUID = c.String(nullable: false),
                        LName = c.String(nullable: false),
                        UrlName = c.String(nullable: false),
                        ExtensionUrl = c.String(nullable: false),
                        ExtensionScore = c.Long(nullable: false),
                        Remark = c.String(),
                        DelFlag = c.Short(nullable: false),
                        Sort = c.String(),
                        SubTime = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        RouteStatisticsLinks_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RouteStatisticsLinks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LName = c.String(nullable: false),
                        Url = c.String(nullable: false),
                        Remark = c.String(),
                        DelFlag = c.Short(nullable: false),
                        Sort = c.String(),
                        SubTime = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
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
            
            CreateTable(
                "dbo.DepartmentActionInfo",
                c => new
                    {
                        Department_ID = c.Int(nullable: false),
                        ActionInfo_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Department_ID, t.ActionInfo_ID })
                .ForeignKey("dbo.Department", t => t.Department_ID, cascadeDelete: true)
                .ForeignKey("dbo.ActionInfo", t => t.ActionInfo_ID, cascadeDelete: true)
                .Index(t => t.Department_ID)
                .Index(t => t.ActionInfo_ID);
            
            CreateTable(
                "dbo.UserInfoDepartment",
                c => new
                    {
                        UserInfo_ID = c.Int(nullable: false),
                        Department_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserInfo_ID, t.Department_ID })
                .ForeignKey("dbo.UserInfo", t => t.UserInfo_ID, cascadeDelete: true)
                .ForeignKey("dbo.Department", t => t.Department_ID, cascadeDelete: true)
                .Index(t => t.UserInfo_ID)
                .Index(t => t.Department_ID);
            
            CreateTable(
                "dbo.RoleInfoActionInfo",
                c => new
                    {
                        RoleInfo_ID = c.Int(nullable: false),
                        ActionInfo_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleInfo_ID, t.ActionInfo_ID })
                .ForeignKey("dbo.RoleInfo", t => t.RoleInfo_ID, cascadeDelete: true)
                .ForeignKey("dbo.ActionInfo", t => t.ActionInfo_ID, cascadeDelete: true)
                .Index(t => t.RoleInfo_ID)
                .Index(t => t.ActionInfo_ID);
            
            CreateTable(
                "dbo.RoleInfoUserInfo",
                c => new
                    {
                        RoleInfo_ID = c.Int(nullable: false),
                        UserInfo_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleInfo_ID, t.UserInfo_ID })
                .ForeignKey("dbo.RoleInfo", t => t.RoleInfo_ID, cascadeDelete: true)
                .ForeignKey("dbo.UserInfo", t => t.UserInfo_ID, cascadeDelete: true)
                .Index(t => t.RoleInfo_ID)
                .Index(t => t.UserInfo_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoleInfoUserInfo", "UserInfo_ID", "dbo.UserInfo");
            DropForeignKey("dbo.RoleInfoUserInfo", "RoleInfo_ID", "dbo.RoleInfo");
            DropForeignKey("dbo.RoleInfoActionInfo", "ActionInfo_ID", "dbo.ActionInfo");
            DropForeignKey("dbo.RoleInfoActionInfo", "RoleInfo_ID", "dbo.RoleInfo");
            DropForeignKey("dbo.R_UserInfo_ActionInfo", "UserInfoID", "dbo.UserInfo");
            DropForeignKey("dbo.R_UserInfo_ActionInfo", "ActionInfoID", "dbo.ActionInfo");
            DropForeignKey("dbo.UserInfoDepartment", "Department_ID", "dbo.Department");
            DropForeignKey("dbo.UserInfoDepartment", "UserInfo_ID", "dbo.UserInfo");
            DropForeignKey("dbo.DepartmentActionInfo", "ActionInfo_ID", "dbo.ActionInfo");
            DropForeignKey("dbo.DepartmentActionInfo", "Department_ID", "dbo.Department");
            DropIndex("dbo.RoleInfoUserInfo", new[] { "UserInfo_ID" });
            DropIndex("dbo.RoleInfoUserInfo", new[] { "RoleInfo_ID" });
            DropIndex("dbo.RoleInfoActionInfo", new[] { "ActionInfo_ID" });
            DropIndex("dbo.RoleInfoActionInfo", new[] { "RoleInfo_ID" });
            DropIndex("dbo.UserInfoDepartment", new[] { "Department_ID" });
            DropIndex("dbo.UserInfoDepartment", new[] { "UserInfo_ID" });
            DropIndex("dbo.DepartmentActionInfo", new[] { "ActionInfo_ID" });
            DropIndex("dbo.DepartmentActionInfo", new[] { "Department_ID" });
            DropIndex("dbo.R_UserInfo_ActionInfo", new[] { "ActionInfoID" });
            DropIndex("dbo.R_UserInfo_ActionInfo", new[] { "UserInfoID" });
            DropTable("dbo.RoleInfoUserInfo");
            DropTable("dbo.RoleInfoActionInfo");
            DropTable("dbo.UserInfoDepartment");
            DropTable("dbo.DepartmentActionInfo");
            DropTable("dbo.RuntimeModel_PersonInfo");
            DropTable("dbo.RouteStatisticsLinks");
            DropTable("dbo.ExtensionAgents");
            DropTable("dbo.RoleInfo");
            DropTable("dbo.R_UserInfo_ActionInfo");
            DropTable("dbo.UserInfo");
            DropTable("dbo.Department");
            DropTable("dbo.ActionInfo");
        }
    }
}
