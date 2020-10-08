using Microsoft.AspNet.Identity;
using Repository.DatabaseContext;
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
    public class HomeController : BaseController
    {
        #region variables & constructor
        private DatabaseContext db = new DatabaseContext();
        #endregion

        // GET: Home
        public ActionResult Index()
        {
            ViewBag.LoggedInUserId = HttpContext.Request.GetOwinContext().Request.User.Identity.GetUserId();
            ViewBag.BlogList = db.Blog.Where(o=>o.Attribute1!="feedback" && o.IsDisabled!=true).ToList();
            ViewBag.FeedbackList=db.Blog.Where(o => o.Attribute1 == "feedback" && o.IsDisabled != true).ToList();
            var bloodDonor = db.BloodDonors.ToList();
            ViewBag.NumberOfDonor = bloodDonor.Count;
            ViewBag.NumberOfActiveDonor = bloodDonor.Where(w=>w.ReadyForDonate!=false).Any()? bloodDonor.Where(w => w.ReadyForDonate != false).Count():0;
            ViewBag.NumberOfBlog = db.Blog.Where(w=>w.Attribute1!="feedback" && w.IsDisabled != true).Count();
            ViewBag.NumberOfFeedback = db.Blog.Where(w => w.Attribute1 == "feedback"&&w.IsDisabled!=true).Count();
            var configValues = db.ConfigValueSets.ToList();
            var donorList = db.BloodDonors.ToList();
            var bloodGroupSetId = db.ConfigMasters.FirstOrDefault(f => f.ConfigShortName == "bloodgroup") != null ? db.ConfigMasters.FirstOrDefault(f => f.ConfigShortName == "bloodgroup").ConfigId : 0;
            var bloodGroupList = configValues.Where(w => w.ConfigValueSetId == bloodGroupSetId).ToList();

            if (bloodGroupList.Any())
            {
                //get every Blood group Id
                int A_positive = bloodGroupList.FirstOrDefault(w => w.ConfigShortValue == "A+").ConfigValueId;
                int A_negative = bloodGroupList.FirstOrDefault(w => w.ConfigShortValue == "A-").ConfigValueId;
                int B_positive = bloodGroupList.FirstOrDefault(w => w.ConfigShortValue == "B+").ConfigValueId;
                int B_negative = bloodGroupList.FirstOrDefault(w => w.ConfigShortValue == "B-").ConfigValueId;
                int AB_positive = bloodGroupList.FirstOrDefault(w => w.ConfigShortValue == "AB+").ConfigValueId;
                int AB_negative = bloodGroupList.FirstOrDefault(w => w.ConfigShortValue == "AB-").ConfigValueId;
                int O_positive = bloodGroupList.FirstOrDefault(w => w.ConfigShortValue == "O+").ConfigValueId;
                int O_negative = bloodGroupList.FirstOrDefault(w => w.ConfigShortValue == "O-").ConfigValueId;

                //count blood group wise donor quantity
                string A_positiveDonorCount = bloodDonor.Where(w => w.Bloodgroup == A_positive).ToList().Count.ToString();
                string A_negativeDonorCount = bloodDonor.Where(w => w.Bloodgroup == A_negative).ToList().Count.ToString();
                string B_positiveDonorCount = bloodDonor.Where(w => w.Bloodgroup == B_positive).ToList().Count.ToString();
                string B_negativeDonorCount = bloodDonor.Where(w => w.Bloodgroup == B_negative).ToList().Count.ToString();
                string AB_positiveDonorCount = bloodDonor.Where(w => w.Bloodgroup == AB_positive).ToList().Count.ToString();
                string AB_negativeDonorCount = bloodDonor.Where(w => w.Bloodgroup == AB_negative).ToList().Count.ToString();
                string O_positiveDonorCount = bloodDonor.Where(w => w.Bloodgroup == O_positive).ToList().Count.ToString();
                string O_negativeDonorCount = bloodDonor.Where(w => w.Bloodgroup == O_negative).ToList().Count.ToString();

                var arrPieData = new List<string>();
                var arrPieLabel = new List<string>();


                if (A_positiveDonorCount != "0")
                {
                    arrPieData.Add(A_positiveDonorCount);
                    arrPieLabel.Add("A+");
                }
                if (A_negativeDonorCount != "0")
                {
                    arrPieData.Add(A_negativeDonorCount);
                    arrPieLabel.Add("A-");
                }
                if (B_positiveDonorCount != "0")
                {
                    arrPieData.Add(B_positiveDonorCount);
                    arrPieLabel.Add("B+");
                }
                if (B_negativeDonorCount != "0")
                {
                    arrPieData.Add(B_negativeDonorCount);
                    arrPieLabel.Add("B-");
                }
                if (AB_positiveDonorCount != "0")
                {
                    arrPieData.Add(AB_positiveDonorCount);
                    arrPieLabel.Add("AB+");
                }
                if (AB_negativeDonorCount != "0")
                {
                    arrPieData.Add(AB_negativeDonorCount);
                    arrPieLabel.Add("AB-");
                }
                if (O_positiveDonorCount != "0")
                {
                    arrPieData.Add(O_positiveDonorCount);
                    arrPieLabel.Add("O+");
                }
                if (O_negativeDonorCount != "0")
                {
                    arrPieData.Add(O_negativeDonorCount);
                    arrPieLabel.Add("O-");
                }

                ViewBag.PieChartData = arrPieData;
                ViewBag.PieChartLabels = arrPieLabel;
            }

            //64 districts
            var districts = db.District.ToList();
            List<VmDistrictWiseBloodDonor> districtWiseBloodDonorList = new List<VmDistrictWiseBloodDonor>();

            //List<string> districtNames = new List<string>();
            //List<string> districtCount = new List<string>();

            if (districts.Any())
            {
                foreach (var item in districts)
                {
                    VmDistrictWiseBloodDonor vmDistrictWiseBloodDonor = new VmDistrictWiseBloodDonor();

                    string countQuantity = bloodDonor.Where(w => w.District == item.DistrictHeaderId) != null ? bloodDonor.Where(w => w.District == item.DistrictHeaderId).Count().ToString() : "";
                    if (countQuantity != "0")
                    {
                        string districtName = item.DistrictName;
                        vmDistrictWiseBloodDonor.DistrictName = districtName;
                        vmDistrictWiseBloodDonor.DistrictCount = countQuantity;
                        districtWiseBloodDonorList.Add(vmDistrictWiseBloodDonor);
                    }
                }

                ViewBag.DistrictWiseBloodDonor = districtWiseBloodDonorList;

                //foreach (var item in districts)
                //{
                //    string countQuantity = bloodDonor.Where(w => w.District == item.DistrictHeaderId) != null ? bloodDonor.Where(w => w.District == item.DistrictHeaderId).Count().ToString() : "";
                //    if (countQuantity != "0")
                //    {
                //        string districtName = item.DistrictName;
                //        districtCount.Add(countQuantity);
                //        districtNames.Add(districtName);
                //    }
                //}
                //ViewBag.DistrictCount = districtCount;
                //ViewBag.DistrictName = districtNames;
            }
            return View(donorList);
        }

        public ActionResult BlogDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //ViewBag.BlogList = db.Blog.Where(a => a.Attribute1 != "feedback" && a.IsDisabled != true).ToList();
            //var bloodDonor = db.BloodDonors.ToList();
            var obj = db.Blog.Find(id);
            ViewBag.Donor = db.BloodDonors.Find(obj.BloodDonorHeaderId).BloodDonorName;
            return View(obj);
        }


        //public ActionResult ChartJsData()
        //{
        //    var bloodDonor = db.BloodDonors.ToList();
        //    var configValues = db.ConfigValueSets.ToList();

        //    var bloodGroupSetId = db.ConfigMasters.FirstOrDefault(f=>f.ConfigShortName == "bloodgroup") !=null ? db.ConfigMasters.FirstOrDefault(f => f.ConfigShortName == "bloodgroup").ConfigId : 0;
        //    var bloodGroupList = configValues.Where(w=>w.ConfigValueSetId == bloodGroupSetId).ToList();

        //    if (bloodGroupList.Any())
        //    {
        //        //get every Blood group Id
        //        int A_positive = bloodGroupList.FirstOrDefault(w=>w.ConfigShortValue == "A+").ConfigValueId;
        //        int A_negative = bloodGroupList.FirstOrDefault(w=>w.ConfigShortValue == "A-").ConfigValueId;
        //        int B_positive = bloodGroupList.FirstOrDefault(w=>w.ConfigShortValue == "B+").ConfigValueId;
        //        int B_negative = bloodGroupList.FirstOrDefault(w=>w.ConfigShortValue == "B-").ConfigValueId;
        //        int AB_positive = bloodGroupList.FirstOrDefault(w=>w.ConfigShortValue == "AB+").ConfigValueId;
        //        int AB_negative = bloodGroupList.FirstOrDefault(w=>w.ConfigShortValue == "AB-").ConfigValueId;
        //        int O_positive = bloodGroupList.FirstOrDefault(w=>w.ConfigShortValue == "O+").ConfigValueId;
        //        int O_negative = bloodGroupList.FirstOrDefault(w=>w.ConfigShortValue == "O-").ConfigValueId;

        //        //count blood group wise donor quantity
        //        string A_positiveDonorCount = bloodDonor.Where(w=>w.Bloodgroup == A_positive).ToList().Count.ToString();
        //        string A_negativeDonorCount = bloodDonor.Where(w=>w.Bloodgroup == A_negative).ToList().Count.ToString();
        //        string B_positiveDonorCount = bloodDonor.Where(w=>w.Bloodgroup == B_positive).ToList().Count.ToString();
        //        string B_negativeDonorCount = bloodDonor.Where(w=>w.Bloodgroup == B_negative).ToList().Count.ToString();
        //        string AB_positiveDonorCount = bloodDonor.Where(w=>w.Bloodgroup == AB_positive).ToList().Count.ToString();
        //        string AB_negativeDonorCount = bloodDonor.Where(w=>w.Bloodgroup == AB_negative).ToList().Count.ToString();
        //        string O_positiveDonorCount = bloodDonor.Where(w=>w.Bloodgroup == O_positive).ToList().Count.ToString();
        //        string O_negativeDonorCount = bloodDonor.Where(w=>w.Bloodgroup == O_negative).ToList().Count.ToString();
                
        //        var arrPieData = new List<string>();
        //        arrPieData.Add(A_positiveDonorCount);
        //        arrPieData.Add(A_negativeDonorCount);
        //        arrPieData.Add(B_positiveDonorCount);
        //        arrPieData.Add(B_negativeDonorCount);
        //        arrPieData.Add(AB_positiveDonorCount);
        //        arrPieData.Add(AB_negativeDonorCount);
        //        arrPieData.Add(O_positiveDonorCount);
        //        arrPieData.Add(O_negativeDonorCount);

        //        ViewBag.PieChartData = arrPieData;
        //    }

        //    return Json("", JsonRequestBehavior.AllowGet);
        //}
    }
}