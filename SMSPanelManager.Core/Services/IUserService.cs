using SMSPanelManager.Core.DTO;
using SMSPanelManager.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPanelManager.Core.Services
{
    public interface IUserService
    {
        public User LoginUser(LoginDTOs login);

        public bool UpdateUser(EditUserDTO editUserDTO);
    }
}
