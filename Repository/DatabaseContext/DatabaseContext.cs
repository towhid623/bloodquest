using Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DatabaseContext
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("name = projectconnectionstring")
        {

        }
        public virtual DbSet<ConfigMaster> ConfigMasters { get; set; }
        public virtual DbSet<ConfigValueSet> ConfigValueSets { get; set; }
        public virtual DbSet<BloodDonor> BloodDonors { get; set; }
        public virtual DbSet<Blog> Blog { get; set; }
        public virtual DbSet<District> District { get; set; }
        public virtual DbSet<Division> Division { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Upazila> Upazila { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
    }
}
