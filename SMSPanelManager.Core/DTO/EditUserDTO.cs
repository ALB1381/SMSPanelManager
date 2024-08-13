using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPanelManager.Core.DTO
{
    public class EditUserDTO
    {
        public string UserName { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string ReNewPassword { get; set; }
    }
}
