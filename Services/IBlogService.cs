using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IBlogService
    {
        int Add(VmBlogAdd vmBlogAdd, int loggedInDonorId, out int blogId);
        int Delete(int id);
        IEnumerable<VmBlogList> GetBetween(int start, int displayLength, string searchValue, out int totalLength);
    }
}
