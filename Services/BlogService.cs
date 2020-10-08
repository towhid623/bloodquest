using Entities.Models;
using Repository.DatabaseContext;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Services
{
    public class BlogService: IBlogService
    {
        DatabaseContext db;
        public BlogService(DatabaseContext db)
        {
            this.db = db;
        }

        public int Add(VmBlogAdd vmBlog,int loggedInDonorId, out int blogId)
        {
            var isSaved = 0;
            if (vmBlog.BlogHeaderId > 0)
            {
                var data = db.Blog.Where(w=>w.Attribute1!="feedback"&&w.BlogId== vmBlog.BlogHeaderId).FirstOrDefault();
                data.Heading = vmBlog.BlogTitle;
                data.Details = vmBlog.Description;
                if (vmBlog.ImageUrl != null)
                {
                    data.ImageUrl = vmBlog.ImageUrl;
                }
                if (vmBlog.IsFeedback)
                {
                    data.Attribute1 = "feedback";
                }
                data.BloodDonorHeaderId = loggedInDonorId;
                data.LastUpdateDate = DateTime.Now;
                isSaved = db.SaveChanges();
                blogId = data.BlogId;
            }
            else
            {
                var data = new Blog();
                data.Heading = vmBlog.BlogTitle;
                data.Details = vmBlog.Description;
                data.ImageUrl = vmBlog.ImageUrl;
                data.BloodDonorHeaderId = loggedInDonorId;
                if (vmBlog.IsFeedback)
                {
                    data.Attribute1 = "feedback";
                }
                data.CreationDate = DateTime.Now;
                db.Blog.Add(data);
                isSaved = db.SaveChanges();
                blogId = data.BlogId;
            }
            return isSaved;
        }

        public int Delete(int id)
        {
            return db.SaveChanges();
        }

        public IEnumerable<VmBlogList> GetBetween(int start, int displayLength, string searchValue, out int totalLength)
        {
            var allValues = db.Blog.AsEnumerable();
            if (!string.IsNullOrEmpty(searchValue))
            {
                allValues = allValues.Where(w => (!string.IsNullOrEmpty(w.Heading) && w.Heading.ToLower().Contains(searchValue.ToLower())) || (!string.IsNullOrEmpty(w.Details) && w.Details.ToLower().Contains(searchValue.ToLower())));
            }

            var displayedValues = displayLength == -1 ? allValues
                .Skip(start).ToList() : allValues
                .Skip(start)
                .Take(displayLength).ToList();
            totalLength = allValues.Count();
            var result = displayedValues.Select(s => new VmBlogList
            {
                BlogId = s.BlogId,
                BloodDonorHeaderId = s.BloodDonorHeaderId,
                Heading = s.Heading,
                Details = s.Details,
                ImageUrl = s.ImageUrl
                
            });
            return result;
        }

    }
}
