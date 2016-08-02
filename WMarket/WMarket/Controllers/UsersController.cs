using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WMarket.Models;
using WMarket.ViewModels;

namespace WMarket.Controllers
{
    public class UsersController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        //
        // GET: /Users/
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            //var users = userManager.UserValidator|.ToList();
            var users = db.Users.ToList();
            var usersView = new List<UserView>();

            foreach (var user in users)
            {
                var userView = new UserView
                {
                    Name = user.UserName,
                    UserID = user.Id
                };

                usersView.Add(userView);
            }

            return View(usersView);
        }

        public ActionResult Roles(string UserId)
        {
            var users = db.Users.ToList();
            var roles = db.Roles.ToList();

            var user = users.Find(u => u.Id == UserId);

            var rolesView = new List<RoleView>();

            foreach (var item in user.Roles)
            {
                var role = roles.Find(r => r.Id == item.RoleId);

                var roleView = new RoleView
                {
                    RoleID = role.Id,
                    RoleName = role.Name
                };

                rolesView.Add(roleView);
            }                        

            var userView = new UserView
            {
                Name = user.UserName,
                UserID = user.Id,
                Roles = rolesView
            };

            return View(userView);
        }

        public ActionResult AddRole(string userID)
        {
            if (string.IsNullOrEmpty(userID))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var users = db.Users.ToList();
            var user = users.Find(u => u.Id == userID);

            if (user == null)
            {
                return HttpNotFound();
            }
                        
            var userView = new UserView
            {
                Name = user.UserName,
                UserID = user.Id
            };

            var list = db.Roles.ToList();
            list.Add(new IdentityRole { Id = "", Name = "[Seleccione un Cliente]" });
            list = list.OrderBy(r => r.Name).ToList();
            ViewBag.RoleID = new SelectList(list, "Id", "Name");

            return View(userView); 
        }

        [HttpPost]
        public ActionResult AddRole(string userID, FormCollection form) 
        {
            var roleId = Request["RoleID"];

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = db.Users.ToList();
            var user = users.Find(u => u.Id == userID);
            var userView = new UserView
            {
                Name = user.UserName,
                UserID = user.Id
            };

            if (string.IsNullOrEmpty(roleId))
            {
                ViewBag.Error = "You Must Select a Role";                

                var list = db.Roles.ToList();
                list.Add(new IdentityRole { Id = "", Name = "[Seleccione un Cliente]" });
                list = list.OrderBy(r => r.Name).ToList();
                ViewBag.RoleID = new SelectList(list, "Id", "Name");
                return View(userView); 
            }

             var roles = db.Roles.ToList();
             var role = roles.Find(r => r.Id == roleId);

            if(!userManager.IsInRole(userID, role.Name))
            {
                userManager.AddToRole(userID, role.Name);
            }

            
            var rolesView = new List<RoleView>();

            foreach (var item in user.Roles)
            {
                role = roles.Find(r => r.Id == item.RoleId);

                var roleView = new RoleView
                {
                    RoleID = role.Id,
                    RoleName = role.Name
                };

                rolesView.Add(roleView);
            }


            userView = new UserView
            {
                Name = user.UserName,
                UserID = user.Id,
                Roles = rolesView
            };
            
            return View("Roles", userView);
        }

        public ActionResult Delete(string userID, string roleID) 
        {
            if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(roleID))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); 
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = db.Users.ToList();
            var roles = db.Roles.ToList();

            var user = users.Find(u => u.Id == userID);
            var role = roles.Find(r => r.Id == roleID);

            if(userManager.IsInRole(userID, role.Name))
            {
                 userManager.RemoveFromRole(userID, role.Name);
            }

            //var users = db.Users.ToList();

            var rolesView = new List<RoleView>();

            foreach (var item in user.Roles)
            {
                 role = roles.Find(r => r.Id == item.RoleId);

                var roleView = new RoleView
                {
                    RoleID = role.Id,
                    RoleName = role.Name
                };

                rolesView.Add(roleView);
            }


            var userView = new UserView
            {
                Name = user.UserName,
                UserID = user.Id,
                Roles = rolesView
            };

            
            return View("Roles", userView); 
        } 

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
	}
}