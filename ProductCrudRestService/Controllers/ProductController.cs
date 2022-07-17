using Common.Interfaces;
using Common.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ProductCrudRestService.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController : Controller
    {
        private readonly IBusinessLogicLayer businessLogicLayer;

        public ProductController(IBusinessLogicLayer businessLogicLayer)
        {
            this.businessLogicLayer = businessLogicLayer;
        }

        [HttpPost()]
        public Product AddProduct(Product product)
        {
            return this.businessLogicLayer.AddProduct(product);
        }

        [HttpPut("{ean}")]
        [HttpPatch("{ean}")]
        public Product EditProduct(string ean, Product product)
        {
            product.Ean = ean;
            return this.businessLogicLayer.EditProduct(product);
        }

        [HttpGet("{ean}")]
        public Product GetProduct(string ean)
        {
            return this.businessLogicLayer.GetProduct(ean);
        }

        [HttpGet()]
        public ICollection<Product> GetProducts()
        {
            return this.businessLogicLayer.GetProducts();
        }

        [HttpDelete("{ean}")]
        public void RemoveProduct(string ean)
        {
            this.businessLogicLayer.RemoveProduct(ean);
        }
    }
}
