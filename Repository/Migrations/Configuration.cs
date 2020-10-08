namespace Repository.Migrations
{
    using Entities.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Repository.DatabaseContext.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Repository.DatabaseContext.DatabaseContext context)
        {
          
            context.ConfigMasters.AddOrUpdate(a => a.ConfigShortName,
            new ConfigMaster { ConfigName = "Blood Group", ConfigShortName = "bloodgroup" },
            new ConfigMaster { ConfigName = "Gender", ConfigShortName = "gender" }
            );
            context.SaveChanges();
            context.ConfigValueSets.AddOrUpdate(a => a.ConfigShortValue,
            new ConfigValueSet { ConfigValue = "A+", ConfigShortValue = "A+", ConfigValueSetId = context.ConfigMasters.FirstOrDefault(f => f.ConfigShortName == "bloodgroup").ConfigId },
            new ConfigValueSet { ConfigValue = "A-", ConfigShortValue = "A-", ConfigValueSetId = context.ConfigMasters.FirstOrDefault(f => f.ConfigShortName == "bloodgroup").ConfigId },
            new ConfigValueSet { ConfigValue = "AB+", ConfigShortValue = "AB+", ConfigValueSetId = context.ConfigMasters.FirstOrDefault(f => f.ConfigShortName == "bloodgroup").ConfigId },
            new ConfigValueSet { ConfigValue = "AB-", ConfigShortValue = "AB-", ConfigValueSetId = context.ConfigMasters.FirstOrDefault(f => f.ConfigShortName == "bloodgroup").ConfigId },
            new ConfigValueSet { ConfigValue = "B+", ConfigShortValue = "B+", ConfigValueSetId = context.ConfigMasters.FirstOrDefault(f => f.ConfigShortName == "bloodgroup").ConfigId },
            new ConfigValueSet { ConfigValue = "B-", ConfigShortValue = "B-", ConfigValueSetId = context.ConfigMasters.FirstOrDefault(f => f.ConfigShortName == "bloodgroup").ConfigId },
            new ConfigValueSet { ConfigValue = "O+", ConfigShortValue = "O+", ConfigValueSetId = context.ConfigMasters.FirstOrDefault(f => f.ConfigShortName == "bloodgroup").ConfigId },
            new ConfigValueSet { ConfigValue = "O-", ConfigShortValue = "O-", ConfigValueSetId = context.ConfigMasters.FirstOrDefault(f => f.ConfigShortName == "bloodgroup").ConfigId },
            new ConfigValueSet { ConfigValue = "Unknown", ConfigShortValue = "unknown", ConfigValueSetId = context.ConfigMasters.FirstOrDefault(f => f.ConfigShortName == "bloodgroup").ConfigId },
            new ConfigValueSet { ConfigValue = "Male", ConfigShortValue = "male", ConfigValueSetId = context.ConfigMasters.FirstOrDefault(f => f.ConfigShortName == "gender").ConfigId },
            new ConfigValueSet { ConfigValue = "Female", ConfigShortValue = "female", ConfigValueSetId = context.ConfigMasters.FirstOrDefault(f => f.ConfigShortName == "gender").ConfigId },
            new ConfigValueSet { ConfigValue = "Others", ConfigShortValue = "others", ConfigValueSetId = context.ConfigMasters.FirstOrDefault(f => f.ConfigShortName == "gender").ConfigId }
            );
        }
    }
}
