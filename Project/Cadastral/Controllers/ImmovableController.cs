using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cadastral.Controllers
{
    public class ImmovableController : Controller
    {
        // GET: Immovable
        public ActionResult Index()
        {
            return View();
        }
    }
}