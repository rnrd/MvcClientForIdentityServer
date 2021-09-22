using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcClientForIdentityServer.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MvcClientForIdentityServer.Controllers
{
    public class HomeController : Controller
    {
    

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public  IActionResult Secret()
        {
            
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            
            await HttpContext.SignOutAsync("Cookie");
            await HttpContext.SignOutAsync("oidc");
            return Redirect("https://localhost:46834/account/logout");
        }

        

        
    }
}
