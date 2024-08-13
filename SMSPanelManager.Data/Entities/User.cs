using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPanelManager.Data.Entities
{
    public class User
    {
        [Key]
        [Column(TypeName = "TINYINT")]
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string UserPassword { get; set; }
    }
}
