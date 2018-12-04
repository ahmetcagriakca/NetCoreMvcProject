using Microsoft.EntityFrameworkCore;
using NetCoreMvcProject.Models;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreMvcProject.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public DbSet<Product> _products
        {
            get
            {
                return dbContext.Set<Product>();
            }
        }
        private readonly ProductDbContext dbContext;

        public ProductRepository(ProductDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Product Get(string name) => _products.First(en => en.Name == name);

        public IEnumerable<Product> GetAll() => _products;
        public Product GetById(int id) => _products.First(en => en.Id == id);

        public void Create(Product product)
        {
            _products.Add(product);
        }
        public void Delete(int id)
        {
            var product = GetById(id);
            _products.Remove(product);
        }

        public IEnumerable<Product> Search(string searchString)
        {
            foreach (var item in _products)
            {
                if (item.ToString().Contains(searchString))
                {
                    yield return item;
                }
            }
        }

    }
}