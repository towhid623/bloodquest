using Entities.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Repository.DatabaseContext;
using Repository.Enums;
using Service.Helper;
using Services;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteBack.Models;

namespace WebsiteBack.Controllers
{
    public class DonorController : Controller
    {
        #region variables & constructor
        private DatabaseContext db = new DatabaseContext();
        private ApplicationUserManager _userManager;
        ApplicationDbContext applicationDbContext = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;
        #endregion
        // GET: Donor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            var blood = db.ConfigMasters.FirstOrDefault(s => s.ConfigShortName == "bloodgroup").ConfigId;
            ViewBag.bloodGroups=db.ConfigValueSets.Where(b=>b.ConfigValueSetId==blood).Select(s => new VmSelectList { Id = s.ConfigValueId, Name = s.ConfigValue });

            var gender = db.ConfigMasters.FirstOrDefault(s => s.ConfigShortName == "gender").ConfigId;
            ViewBag.gendertypes = db.ConfigValueSets.Where(g => g.ConfigValueSetId == gender).Select(s => new VmSelectList { Id = s.ConfigValueId, Name = s.ConfigValue });

            ViewBag.DivisionList = db.Division.Select(s => new VmSelectList { Id = s.DivisionHeaderId, Name = s.DivisionName });

            ViewBag.DistrictList = db.District.Select(s => new VmDistrict { DistrictHeaderId = s.DistrictHeaderId, DistrictName = s.DistrictName, DivisionHeaderId = s.DivisionHeaderId });

