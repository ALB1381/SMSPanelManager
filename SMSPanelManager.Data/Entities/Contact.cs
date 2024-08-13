using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPanelManager.Data.Entities
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public List<PrinteableSMS> PrinteableSMs { get; set; }
    }
}
