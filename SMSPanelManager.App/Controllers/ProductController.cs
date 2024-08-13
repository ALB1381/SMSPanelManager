using Microsoft.AspNetCore.Mvc;
using SMSPanelManager.Core.Security;
using SMSPanelManager.Core.Services;

namespace SMSPanelManager.App.Controllers
{
    [PermissionChecker()]
    public class ProductController : Controller
    {
        IProductService _srvice;

        public ProductController(IProductService srvice)
        {
            _srvice = srvice;
        }

        public IActionResult Index()
        {

            return View(_srvice.GetAllProducts());
        }
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(string ProductName)
        {
            _srvice.CreateProduct(ProductName);
            return RedirectToAction("Index");
        }
        public IActionResult DeleteProduct(int ProductId)
        {
            _srvice.DeleteProduct(ProductId);
            return RedirectToAction("Index");   

        }

    }
}
