using Cadastral.DAO;
using Cadastral.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Cadastral.Controllers
{
    public class OwnerController : Controller
    {

        OwnerDAO _owner = new OwnerDAO();

        public async Task<ActionResult> Index()
        {
            var owners = await _owner.GetOwners();
            return View(owners);
        }

        [HttpGet]
        public ActionResult CreateOwner()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CreateOwner(OwnerViewModel model)
        {
            if (ModelState.IsValid && model != null)
            {
                await _owner.CreateOwner(model);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> EditOwnerData(int id)
        {
            var owner = await _owner.GetOwnerById(id);
            return View(owner);
        }

        [HttpPost]
        public async Task<ActionResult> EditOwnerData(OwnerViewModel model)
        {
            if (ModelState.IsValid && model != null)
            {
                await _owner.EditOwner(model);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var owner = await _owner.GetOwnerById(id);
            return View(owner);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(OwnerViewModel model)
        {
            if (ModelState.IsValid && model != null)
            {
                await _owner.RemoveOwner(model);
            }
            return RedirectToAction("Index");
        }

    }
}