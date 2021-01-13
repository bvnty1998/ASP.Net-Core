using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.IRepositories;

namespace TeduCoreApp.EF.Repositories
{
    public class ProductRepository : EFRepository<Product,int>,IProductRepository
    {
        AppDbContext _context;
        public ProductRepository(AppDbContext context):base(context)
        {
            _context = context;
        }

        public Product UpdateProduct(Product product)
        {
           _context.Update(product);
            _context.SaveChanges();
            return product;
        }
    }
}
