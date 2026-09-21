using Microsoft.EntityFrameworkCore;
using MiniB2B.Entities;

namespace MiniB2B.DataAccess;

public class MiniB2BContext : DbContext
{
    public MiniB2BContext(DbContextOptions<MiniB2BContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<GridColumnConfig> GridColumnConfigs { get; set; }
    public DbSet<Slider> Sliders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ProductCode is Unique
        modelBuilder.Entity<Product>()
            .HasIndex(p => p.ProductCode)
            .IsUnique();

        // Seed Data
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // 1. Categories
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Elektronik", Description = "Elektronik Ürünler" },
            new Category { Id = 2, Name = "Kırtasiye", Description = "Ofis Kırtasiye" }
        );

        // 2. Products
        modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, CategoryId = 1, ProductCode = "LP-XPS15", ProductName = "Dell XPS 15 Laptop", Brand = "Dell", ManufacturerCode = "DELL-XPS-2026", SpecialCode1 = "YENI-NESIL", Price = 45000, StockQuantity = 15, CriticalStockLevel = 5, ImageUrl = "/images/dell_laptop_product_1789753837200.jpg", Description = "Yüksek performanslı iş bilgisayarı." },
                new Product { Id = 2, CategoryId = 1, ProductCode = "MS-MX3", ProductName = "Logitech MX Master 3", Brand = "Logitech", ManufacturerCode = "LOGI-MX3", Price = 2500, StockQuantity = 50, CriticalStockLevel = 10, ImageUrl = "/images/logitech_mouse_product_1789753847396.jpg", Description = "Ergonomik kablosuz mouse." },
                new Product { Id = 3, CategoryId = 2, ProductCode = "KP-A4", ProductName = "A4 Fotokopi Kağıdı (500'lü)", Brand = "Copier", SpecialCode1 = "TOPTAN", Price = 120, StockQuantity = 500, CriticalStockLevel = 100, ImageUrl = "/images/a4_paper_product_1789753857408.jpg", Description = "Premium kalite fotokopi kağıdı." }
        );

        // 3. Users (SHA256 of "123456" is "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92")
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, FirstName = "Admin", LastName = "User", Email = "admin@minib2b.com", Username = "admin", PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", Role = "Admin", CreatedAt = DateTime.Now },
            new User { Id = 2, FirstName = "Test", LastName = "Customer", Email = "customer@minib2b.com", Username = "customer", PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", Role = "Customer", CreatedAt = DateTime.Now }
        );

        // 4. GridColumnConfig
        modelBuilder.Entity<GridColumnConfig>().HasData(
            new GridColumnConfig { Id = 1, TableName = "Product", PropertyName = "ImageUrl", HeaderText = "Görsel", OrderIndex = 1, RenderType = "Image", IsVisible = true, Width = "60px" },
            new GridColumnConfig { Id = 2, TableName = "Product", PropertyName = "ProductCode", HeaderText = "Ürün Kodu", OrderIndex = 2, RenderType = "Text", IsVisible = true },
            new GridColumnConfig { Id = 3, TableName = "Product", PropertyName = "ProductName", HeaderText = "Ürün Adı", OrderIndex = 3, RenderType = "Text", IsVisible = true },
            new GridColumnConfig { Id = 4, TableName = "Product", PropertyName = "Brand", HeaderText = "Marka", OrderIndex = 4, RenderType = "Text", IsVisible = true },
            new GridColumnConfig { Id = 5, TableName = "Product", PropertyName = "Price", HeaderText = "Fiyat", OrderIndex = 5, RenderType = "Price", IsVisible = true },
            new GridColumnConfig { Id = 6, TableName = "Product", PropertyName = "StockQuantity", HeaderText = "Stok", OrderIndex = 6, RenderType = "StockStatus", IsVisible = true, Width = "100px" },
            new GridColumnConfig { Id = 7, TableName = "Product", PropertyName = "Id", HeaderText = "İşlem", OrderIndex = 7, RenderType = "Action", IsVisible = true, Width = "150px" }
        );

        // 5. Sliders
        modelBuilder.Entity<Slider>().HasData(
            new Slider { Id = 1, ImageUrl = "/images/b2b_banner_welcome_1789753868740.jpg", Title = "Banner 1", OrderIndex = 1, IsActive = true },
            new Slider { Id = 2, ImageUrl = "/images/b2b_banner_stock_1789753880181.jpg", Title = "Banner 2", OrderIndex = 2, IsActive = true },
            new Slider { Id = 3, ImageUrl = "/images/b2b_banner_discount_1789753891697.jpg", Title = "Banner 3", OrderIndex = 3, IsActive = true }
        );
    }
}
