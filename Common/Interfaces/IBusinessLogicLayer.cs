using Common.Model;
using System.Collections.Generic;

namespace Common.Interfaces
{
    public interface IBusinessLogicLayer
    {
        Product AddProduct(Product product);
        Product EditProduct(Product product);
        Product GetProduct(string ean);
        ICollection<Product> GetProducts();
        void RemoveProduct(string ean);
    }
}
