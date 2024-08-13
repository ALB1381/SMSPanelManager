using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SMSPanelManager.App.Models;
using SMSPanelManager.Core.DTO;
using SMSPanelManager.Core.Security;
using SMSPanelManager.Core.Services;
using SMSPanelManager.Data.Entities;
using System.Diagnostics;
using System.Security.Claims;

namespace SMSPanelManager.App.Controllers
{

    public class HomeController : Controller
    {
        ISMSService _SMSservice;
        IUserService _UserService;
        public HomeController(ISMSService sMSService,IUserService userService)
        {
            _SMSservice = sMSService;
            _UserService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(LoginDTOs login)
        {
            if (login == null)
            {
                return Redirect("NotFound");
            }
            else
            {
                if (ModelState.IsValid)
                {
                  
					var user = _UserService.LoginUser(login);
                    if (user == null)
                    {
						return Redirect("NotFound");
					}
                    else
                    {
                        var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                        new Claim(ClaimTypes.Name,user.UserName.ToString())
                    };
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var princopal = new ClaimsPrincipal(identity);
                        var Properties = new AuthenticationProperties()
                        {
                            AllowRefresh = true,
                            IsPersistent = true
                        };
                        HttpContext.SignInAsync(princopal, Properties);
                        return Redirect("/SMSPanel/SMSList");
                    }
                }
                else
                {
					return Redirect("NotFound");
				}
            }
        }

     

        [Route("NotFound")]
        public IActionResult NotFound()
        {
            return View();
        }

        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("Home/Index");
        }

        [PermissionChecker()]
        [Route("EditProfile")]
        public IActionResult ChegeUserNameAndPassword()
        {
            return View();
        }
        [PermissionChecker()]
        [Route("EditProfile")]
        [HttpPost]
        public IActionResult ChegeUserNameAndPassword(EditUserDTO editUserDTO)
        {
            if (!ModelState.IsValid || editUserDTO.NewPassword != editUserDTO.ReNewPassword)
            {
                return Redirect("SMSPanel/SMSList");
            }
            else
            {
              var res =  _UserService.UpdateUser(editUserDTO);
                if (res)
                {
                    return Redirect("Logout");
                }
                else
                {
                    return View();
                }
            }
         
        }
    }
}
