using Microsoft.EntityFrameworkCore;
using QuickGridDemo.Data.Models;

namespace QuickGridDemo.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly NorthwindContext _context;
        private IOrderedQueryable<Product>? products;

        public ProductRepository(NorthwindContext context)
        {
            _context = context;
        }

        
        public IQueryable<Product> SearchProducts(string? filter)
        {
            products = _context.Products.AsNoTracking().OrderBy(x => x.ProductId); //.Take(50);
            if (!string.IsNullOrEmpty(filter))
            {
                products = products.Where(x => EF.Functions.Like(x.ProductName, filter.Replace("%", ""))).OrderBy(x => x.ProductId);
            }
            return products;
        }
    }

    public interface IProductRepository
    {
        IQueryable<Product> SearchProducts(string? filter);
    }
}