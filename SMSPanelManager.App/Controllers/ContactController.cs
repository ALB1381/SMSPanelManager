using Microsoft.AspNetCore.Mvc;
using SMSPanelManager.Core.Security;
using SMSPanelManager.Core.Services;
using SMSPanelManager.Data.Entities;

namespace SMSPanelManager.App.Controllers
{
    [PermissionChecker()]
    public class ContactController : Controller
    {

        IContactService _service;
        public ContactController(IContactService contactService)
        {
            _service = contactService;
        }
        public IActionResult Index()
        {
            return View(_service.GetAllContacts());
        }

        
        public IActionResult CreateNewContact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateNewContact(Contact contact)
        {
            _service.AddContact(contact);
            return RedirectToAction("Index");
        }
        public IActionResult DeleteContact(int ContactId)
        {
            _service.DeleteContact(ContactId);
            return RedirectToAction("Index");
        }
    }
}
