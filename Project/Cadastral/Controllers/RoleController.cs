using Cadastral.DataModel;
using Cadastral.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cadastral.Controllers
{
    [Authorize(Roles = "Administration")]
    public class RoleController : Controller
    {
        private CadastraDBEntities _edm = new CadastraDBEntities();

        public RoleController()
        {
        }

        [Authorize(Roles = "Administration")]
        public ActionResult Index()
        {
            var users = GetUsers();
            return View(users);
        }

        private List<RegisterViewModel> GetUsers()
        {
            var users = (from user in _edm.AspNetUsers
                         join owner in _edm.Owners on user.Id equals owner.UserId
                         select new RegisterViewModel
                         {
                             UserId = user.Id,
                             Email = user.Email,
                             Owner = new OwnerViewModel
                             {
                                 Name = owner.Name,
                                 Surname = owner.Surname,
                                 BirthDate = owner.DateBirth
                             },
                             Role = new RoleViewModel
                             {
                                 Id = user.AspNetRoles.FirstOrDefault().Id,
                                 RoleName = user.AspNetRoles.FirstOrDefault().Name
                             }
                         }).ToList();
            return users;
        }

        [HttpGet]
        [Authorize(Roles = "Administration")]
        public ActionResult ChangeUserRole(string userId)
        {
            var reg = GetUsers().FirstOrDefault(x => x.UserId == userId);
            var roles = new SelectList(_edm.AspNetRoles.ToList(), "Id", "Name");
            ViewBag.Roles = roles;
            return View(reg);
        }

    }
}