using SMSPanelManager.Data.Context;
using SMSPanelManager.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPanelManager.Core.Services
{
    public class ContactService : IContactService
    {
        SMSPanelContext _context;
        public ContactService(SMSPanelContext sMSPanelContext)
        {
            _context = sMSPanelContext;
        }
        public void AddContact(Contact contact)
        {
            _context.Contacts.Add(contact);
            _context.SaveChanges();
        }

        public void DeleteContact(int ContactId)
        {
            var PritableSMs = _context.PrinteableSMs.Where(s => s.ContactId == ContactId);
            var Contact = _context.Contacts.FirstOrDefault(c=>c.ContactId == ContactId);
            foreach (var s in PritableSMs)
            {
                s.ContactId = null;
                _context.PrinteableSMs.Update(s);
            }
            _context.Contacts.Remove(Contact);
            _context.SaveChanges();
        }
        public List<Contact> GetAllContacts()
        {
            return _context.Contacts.ToList();
        }
    }
}
