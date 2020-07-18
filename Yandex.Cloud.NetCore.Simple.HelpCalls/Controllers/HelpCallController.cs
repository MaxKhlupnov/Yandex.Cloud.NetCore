using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Yandex.Cloud.NetCore.Simple.HelpCalls.Controllers
{
    public class HelpCallController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
