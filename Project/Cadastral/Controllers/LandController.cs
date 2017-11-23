using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cadastral.Controllers
{
    public class LandController : Controller
    {
        private LandDAO _land = new LandDAO();

        // GET: Land
        public ActionResult Index()
        {
            var lands = _land.GetLands();
            return View(lands);
        }

        [HttpGet]
        public ActionResult CreateLand()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateLand(LandViewModel model)
        {
            try
            {
                if (model != null && ModelState.IsValid)
                    await _land.CreateLand(model);
                else
                    throw new Exception();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> EditLand(int id)
        {
            LandViewModel land = new LandViewModel();
            try
            {
                land = await _land.GetLandById(id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
            }
            return View(land);
        }

        [HttpPost]
        public async Task<ActionResult> EditLand(LandViewModel model)
        {
            try
            {
                if (model != null && ModelState.IsValid)
                    await _land.EditLand(model);
                else
                    throw new Exception();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(LandViewModel model)
        {
            try
            {
                if (ModelState.IsValid && model != null)
                    await _land.RemoveLand(model);
                else
                    throw new Exception();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
            }
            return RedirectToAction("Index");
        }
    }
}