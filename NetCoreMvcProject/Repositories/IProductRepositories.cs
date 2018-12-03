using NetCoreMvcProject.Models;
using System.Collections.Generic;

namespace NetCoreMvcProject.Repositories
{
    public interface IProductRepository
    {
        Product GetById(int id);
        Product Get(string name);
        IEnumerable<Product> GetAll();
        IEnumerable<Product> Search(string searchString);
        void Create(Product product);
        void Delete(int id);
    }
}