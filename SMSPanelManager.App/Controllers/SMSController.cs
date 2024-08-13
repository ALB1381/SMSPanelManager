using Microsoft.AspNetCore.Mvc;
using SMSPanelManager.Core.Security;
using SMSPanelManager.Core.Services;
using SMSPanelManager.Data.Entities;

namespace SMSPanelManager.App.Controllers
{
    [PermissionChecker()]
    public class SMSController : Controller
    {
        ISMSService _service;
		public SMSController(ISMSService service)
		{
			_service = service;
		}

		[Route("SMSPanel/SMSList")]
        public IActionResult Index()
        {
            var smss = _service.GetSMSList();
            return View(smss);
        }

        public IActionResult Print()
        {
                 
                var res = _service.GetSMSsToPrint();
                return View(res);
            
        }
        public IActionResult SinglePrint(int Id)
        {

           var res = _service.GetSmsToPrint(Id);
            _service.MakeASMSPrinted(Id);
            return View(res);

        }

        public IActionResult DeleteSMSs(string Type)
        {
            _service.DeleteSMSs(Type);
            return Redirect("/Home/Index");
        }
    }
}
