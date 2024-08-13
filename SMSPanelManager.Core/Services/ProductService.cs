using SMSPanelManager.Data.Context;
using SMSPanelManager.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPanelManager.Core.Services
{
    public class ProductService : IProductService
    {
        SMSPanelContext _cotext;

        public ProductService(SMSPanelContext cotext)
        {
            _cotext = cotext;
        }

        public void CreateProduct(string ProductName)
        {
            _cotext.Products.Add(new Products()
            {
                ProductName = ProductName
            });
            _cotext.SaveChanges();
        }

        public void DeleteProduct(int productId)
        {
            var product = _cotext.Products.FirstOrDefault(x => x.ProductId == productId);
            if (product != null)
            {
                _cotext.Products.Remove(product);
                _cotext.SaveChanges();
            }

        }

        public List<Products> GetAllProducts()
        {
            return _cotext.Products.ToList();
        }
    }
}
