using Entities.Models;
using Repository.DatabaseContext;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class DonorService : IDonorService
    {
        DatabaseContext db;
        public DonorService(DatabaseContext db)
        {
            this.db = db;
        }

        public int Add(VmDonorAdd vmDonor,out int donorHeaderId)
        {
            var isSaved = 0;
            if (vmDonor.BloodDonorHeaderId > 0)
            {
                var data = db.BloodDonors.Find(vmDonor.BloodDonorHeaderId);
                data.Address = vmDonor.Address;
                data.BloodDonorId = vmDonor.BloodDonorId;
                data.BloodDonorName = vmDonor.BloodDonorName;
                data.Bloodgroup = vmDonor.Bloodgroup;
                data.Country = vmDonor.Country;
                data.District = vmDonor.District;
                data.Division = vmDonor.Division;
                data.DOB = Convert.ToDateTime(vmDonor.DOB);
                data.Email = vmDonor.Email;
                data.FbUrl = vmDonor.FbUrl;
                data.Gender = vmDonor.Gender;
                if(vmDonor.ImageUrl != null)
                {
                    data.ImageUrl = vmDonor.ImageUrl;
                }
                data.IsActive = vmDonor.IsActive;
                if(vmDonor.LastDonated != null)
                {
                    data.LastDonated = Convert.ToDateTime(vmDonor.LastDonated);
                }
                data.MobileNo = vmDonor.MobileNo;
                data.Occupation = vmDonor.Occupation;
                data.PhoneNo = vmDonor.PhoneNo;
                data.ReadyForDonate = vmDonor.ReadyForDonate;
                data.Thana = vmDonor.Thana;
                isSaved = db.SaveChanges();
                donorHeaderId = data.BloodDonorHeaderId;
            }
            else
            {
                var data = new BloodDonor();
                data.Address = vmDonor.Address;
                data.BloodDonorId = GenerateDonorId();
                data.BloodDonorName = vmDonor.BloodDonorName;
                data.Bloodgroup = vmDonor.Bloodgroup;
                data.Country = vmDonor.Country;
                data.District = vmDonor.District;
                data.Division = vmDonor.Division;
                data.DOB = Convert.ToDateTime(vmDonor.DOB);
                data.Email = vmDonor.Email;
                data.FbUrl = vmDonor.FbUrl;
                data.Gender = vmDonor.Gender;
                data.ImageUrl = vmDonor.ImageUrl;
                data.IsActive = vmDonor.IsActive;
                if (vmDonor.LastDonated != null)
                {
                    data.LastDonated = Convert.ToDateTime(vmDonor.LastDonated);
                }
                data.MobileNo = vmDonor.MobileNo;
                data.Occupation = vmDonor.Occupation;
                data.PhoneNo = vmDonor.PhoneNo;
                data.ReadyForDonate = vmDonor.ReadyForDonate;
                data.Thana = vmDonor.Thana;
                db.BloodDonors.Add(data);
                isSaved = db.SaveChanges();
                donorHeaderId = data.BloodDonorHeaderId;

            }
            return isSaved;
        }

        public int Delete(int id)
        {

            return db.SaveChanges();
        }

        public IEnumerable<VmDonorList> GetBetween(int start, int displayLength, string searchValue, out int totalLength)
        {
            var district = db.District;
            var division = db.Division;
            var thana = db.Upazila;
            var configValues = db.ConfigValueSets;
            var allValues = db.BloodDonors.AsEnumerable();
            if (!string.IsNullOrEmpty(searchValue))
            {
                allValues = allValues.Where(w => (!string.IsNullOrEmpty(w.Address) && w.Address.ToLower().Contains(searchValue.ToLower())) || (!string.IsNullOrEmpty(w.BloodDonorId) && w.BloodDonorId.ToLower().Contains(searchValue.ToLower())) || (!string.IsNullOrEmpty(w.BloodDonorName) && w.BloodDonorName.ToLower().Contains(searchValue.ToLower())) || (!string.IsNullOrEmpty(w.Email) && w.Email.ToLower().Contains(searchValue.ToLower())) || (!string.IsNullOrEmpty(w.FbUrl) && w.FbUrl.ToLower().Contains(searchValue.ToLower())) || (!string.IsNullOrEmpty(w.MobileNo) && w.MobileNo.ToLower().Contains(searchValue.ToLower())) || (!string.IsNullOrEmpty(w.Occupation) && w.Occupation.ToLower().Contains(searchValue.ToLower())) || (!string.IsNullOrEmpty(w.PhoneNo) && w.PhoneNo.ToLower().Contains(searchValue.ToLower())));
            }

            var displayedValues = displayLength == -1 ? allValues
                .Skip(start).ToList() : allValues
                .Skip(start)
                .Take(displayLength).ToList();
            totalLength = allValues.Count();
            var result = displayedValues.Select(s => new VmDonorList
            {
                Address = s.Address,
                BloodDonorHeaderId = s.BloodDonorHeaderId,
                BloodDonorName = s.BloodDonorName,
                DistrictId = s.District,
                DistrictName = district.Find(s.District).DistrictName,
                DivisionId = s.Division,
                DivisionName = division.Find(s.Division).DivisionName,
                BloodDonorId = s.BloodDonorId,
                BloodgroupId = s.Bloodgroup,
                BloodgroupName = configValues.Find(s.Bloodgroup).ConfigValue,
                DOB = s.DOB.ToString("dd MMM yyyy"),
                Email = s.Email,
                FbUrl = s.FbUrl,
                GenderId = s.Gender,
                GenderName = configValues.Find(s.Gender).ConfigValue,
                ImageUrl = s.ImageUrl,
                IsActive = s.IsActive != null && s.IsActive == false ? "Inactive" : "Active",
                LastDonated = s.LastDonated != null ? s.LastDonated.Value.ToString("dd MMM yyyy") : "",
                MobileNo = s.MobileNo,
                Occupation = s.Occupation,
                PhoneNo = s.PhoneNo,
                ReadyForDonate = s.ReadyForDonate,
                ThanaId = s.Thana,
                ReadyForDonateString = s.ReadyForDonate.ToString(),
                ThanaName = thana.Find(s.Thana).UpazilaName,
            });
            return result;
        }

        public string GenerateDonorId()
        {
            var resultantItemID = "000001";
            var lastItem = db.BloodDonors.OrderByDescending(o => o.BloodDonorHeaderId).FirstOrDefault();
            string lItemID = lastItem == null ? "" : lastItem.BloodDonorId == null ? "" : lastItem.BloodDonorId == "" ? "" : lastItem.BloodDonorId;
            if (!string.IsNullOrEmpty(lItemID))
            {
                int cnt = Convert.ToInt32(lItemID);
                resultantItemID = (cnt + 1).ToString("D6");
            }
            return resultantItemID;
        }
    }
}
