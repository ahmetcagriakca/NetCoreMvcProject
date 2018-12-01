using FirstNetCoreMvcProject.Models;
using System.Collections.Generic;
using System.Linq;

namespace FirstNetCoreMvcProject.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private static readonly ISet<Product> _products = new HashSet<Product>()

        {
            new Product() { Id = 1 ,Name = "test", Image = "Image"},
            new Product() { Id = 2 ,Name = "tes", Image = "Image"},
            new Product() { Id = 3 ,Name = "tez", Image = "Image"},
        };

        public Product Get(string name) => _products.First(en => en.Name == name);

        public IEnumerable<Product> GetAll() => _products;
        public Product GetById(int id) => _products.First(en => en.Id == id);

        public void Create(Product product)
        {
            var maxId=_products.Max(en => en.Id);
            product.Id = maxId + 1;
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