            ViewBag.ThanaList = db.Upazila.Select(s => new VmUpazilla { ThanaHeaderId = s.UpazilaHeaderId, ThanaName = s.UpazilaName, DistrictHeaderId = s.DistrictHeaderId });
            return View();
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Add(VmDonorAdd model)
        {
            if (ModelState.IsValid)
            {
                #region Image Upload
                var uri = Request.Url.Host;
                System.IO.Directory.CreateDirectory(Server.MapPath("~/Images/ProfilePic/" + uri));
                string path = "";
                if (model.ProPic != null)
                {
                    string pic = System.IO.Path.GetFileName(model.ProPic.FileName);
                    string physicalPath =
                        System.IO.Path.Combine(Server.MapPath("~/Images/ProfilePic/" + uri), pic);
                    path = "/Images/ProfilePic/" + uri + "/" + pic;
                    model.ProPic.SaveAs(physicalPath);
                    model.ImageUrl = path;
                }
                #endregion
                model.DOB = DateTimeHelperService.ConvertBDDateStringToDateTimeObject(model.DOB);
                model.LastDonated = !string.IsNullOrEmpty(model.LastDonated) ?DateTimeHelperService.ConvertBDDateStringToDateTimeObject(model.LastDonated):null;
                var donorService = new DonorService(db);
                int donorHeaderId = 0;
                donorService.Add(model,out donorHeaderId);
                if(model.BloodDonorHeaderId == 0 && model.Password == model.ConfirmPassword)
                {
                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email,UserType = (int)UserType.Donor,ReferrenceId= donorHeaderId,CreationDate=DateTime.Now,LastUpdateDate=DateTime.Now };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                        // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var userid = HttpContext.Request.GetOwinContext().Request.User.Identity.GetUserId();
            var currentUser = applicationDbContext.Users.FirstOrDefault(o => o.Id == userid);
            var Donorid = applicationDbContext.Users.FirstOrDefault(o => o.Id == userid).ReferrenceId;
            var user = db.BloodDonors.FirstOrDefault(o => o.BloodDonorHeaderId == id);

            ViewBag.IsEditable = Donorid == id ? true : false;

            VmDonorDetails obj = new VmDonorDetails();
            obj.BloodDonorHeaderId = id;
            obj.BloodDonorName = user.BloodDonorName;
            obj.Email = user.Email;
            obj.MobileNo = user.MobileNo;
            obj.PhoneNo = user.PhoneNo;
            obj.Gender = db.ConfigValueSets.Find(user.Gender).ConfigValue;
            obj.DOB = user.DOB.ToString("dd MMM yyyy");
            //obj.LastDonated=user.LastDonated.ToString("dd MMM yyyy");
            obj.Division = db.Division.Find(user.Division).DivisionName;
            obj.District = db.District.Find(user.District).DistrictName;
            obj.Thana = db.Upazila.Find(user.Thana).UpazilaName;
            obj.Address = user.Address;
            obj.ReadyForDonate = user.ReadyForDonate == true ? "Ready to Donate" : "Not Ready to Donate";
            obj.FbUrl = user.FbUrl;
            obj.Occupation = user.Occupation;
            obj.ImageUrl = user.ImageUrl;
            obj.Bloodgroup = db.ConfigValueSets.Find(user.Bloodgroup).ConfigValue;
            return View(obj);
        }

        public ActionResult AjaxDonorList(VmJqueryDatatable param)
        {
            var test = db.BloodDonors.Where(w=>w.ReadyForDonate!=false).ToList();
            var flex = db.ConfigValueSets.AsEnumerable();
            var district = db.District.AsEnumerable();
            var division = db.Division.AsEnumerable();
            var result = test.Select(s => new 
            {
                DonorHeaderId = s.BloodDonorHeaderId,
                BloodGroup = flex.FirstOrDefault(f=>f.ConfigValueId == s.Bloodgroup)!=null? flex.FirstOrDefault(f => f.ConfigValueId == s.Bloodgroup).ConfigValue:"",
                Location = (division.FirstOrDefault(f => f.DivisionHeaderId == s.Division) != null ? division.FirstOrDefault(f => f.DivisionHeaderId == s.Division).DivisionName+", " : "")+(district.FirstOrDefault(f => f.DistrictHeaderId == s.District) != null ? district.FirstOrDefault(f => f.DistrictHeaderId == s.District).DistrictName : ""),
                Mobile = s.MobileNo,
                Name = s.BloodDonorName,
                LastDonationDate = s.LastDonated!=null?s.LastDonated.Value.ToString("dd MMM yyyy"):""
            }).ToList();
            return Json(new
            {
                sEcho = param.draw,
                iTotalRecords = result.Count,
                iTotalDisplayRecords = result,
                data = result
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var donorService = new DonorService(db);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BloodDonor donor = db.BloodDonors.Find(id);
           
            if (donor == null)
            {
                return HttpNotFound();
            }
            var blood = db.ConfigMasters.FirstOrDefault(s => s.ConfigShortName == "bloodgroup").ConfigId;
            ViewBag.bloodGroups = db.ConfigValueSets.Where(b => b.ConfigValueSetId == blood).Select(s => new VmSelectList { Id = s.ConfigValueId, Name = s.ConfigValue });

            var gender = db.ConfigMasters.FirstOrDefault(s => s.ConfigShortName == "gender").ConfigId;
            ViewBag.gendertypes = db.ConfigValueSets.Where(g => g.ConfigValueSetId == gender).Select(s => new VmSelectList { Id = s.ConfigValueId, Name = s.ConfigValue });

            ViewBag.DivisionList = db.Division.Select(s => new VmSelectList { Id = s.DivisionHeaderId, Name = s.DivisionName });

            ViewBag.DistrictList = db.District.Select(s => new VmDistrict { DistrictHeaderId = s.DistrictHeaderId, DistrictName = s.DistrictName, DivisionHeaderId = s.DivisionHeaderId });

            ViewBag.ThanaList = db.Upazila.Select(s => new VmUpazilla { ThanaHeaderId = s.UpazilaHeaderId, ThanaName = s.UpazilaName, DistrictHeaderId = s.DistrictHeaderId });
            var VmDonorAdd = new VmDonorAdd();
            VmDonorAdd.BloodDonorHeaderId = id??0;
            VmDonorAdd.BloodDonorName = donor.BloodDonorName;
            VmDonorAdd.Email = donor.Email;
            VmDonorAdd.MobileNo = donor.MobileNo;
            VmDonorAdd.PhoneNo = donor.PhoneNo;
            VmDonorAdd.Bloodgroup = donor.Bloodgroup;
            VmDonorAdd.Gender = donor.Gender;
            VmDonorAdd.Division = donor.Division;
            VmDonorAdd.District = donor.District;
            VmDonorAdd.Thana = donor.Thana;
            VmDonorAdd.DOB = donor.DOB.ToString("dd/MM/yyyy");
            VmDonorAdd.Occupation = donor.Occupation;
            VmDonorAdd.FbUrl = donor.FbUrl;
            VmDonorAdd.LastDonated= donor.LastDonated!=null? donor.LastDonated.Value.ToString("dd/MM/yyyy"):"";
            VmDonorAdd.ReadyForDonate = donor.ReadyForDonate;
            return View(VmDonorAdd);
        }


        #region Essential Functions
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        #endregion
    }
}