using SMSPanelManager.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPanelManager.Core.Services
{
    public interface IProductService
    {
        public List<Products> GetAllProducts();

        public void DeleteProduct(int productId);

        public void CreateProduct(string ProductName);
    }
}
