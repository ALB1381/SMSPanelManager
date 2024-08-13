using SMSPanelManager.Core.DTO;
using SMSPanelManager.Core.Security;
using SMSPanelManager.Data.Context;
using SMSPanelManager.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSPanelManager.Core.Services
{
    public class UserService : IUserService
    {
        SMSPanelContext _context;

        public UserService(SMSPanelContext context)
        {
            _context = context;
        }

        public User? LoginUser(LoginDTOs? login)
        {
            if (login.UserName == null || login == null)
            {
                return null;
            }
            else
            {
                string HashedPassword = PasswordHelper.EncodePasswordMd5(login.Password);
                var User = _context.Users.FirstOrDefault(c=>c.UserPassword== HashedPassword && c.UserName == login.UserName);
                if (User == null)
                {
                    return null;
                }
                return User;
            }
       

        }

        public bool UpdateUser(EditUserDTO editUserDTO)
        {
            string HashedPassword = PasswordHelper.EncodePasswordMd5(editUserDTO.OldPassword);
            var user = _context.Users.FirstOrDefault(c=> c.UserPassword == HashedPassword);
            if (user == null) { return false; }
            string newhashedpassword = PasswordHelper.EncodePasswordMd5(editUserDTO.NewPassword);
            user.UserPassword = newhashedpassword;
            user.UserName = editUserDTO.UserName;
            _context.Users.Update(user);
            _context.SaveChanges();
            return true;

        }
    }
}
