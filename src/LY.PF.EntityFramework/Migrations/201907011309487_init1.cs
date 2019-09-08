namespace LY.PF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClientType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientTypeName = c.String(maxLength: 128),
                        Remark = c.String(maxLength: 512),
                        IsValid = c.Boolean(nullable: false),
                        CreateBy = c.String(maxLength: 32),
                        UpdateBy = c.String(maxLength: 32),
                        UpdateTime = c.DateTime(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.District",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DistrictName = c.String(maxLength: 64),
                        Address = c.String(maxLength: 512),
                        Remark = c.String(maxLength: 512),
                        IsValid = c.Boolean(nullable: false),
                        ParentDistrictId = c.Int(nullable: false),
                        CreateBy = c.String(maxLength: 32),
                        UpdateBy = c.String(maxLength: 32),
                        UpdateTime = c.DateTime(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Indent",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductName = c.String(maxLength: 64),
                        ProductType = c.Int(nullable: false),
                        ProductModel = c.String(maxLength: 64),
                        ProductBrand = c.String(maxLength: 64),
                        ScheduleNumber = c.Int(nullable: false),
                        ImageUrl = c.String(maxLength: 512),
                        ActualNumber = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        Remark = c.String(maxLength: 512),
                        CreateBy = c.String(maxLength: 32),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateBy = c.String(maxLength: 32),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductTypeName = c.String(maxLength: 64),
                        Remark = c.String(maxLength: 512),
                        IsValid = c.Boolean(nullable: false),
                        CreateBy = c.String(maxLength: 32),
                        UpdateBy = c.String(maxLength: 32),
                        UpdateTime = c.DateTime(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SaleFunnel",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        District = c.Int(nullable: false),
                        Saler = c.String(maxLength: 64),
                        ClientName = c.String(maxLength: 64),
                        Adress = c.String(maxLength: 512),
                        ClientType = c.Int(nullable: false),
                        ProductType = c.Int(nullable: false),
                        ProductName = c.String(maxLength: 64),
                        ProductModel = c.String(maxLength: 64),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Number = c.Int(nullable: false),
                        SumPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StatementTime = c.DateTime(nullable: false),
                        ContendNumber = c.Int(nullable: false),
                        Opportunitie = c.String(maxLength: 64),
                        Stage = c.String(maxLength: 64),
                        StageTime = c.DateTime(nullable: false),
                        ChanceSum = c.String(maxLength: 64),
                        LastTime = c.DateTime(nullable: false),
                        NextAction = c.String(maxLength: 512),
                        NextTime = c.DateTime(nullable: false),
                        RivalA = c.String(maxLength: 64),
                        ProductModelA = c.String(maxLength: 64),
                        RivalB = c.String(maxLength: 64),
                        ProductModelB = c.String(maxLength: 64),
                        Contact = c.String(maxLength: 64),
                        ContactMobile = c.String(maxLength: 64),
                        Purchase = c.String(maxLength: 64),
                        PurchaseMobile = c.String(maxLength: 64),
                        Dean = c.String(maxLength: 64),
                        DeanMobile = c.String(maxLength: 64),
                        LeadSource = c.String(maxLength: 64),
                        CreationTime = c.DateTime(nullable: false),
                        CreateBy = c.String(maxLength: 32),
                        UpdateTime = c.DateTime(),
                        UpdateBy = c.String(maxLength: 32),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SaleOrder",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductName = c.String(maxLength: 64),
                        ProductType = c.Int(nullable: false),
                        ProductModel = c.String(maxLength: 64),
                        ProductBrand = c.String(maxLength: 64),
                        ScheduleNumber = c.Int(),
                        ImageUrl = c.String(maxLength: 512),
                        ActualNumber = c.Int(),
                        Price = c.Decimal(precision: 18, scale: 2),
                        TotalPrice = c.Decimal(precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        Remark = c.String(maxLength: 512),
                        CreateBy = c.String(maxLength: 32),
                        CreateTime = c.DateTime(nullable: false),
                        UpdateBy = c.String(maxLength: 32),
                        UpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SaleOrder");
            DropTable("dbo.SaleFunnel");
            DropTable("dbo.ProductType");
            DropTable("dbo.Indent");
            DropTable("dbo.District");
            DropTable("dbo.ClientType");
        }
    }
}
