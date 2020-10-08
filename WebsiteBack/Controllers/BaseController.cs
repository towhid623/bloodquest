using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Repository.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBack.Models;

namespace WebsiteBack.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var appDbContext = new ApplicationDbContext();
            var db = new DatabaseContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(appDbContext));
            ViewBag.Donorid = userManager.FindById(User.Identity.GetUserId()) != null ? userManager.FindById(User.Identity.GetUserId()).ReferrenceId : 0;
            if (ViewBag.Donorid > 0)
            {
                ViewBag.UserName = db.BloodDonors.Find(ViewBag.Donorid).BloodDonorName;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}