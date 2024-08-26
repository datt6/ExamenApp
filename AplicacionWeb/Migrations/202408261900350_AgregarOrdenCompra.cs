namespace AplicacionWebNomina.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregarOrdenCompra : DbMigration
    {
        public override void Up()
        {/*
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Phone = c.String(),
                        Email = c.String(),
                        Street = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId);
            */
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        ItemId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        ListPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Product_Codigo = c.Int(),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_Codigo)
                .Index(t => t.OrderId)
                .Index(t => t.Product_Codigo);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        StoreId = c.Int(nullable: false),
                        StaffId = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        ShippedDate = c.DateTime(),
                        OrderStatus = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Staffs", t => t.StaffId, cascadeDelete: true)
                .ForeignKey("dbo.Stores", t => t.StoreId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.StoreId)
                .Index(t => t.StaffId);
            
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        StaffId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Phone = c.String(),
                        Active = c.Boolean(nullable: false),
                        StoreId = c.Int(nullable: false),
                        ManagerId = c.Int(),
                        Manager_StaffId = c.Int(),
                    })
                .PrimaryKey(t => t.StaffId)
                .ForeignKey("dbo.Staffs", t => t.Manager_StaffId)
                .ForeignKey("dbo.Stores", t => t.StoreId, cascadeDelete: true)
                .Index(t => t.StoreId)
                .Index(t => t.Manager_StaffId);
            
            CreateTable(
                "dbo.Stores",
                c => new
                    {
                        StoreId = c.Int(nullable: false, identity: true),
                        StoreName = c.String(nullable: false),
                        Phone = c.String(),
                        Email = c.String(),
                        Street = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                    })
                .PrimaryKey(t => t.StoreId);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        StoreId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Product_Codigo = c.Int(),
                    })
                .PrimaryKey(t => new { t.StoreId, t.ProductId })
                .ForeignKey("dbo.Products", t => t.Product_Codigo)
                .ForeignKey("dbo.Stores", t => t.StoreId, cascadeDelete: true)
                .Index(t => t.StoreId)
                .Index(t => t.Product_Codigo);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Codigo = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        List_Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Stock = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Codigo);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false, identity: true),
                        Usuario = c.String(),
                        Username = c.String(nullable: false, maxLength: 50),
                        Pass = c.String(nullable: false),
                        CorreoElectronico = c.String(nullable: false),
                        Rol = c.String(),
                        StoreId = c.Int(nullable: false),
                        Activo = c.Boolean(nullable: false),
                        Nombre = c.String(),
                        Apellido = c.String(),
                        StaffId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UsuarioId)
                .ForeignKey("dbo.Staffs", t => t.StaffId, cascadeDelete: true)
                .Index(t => t.StaffId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "StaffId", "dbo.Staffs");
            DropForeignKey("dbo.OrderItems", "Product_Codigo", "dbo.Products");
            DropForeignKey("dbo.Orders", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.Orders", "StaffId", "dbo.Staffs");
            DropForeignKey("dbo.Stocks", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.Stocks", "Product_Codigo", "dbo.Products");
            DropForeignKey("dbo.Staffs", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.Staffs", "Manager_StaffId", "dbo.Staffs");
            DropForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Users", new[] { "StaffId" });
            DropIndex("dbo.Stocks", new[] { "Product_Codigo" });
            DropIndex("dbo.Stocks", new[] { "StoreId" });
            DropIndex("dbo.Staffs", new[] { "Manager_StaffId" });
            DropIndex("dbo.Staffs", new[] { "StoreId" });
            DropIndex("dbo.Orders", new[] { "StaffId" });
            DropIndex("dbo.Orders", new[] { "StoreId" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.OrderItems", new[] { "Product_Codigo" });
            DropIndex("dbo.OrderItems", new[] { "OrderId" });
            DropTable("dbo.Users");
            DropTable("dbo.Products");
            DropTable("dbo.Stocks");
            DropTable("dbo.Stores");
            DropTable("dbo.Staffs");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Customers");
        }
    }
}
