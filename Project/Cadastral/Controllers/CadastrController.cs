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
    [Authorize]
    public class CadastrController : Controller
    {

        CadastrDAO _cad = new CadastrDAO();

        // GET: Cadastr
        public async Task<ActionResult> Index()
        {
            var cadastras = await _cad.GetCadastras();
            return View(cadastras);
        }

        [Authorize(Roles = "Administration")]
        public async Task<ActionResult> Edit(int id)
        {
            var cadastr =  await _cad.GetCadastrById(id);
            return View(cadastr);
        }

        [HttpPost]
        [Authorize(Roles = "Administration")]
        public async Task<ActionResult> Edit(CadastrViewModel model)
        {
            await _cad.EditCadastr(model);
            return RedirectToAction("Index");
        }

    }
}