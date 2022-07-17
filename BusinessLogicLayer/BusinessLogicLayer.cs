using Common.Interfaces;
using Common.Model;
using System.Collections.Generic;

namespace BusinessLogicLayer
{
    public class BusinessLogicLayer : IBusinessLogicLayer
    {
        private readonly IDataAccessLayer dataAccessLayer;

        public BusinessLogicLayer(IDataAccessLayer dataAccessLayer)
        {
            this.dataAccessLayer = dataAccessLayer;
        }

        public Product AddProduct(Product product)
        {
            return this.dataAccessLayer.AddProduct(product);
        }

        public Product EditProduct(Product product)
        {
            return this.dataAccessLayer.EditProduct(product);
        }

        public Product GetProduct(string ean)
        {
            return this.dataAccessLayer.GetProduct(ean);
        }

        public ICollection<Product> GetProducts()
        {
            return this.dataAccessLayer.GetProducts();
        }

        public void RemoveProduct(string ean)
        {
            this.dataAccessLayer.RemoveProduct(ean);
        }
    }
}
