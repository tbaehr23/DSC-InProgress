using System.Collections.Generic;
using DSC.Database.Domain;

namespace DSC.WebApi.Models
{
    public static class SeedData
    {

        public static IEnumerable<Job> Jobs()
        {
            return new List<Job>
            {
                new Job {Name = "Job1", IsCompleted = false},
                new Job {Name = "Job2", IsCompleted = true}
            };
        }
    }
}
