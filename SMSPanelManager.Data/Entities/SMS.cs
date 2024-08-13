using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPanelManager.Data.Entities
{
    public class SMS
    {
        [Key]
        public int SMSId { get; set; }

        public string Text { get; set; }

        public string Sender { get; set; }

        public string Receiver { get; set; }

        public int ReceivedSmsId { get; set; }

        public int? PrintableSMSId { get; set; }

        [ForeignKey("PrintableSMSId")]
        public PrinteableSMS PrinteableSMS { get; set; }

        public DateTime ReceivedTime { get; set; } = DateTime.Now;
    }
}
