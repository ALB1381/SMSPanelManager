using SMSPanelManager.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPanelManager.Core.Services
{
    public interface ISMSService
    {
        public void AddNewSMS(SMS sMS);

        public List<PrinteableSMS> GetSMSList();

        public Tuple<List<OrderedProducts>, List<PrinteableSMS>> GetSMSsToPrint();

        public void DeleteSMSs(string Type);

        public void MakeASMSPrinted(int Id);
        public Tuple<List<OrderedProducts>, PrinteableSMS> GetSmsToPrint(int SmsiD);



    }
}
