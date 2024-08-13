using SMSPanelManager.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPanelManager.Core.Services
{
    public interface IContactService
    {
        public void AddContact(Contact contact);

        public List<Contact> GetAllContacts();

        public void DeleteContact(int ContactId);
    }
}
