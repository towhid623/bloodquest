using Entities.Models;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IDonorService
    {
        int Add(VmDonorAdd vmDonor,out int donorHeaderId);
        int Delete(int id);
        IEnumerable<VmDonorList> GetBetween(int start, int displayLength, string searchValue, out int totalLength);
        string GenerateDonorId();
    }
}
