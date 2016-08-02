using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using WMarket.Models;

namespace WMarket
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<WMarketContext, Migrations.Configuration>());
            
            ApplicationDbContext db = new ApplicationDbContext();            
            CreateRoles(db);
            CreateSuperuser(db);
            AddPermisionsToSuperuser(db);
            db.Dispose();
            
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void AddPermisionsToSuperuser(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            
            var user = userManager.FindByName("Vmonsalve");

            if (!userManager.IsInRole(user.Id, "View"))
            {
                userManager.AddToRole(user.Id, "View");
            }
            //
            if (!userManager.IsInRole(user.Id, "Edit"))
            {
                userManager.AddToRole(user.Id, "Edit");
            }
            //
            if (!userManager.IsInRole(user.Id, "Create"))
            {
                userManager.AddToRole(user.Id, "Create");
            }
            //
            if (!userManager.IsInRole(user.Id, "Delete"))
            {
                userManager.AddToRole(user.Id, "Delete");
            }
            //
            if (!userManager.IsInRole(user.Id, "Admin"))
            {
                userManager.AddToRole(user.Id, "Admin");
            }
            //
            if (!userManager.IsInRole(user.Id, "Orders"))
            {
                userManager.AddToRole(user.Id, "Orders");
            }
            //
            if (!userManager.IsInRole(user.Id, "Inventories"))
            {
                userManager.AddToRole(user.Id, "Inventories");
            }
            //
            if (!userManager.IsInRole(user.Id, "Shoppings"))
            {
                userManager.AddToRole(user.Id, "Shoppings");
            }
            //
            if (!userManager.IsInRole(user.Id, "Sales"))
            {
                userManager.AddToRole(user.Id, "Sales");
            }
            //
            if (!userManager.IsInRole(user.Id, "Categories"))
            {
                userManager.AddToRole(user.Id, "Categories");
            }

        }

        private void CreateSuperuser(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var user = userManager.FindByName("Vmonsalve");

            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "Vmonsalve"
                };
                userManager.Create(user, "Vane2015");
            }
        }

        private void CreateRoles(ApplicationDbContext db)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            if (!roleManager.RoleExists("View")) 
            {
                roleManager.Create(new IdentityRole("View"));
            }
            //                        
            if (!roleManager.RoleExists("Edit"))
            {
                roleManager.Create(new IdentityRole("Edit"));
            }
            //            
            if (!roleManager.RoleExists("Create"))
            {
                roleManager.Create(new IdentityRole("Create"));
            }
            //
            if (!roleManager.RoleExists("Delete"))
            {
                roleManager.Create(new IdentityRole("Delete"));
            }
            //
            if (!roleManager.RoleExists("Admin"))
            {
                roleManager.Create(new IdentityRole("Admin"));
            }
            //
            if (!roleManager.RoleExists("Orders"))
            {
                roleManager.Create(new IdentityRole("Orders"));
            }
            //
            if (!roleManager.RoleExists("Inventories"))
            {
                roleManager.Create(new IdentityRole("Inventories"));
            }
            //
            if (!roleManager.RoleExists("Shoppings"))
            {
                roleManager.Create(new IdentityRole("Shoppings"));
            }
            //
            if (!roleManager.RoleExists("Sales"))
            {
                roleManager.Create(new IdentityRole("Sales"));
            }
            //
            if (!roleManager.RoleExists("Categories"))
            {
                roleManager.Create(new IdentityRole("Categories"));
            }
        }
    }
}
