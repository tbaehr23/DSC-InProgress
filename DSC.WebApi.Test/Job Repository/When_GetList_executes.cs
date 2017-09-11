using System.Collections.Generic;
using System.Linq;
using DSC.Database.Domain;
using DSC.WebApi.Models;
using Xunit;

namespace DSC.WebApi.Test.Job_Repository
{
    [Collection("DSC")]
    public class When_GetList_executes
    {
        [Fact]
        public void Then_a_complete_list_of_seed_jobs_is_returned()
        {
            IEnumerable<Job> jobs;

            using (var context = DSCContextFactory.InMemoryContext())
            {
                var repository = new JobRepository(context);

                jobs = repository.GetList();
            }

            Assert.True(jobs.Any(), "There are not any jobs");

            Assert.All(jobs, j => Assert.IsType<Job>(j));

            var seedDataCount = SeedData.Jobs().Count();
            Assert.True(jobs.Count() == seedDataCount, "There are not " + seedDataCount + " jobs");
        }

    }
}

