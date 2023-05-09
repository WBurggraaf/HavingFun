using Infrastructure.DB.AdventureWorks.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DB.AdventureWorks.Contexts;

public partial class AdventureWorksDbContext : DbContext
{
    public AdventureWorksDbContext()
    {
    }

    public AdventureWorksDbContext(DbContextOptions<AdventureWorksDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<BuildVersion> BuildVersions { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; }

    public virtual DbSet<ErrorLog> ErrorLogs { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductDescription> ProductDescriptions { get; set; }

    public virtual DbSet<ProductModel> ProductModels { get; set; }

    public virtual DbSet<ProductModelProductDescription> ProductModelProductDescriptions { get; set; }

    public virtual DbSet<SalesOrderDetail> SalesOrderDetails { get; set; }

    public virtual DbSet<SalesOrderHeader> SalesOrderHeaders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=C:\\temp\\AdventureWorks_Wilco.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Address");

            entity.HasIndex(e => e.Rowguid, "Address_AK_Address_rowguid")
                .IsUnique()
                .IsDescending();

            entity.HasIndex(e => new { e.AddressLine1, e.AddressLine2, e.City, e.StateProvince, e.PostalCode, e.CountryRegion }, "Address_IX_Address_AddressLine1_AddressLine2_City_StateProvince_PostalCode_CountryRegion").IsDescending();

            entity.HasIndex(e => e.StateProvince, "Address_IX_Address_StateProvince").IsDescending();

            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.AddressLine1).HasColumnType("nvarchar(60)");
            entity.Property(e => e.AddressLine2).HasColumnType("nvarchar(60)");
            entity.Property(e => e.City).HasColumnType("nvarchar(30)");
            entity.Property(e => e.CountryRegion).HasColumnType("nvarchar(50)");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.PostalCode).HasColumnType("nvarchar(15)");
            entity.Property(e => e.Rowguid)
                .HasColumnType("guid")
                .HasColumnName("rowguid");
            entity.Property(e => e.StateProvince).HasColumnType("nvarchar(50)");
        });

        modelBuilder.Entity<BuildVersion>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("BuildVersion");

            entity.Property(e => e.DatabaseVersion)
                .HasColumnType("nvarchar(25)")
                .HasColumnName("Database Version");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.SystemInformationId).HasColumnName("SystemInformationID");
            entity.Property(e => e.VersionDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Customer");

            entity.HasIndex(e => e.Rowguid, "Customer_AK_Customer_rowguid")
                .IsUnique()
                .IsDescending();

            entity.HasIndex(e => e.EmailAddress, "Customer_IX_Customer_EmailAddress").IsDescending();

            entity.Property(e => e.CompanyName).HasColumnType("nvarchar(128)");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.EmailAddress).HasColumnType("nvarchar(50)");
            entity.Property(e => e.FirstName).HasColumnType("nvarchar(50)");
            entity.Property(e => e.LastName).HasColumnType("nvarchar(50)");
            entity.Property(e => e.MiddleName).HasColumnType("nvarchar(50)");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.NameStyle)
                .HasDefaultValueSql("0")
                .HasColumnType("bit");
            entity.Property(e => e.PasswordHash).HasColumnType("varchar(128)");
            entity.Property(e => e.PasswordSalt).HasColumnType("varchar(10)");
            entity.Property(e => e.Phone).HasColumnType("nvarchar(25)");
            entity.Property(e => e.Rowguid)
                .HasColumnType("guid")
                .HasColumnName("rowguid");
            entity.Property(e => e.SalesPerson).HasColumnType("nvarchar(256)");
            entity.Property(e => e.Suffix).HasColumnType("nvarchar(10)");
            entity.Property(e => e.Title).HasColumnType("nvarchar(8)");
        });

        modelBuilder.Entity<CustomerAddress>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CustomerAddress");

            entity.HasIndex(e => e.Rowguid, "CustomerAddress_AK_CustomerAddress_rowguid")
                .IsUnique()
                .IsDescending();

            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.AddressType).HasColumnType("nvarchar(50)");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Rowguid)
                .HasColumnType("guid")
                .HasColumnName("rowguid");
        });

        modelBuilder.Entity<ErrorLog>(entity =>
        {
            entity.ToTable("ErrorLog");

            entity.Property(e => e.ErrorLogId).HasColumnName("ErrorLogID");
            entity.Property(e => e.ErrorMessage).HasColumnType("nvarchar(4000)");
            entity.Property(e => e.ErrorProcedure).HasColumnType("nvarchar(126)");
            entity.Property(e => e.ErrorTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.UserName).HasColumnType("nvarchar(128)");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Product");

            entity.HasIndex(e => e.Name, "Product_AK_Product_Name")
                .IsUnique()
                .IsDescending();

            entity.HasIndex(e => e.ProductNumber, "Product_AK_Product_ProductNumber")
                .IsUnique()
                .IsDescending();

            entity.HasIndex(e => e.Rowguid, "Product_AK_Product_rowguid")
                .IsUnique()
                .IsDescending();

            entity.Property(e => e.Color).HasColumnType("nvarchar(15)");
            entity.Property(e => e.DiscontinuedDate).HasColumnType("datetime");
            entity.Property(e => e.ListPrice).HasColumnType("numeric");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasColumnType("nvarchar(50)");
            entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ProductModelId).HasColumnName("ProductModelID");
            entity.Property(e => e.ProductNumber).HasColumnType("nvarchar(25)");
            entity.Property(e => e.Rowguid)
                .HasColumnType("guid")
                .HasColumnName("rowguid");
            entity.Property(e => e.SellEndDate).HasColumnType("datetime");
            entity.Property(e => e.SellStartDate).HasColumnType("datetime");
            entity.Property(e => e.Size).HasColumnType("nvarchar(5)");
            entity.Property(e => e.StandardCost).HasColumnType("numeric");
            entity.Property(e => e.ThumbnailPhotoFileName).HasColumnType("nvarchar(50)");
            entity.Property(e => e.Weight).HasColumnType("numeric");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ProductCategory");

            entity.HasIndex(e => e.Name, "ProductCategory_AK_ProductCategory_Name")
                .IsUnique()
                .IsDescending();

            entity.HasIndex(e => e.Rowguid, "ProductCategory_AK_ProductCategory_rowguid")
                .IsUnique()
                .IsDescending();

            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasColumnType("nvarchar(50)");
            entity.Property(e => e.ParentProductCategoryId).HasColumnName("ParentProductCategoryID");
            entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");
            entity.Property(e => e.Rowguid)
                .HasColumnType("guid")
                .HasColumnName("rowguid");
        });

        modelBuilder.Entity<ProductDescription>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ProductDescription");

            entity.HasIndex(e => e.Rowguid, "ProductDescription_AK_ProductDescription_rowguid")
                .IsUnique()
                .IsDescending();

            entity.Property(e => e.Description).HasColumnType("nvarchar(400)");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.ProductDescriptionId).HasColumnName("ProductDescriptionID");
            entity.Property(e => e.Rowguid)
                .HasColumnType("guid")
                .HasColumnName("rowguid");
        });

        modelBuilder.Entity<ProductModel>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ProductModel");

            entity.HasIndex(e => e.Name, "ProductModel_AK_ProductModel_Name")
                .IsUnique()
                .IsDescending();

            entity.HasIndex(e => e.Rowguid, "ProductModel_AK_ProductModel_rowguid")
                .IsUnique()
                .IsDescending();

            entity.Property(e => e.CatalogDescription).HasColumnType("varchar");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasColumnType("nvarchar(50)");
            entity.Property(e => e.ProductModelId).HasColumnName("ProductModelID");
            entity.Property(e => e.Rowguid)
                .HasColumnType("guid")
                .HasColumnName("rowguid");
        });

        modelBuilder.Entity<ProductModelProductDescription>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ProductModelProductDescription");

            entity.HasIndex(e => e.Rowguid, "ProductModelProductDescription_AK_ProductModelProductDescription_rowguid")
                .IsUnique()
                .IsDescending();

            entity.Property(e => e.Culture).HasColumnType("char(6)");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.ProductDescriptionId).HasColumnName("ProductDescriptionID");
            entity.Property(e => e.ProductModelId).HasColumnName("ProductModelID");
            entity.Property(e => e.Rowguid)
                .HasColumnType("guid")
                .HasColumnName("rowguid");
        });

        modelBuilder.Entity<SalesOrderDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SalesOrderDetail");

            entity.HasIndex(e => e.Rowguid, "SalesOrderDetail_AK_SalesOrderDetail_rowguid")
                .IsUnique()
                .IsDescending();

            entity.HasIndex(e => e.ProductId, "SalesOrderDetail_IX_SalesOrderDetail_ProductID").IsDescending();

            entity.Property(e => e.LineTotal).HasColumnType("numeric");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.OrderQty).HasColumnType("smallint");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Rowguid)
                .HasColumnType("guid")
                .HasColumnName("rowguid");
            entity.Property(e => e.SalesOrderDetailId).HasColumnName("SalesOrderDetailID");
            entity.Property(e => e.SalesOrderId).HasColumnName("SalesOrderID");
            entity.Property(e => e.UnitPrice).HasColumnType("numeric");
            entity.Property(e => e.UnitPriceDiscount)
                .HasDefaultValueSql("0.0")
                .HasColumnType("numeric");
        });

        modelBuilder.Entity<SalesOrderHeader>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SalesOrderHeader");

            entity.HasIndex(e => e.SalesOrderNumber, "SalesOrderHeader_AK_SalesOrderHeader_SalesOrderNumber")
                .IsUnique()
                .IsDescending();

            entity.HasIndex(e => e.Rowguid, "SalesOrderHeader_AK_SalesOrderHeader_rowguid")
                .IsUnique()
                .IsDescending();

            entity.HasIndex(e => e.CustomerId, "SalesOrderHeader_IX_SalesOrderHeader_CustomerID").IsDescending();

            entity.Property(e => e.AccountNumber).HasColumnType("nvarchar(15)");
            entity.Property(e => e.BillToAddressId).HasColumnName("BillToAddressID");
            entity.Property(e => e.Comment).HasColumnType("nvarchar");
            entity.Property(e => e.CreditCardApprovalCode).HasColumnType("varchar(15)");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.DueDate).HasColumnType("datetime");
            entity.Property(e => e.Freight)
                .HasDefaultValueSql("0.00")
                .HasColumnType("numeric");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.OnlineOrderFlag)
                .HasDefaultValueSql("1")
                .HasColumnType("bit");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.PurchaseOrderNumber).HasColumnType("nvarchar(25)");
            entity.Property(e => e.RevisionNumber).HasColumnType("smallint");
            entity.Property(e => e.Rowguid)
                .HasColumnType("guid")
                .HasColumnName("rowguid");
            entity.Property(e => e.SalesOrderId).HasColumnName("SalesOrderID");
            entity.Property(e => e.SalesOrderNumber).HasColumnType("nvarchar(25)");
            entity.Property(e => e.ShipDate).HasColumnType("datetime");
            entity.Property(e => e.ShipMethod).HasColumnType("nvarchar(50)");
            entity.Property(e => e.ShipToAddressId).HasColumnName("ShipToAddressID");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("1")
                .HasColumnType("smallint");
            entity.Property(e => e.SubTotal)
                .HasDefaultValueSql("0.00")
                .HasColumnType("numeric");
            entity.Property(e => e.TaxAmt)
                .HasDefaultValueSql("0.00")
                .HasColumnType("numeric");
            entity.Property(e => e.TotalDue).HasColumnType("numeric");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
