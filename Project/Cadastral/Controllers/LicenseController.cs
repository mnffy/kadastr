using Cadastral.DAO;
using Cadastral.DataModel;
using Cadastral.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cadastral.Controllers
{
    [Authorize]
    public class LicenseController : Controller
    {
        private CadastraDBEntities _edmx = new CadastraDBEntities();
        LicenseDAO _license = new LicenseDAO();

        public ActionResult Index()
        {
            var licenses = _license.GetAllRequests();
            return View(licenses);
        }

        [HttpGet]
        [Authorize]
        public ActionResult EditLicense(int id)
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
            var license = _license.GetLicenseById(id);
            return View(license);
        }

        [Authorize]
        public ActionResult EditLicense(LicenseRequestModel model)
        {
            if (ModelState.IsValid && model != null)
            {
                _license.EditLicenseData(model);
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administration, Moderator")]
        public ActionResult Accept(int id)
        {
            _license.Accept(id);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administration, Moderator")]
        public ActionResult Reject(int id)
        {
            _license.Reject(id);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administration, Moderator")]
        public ActionResult SentToRevision(int id)
        {
            _license.SentToRevision(id);
            return RedirectToAction("Index");
        }

    }
}