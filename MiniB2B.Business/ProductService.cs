using Microsoft.EntityFrameworkCore;
using MiniB2B.DataAccess;
using MiniB2B.Entities;

namespace MiniB2B.Business;

public class ProductService
{
    private readonly MiniB2BContext _context;

    public ProductService(MiniB2BContext context)
    {
        _context = context;
    }

    public async Task<List<GridColumnConfig>> GetGridConfigurationAsync()
    {
        return await _context.GridColumnConfigs
            .Where(c => c.TableName == "Product" && c.IsVisible)
            .OrderBy(c => c.OrderIndex)
            .ToListAsync();
    }

    public async Task<List<Product>> GetProductsAsync(string? searchTerm = null)
    {
        var query = _context.Products.Include(p => p.Category).AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var term = searchTerm.ToLower();
            query = query.Where(p => 
                p.ProductCode.ToLower().Contains(term) ||
                p.ProductName.ToLower().Contains(term) ||
                (p.Brand != null && p.Brand.ToLower().Contains(term)) ||
                (p.ManufacturerCode != null && p.ManufacturerCode.ToLower().Contains(term)) ||
                (p.SpecialCode1 != null && p.SpecialCode1.ToLower().Contains(term)) ||
                (p.SpecialCode2 != null && p.SpecialCode2.ToLower().Contains(term)) ||
                (p.Description != null && p.Description.ToLower().Contains(term)) ||
                (p.Category != null && p.Category.Name.ToLower().Contains(term))
            );
        }

        return await query.OrderBy(p => p.ProductName).ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
    }
}
