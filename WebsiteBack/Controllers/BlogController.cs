using Microsoft.AspNet.Identity;
using Repository.DatabaseContext;
using Services;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBack.Models;

namespace WebsiteBack.Controllers
{
    public class BlogController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        ApplicationDbContext appDbContext = new ApplicationDbContext();
        // GET: Blog
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(VmBlogAdd model)
        {
            if (ModelState.IsValid)
            {
                var userid = HttpContext.Request.GetOwinContext().Request.User.Identity.GetUserId();
                var loggedInDonorId = appDbContext.Users.FirstOrDefault(f => f.Id == userid).ReferrenceId ?? 0;
                #region Image Upload
                var uri = Request.Url.Host;
                System.IO.Directory.CreateDirectory(Server.MapPath("~/Images/Blog/"+ loggedInDonorId + "/" + uri));
                string path = "";
                if (model.Image != null)
                {
                    string pic = System.IO.Path.GetFileName(model.Image.FileName);
                    string physicalPath =
                        System.IO.Path.Combine(Server.MapPath("~/Images/Blog/" + loggedInDonorId + "/" + uri), pic);
                    path = "/Images/Blog/" + loggedInDonorId + "/" + uri + "/" + pic;
                    model.Image.SaveAs(physicalPath);
                    model.ImageUrl = path;
                }
                #endregion
                BlogService blogService = new BlogService(db);
                int blogId = 0;
                blogService.Add(model, loggedInDonorId, out blogId);
            }
            //return Json(JsonRequestBehavior.AllowGet);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult AjaxBlogList(VmJqueryDatatable param)
        {
            var userid = HttpContext.Request.GetOwinContext().Request.User.Identity.GetUserId();
            var loggedInDonorId = appDbContext.Users.FirstOrDefault(f => f.Id == userid).ReferrenceId ?? 0;
            var test = db.Blog.Where(w=>w.BloodDonorHeaderId==loggedInDonorId).ToList();
            var result = test.Where(p=>p.Attribute1!="feedback"&&p.IsDisabled!=true).Select(s => new
            {
                BlogHeaderId = s.BlogId,
                Date = s.CreationDate!=null?s.CreationDate.Value.ToString("dd MMM yyyy"):"",
                Title = s.Heading,
                Description = s.Details,
                ImageUrl = s.ImageUrl
            }).ToList();
            return Json(new
            {
                sEcho = param.draw,
                iTotalRecords = result.Count,
                iTotalDisplayRecords = result,
                data = result
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Disable(int id)
        {
            var obj = db.Blog.Find(id);
            obj.IsDisabled = true;
            db.SaveChanges();
            return RedirectToAction("Index", "Blog");
        }
    }
}