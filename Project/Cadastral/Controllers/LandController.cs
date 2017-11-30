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
    public class LandController : Controller
    {
        private CadastraDBEntities _edmx = new CadastraDBEntities();
        private LandDAO _land = new LandDAO();

        // GET: Land
        public async Task<ActionResult> Index()
        {
            var lands = await _land.GetLands();
            return View(lands);
        }

        [HttpGet]
        [Authorize]
        public ActionResult CreateLand()
        {
            InitDynamicViewBag();
            var user = _edmx.AspNetUsers.FirstOrDefault(x => x.UserName == User.Identity.Name);
            var owner = _edmx.Owners.FirstOrDefault(x => x.UserId == user.Id);
            var land = new LandViewModel
            {
                Name = owner.Name,
                Surname = owner.Surname,
                CurrentUserId = user.Id,
                Cadastr = new CadastrViewModel
                {
                    CadastrId = 2,
                    CadastrName = "Land"
                }
            };
            return View(land);
        }

        private void InitDynamicViewBag()
        {
            var landTypes = new SelectList(_edmx.LandTypes.ToList(), "LandTypeId", "Name");
            var owns = _edmx.Owners.Select(x => new OwnerViewModel
            {
                OwnerId = x.OwnerId,
                Name = x.Name,
                Surname = x.Surname
            });
            var owners = new SelectList(owns.ToList(), "OwnerId", "Owner");
            var cadastras = new SelectList(_edmx.Cadastrs.ToList(), "CadastrId", "Name");
            ViewBag.LandTypes = landTypes;
            ViewBag.Owners = owners;
            ViewBag.Cadastras = cadastras;            
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreateLand(LandViewModel model)
        {
            try
            {
                if(model.Owner == null)
                {
                    var user = _edmx.AspNetUsers.FirstOrDefault(x => x.UserName == User.Identity.Name);
                    var owner = _edmx.Owners.FirstOrDefault(x => x.UserId == user.Id);
                    model.Owner = new OwnerViewModel();                    
                    model.Owner.OwnerId = owner.OwnerId;
                    model.Cadastr = new CadastrViewModel();
                    model.Cadastr.CadastrId = 2;
                }
                if (model != null)
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
        [Authorize]
        public async Task<ActionResult> EditLand(int id)
        {
            InitDynamicViewBag();
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
        [Authorize]
        public async Task<ActionResult> EditLand(LandViewModel model)
        {
            try
            {
                if (model.Owner == null)
                {
                    var user = _edmx.AspNetUsers.FirstOrDefault(x => x.UserName == User.Identity.Name);
                    var owner = _edmx.Owners.FirstOrDefault(x => x.UserId == user.Id);
                    model.Owner = new OwnerViewModel();
                    model.Owner.OwnerId = owner.OwnerId;
                    model.Cadastr = new CadastrViewModel();
                    model.Cadastr.CadastrId = 2;
                }
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

        [HttpGet]
        [Authorize(Roles = "Administration,Moderator")]
        public async Task<ActionResult> Delete(int id)
        {
            var land = await _land.GetLandById(id);
            return View(land);
        }

        [HttpPost]
        [Authorize(Roles = "Administration,Moderator")]
        public async Task<ActionResult> Delete(LandViewModel model)
        {
            try
            {
                if (model != null)
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