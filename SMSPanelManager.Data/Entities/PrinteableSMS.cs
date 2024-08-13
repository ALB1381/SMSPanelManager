using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPanelManager.Data.Entities
{
    public class PrinteableSMS
    {
        [Key]
        public int Id { get; set; }

        public string SMSText { get; set; }

        public string PhoneNumber { get; set; }

        public int? ContactId { get; set; }

        public int SMSId { get; set; }

        public DateTime CreatedTime { get; set; } = DateTime.Now;

        public bool IsPrinted { get; set; }

        [ForeignKey("SMSId")]
        public SMS SMS { get; set; }

        [ForeignKey("ContactId")]
        public Contact? Contact { get; set; }

        public List<OrderedProducts> OrderedProducts { get; set; }

    }
}
