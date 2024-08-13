using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPanelManager.Data.Entities
{
    public class OrderedProducts
    {
        public int Id { get; set; }

        public string OrderedProduct { get; set; }

        public int Count { get; set; }

        public int PrintableSMSId { get; set; }


        [ForeignKey("PrintableSMSId")]
        public PrinteableSMS PrinteableSMS { get; set; }


    }
}
