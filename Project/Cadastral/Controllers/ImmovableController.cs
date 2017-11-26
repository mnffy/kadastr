using Cadastral.DAO;
using Cadastral.DataModel;
using Cadastral.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Cadastral.Controllers
{
    public class ImmovableController : Controller
    {
        ImmovableDAO _im = new ImmovableDAO();
        private CadastraDBEntities _edmx = new CadastraDBEntities();
        // GET: Immovable
        public async Task<ActionResult> Index()
        {
            var immovables = await _im.GetImmovables();
            return View(immovables);
        }

        [HttpGet]
        public ActionResult Create()
        {
            InitDynamicViewBag();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(ImmovableViewModel model)
        {
            if (ModelState.IsValid && model != null)
            {
                await _im.CreateImmovable(model);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            InitDynamicViewBag();
            var im = await _im.GetImmovableById(id);
            return View(im);
        }

        private void InitDynamicViewBag()
        {
            var immovables = new SelectList(_edmx.ImmovableTypes.ToList(), "ImmovableTypeId", "ImmovableName");
            var owns = _edmx.Owners.Select(x => new OwnerViewModel
            {
                OwnerId = x.OwnerId,
                Name = x.Name,
                Surname = x.Surname
            });
            var owners = new SelectList(owns.ToList(), "OwnerId", "Owner");
            var cadastras = new SelectList(_edmx.Cadastrs.ToList(), "CadastrId", "Name");
            ViewBag.ImmovableTypes = immovables;
            ViewBag.Owners = owners;
            ViewBag.Cadastras = cadastras;
        }


        [HttpPost]
        public async Task<ActionResult> Edit(ImmovableViewModel model)
        {
            if (ModelState.IsValid && model != null)
            {
                await _im.EditImmovable(model);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var im = await _im.GetImmovableById(id);
            return View(im);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(ImmovableViewModel model)
        {
            try
            {
                if (ModelState.IsValid && model != null)
                    await _im.RemoveImmovable(model.ImmovableId);
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