using System.Linq;
using DSC.Database.Domain;
using DSC.WebApi.Models;
using Xunit;

namespace DSC.WebApi.Test.Job_Repository
{
    [Collection("DSC")]
    public class When_GetById_executes
    {
        [Theory, InlineData(1), InlineData(2)]
        public void With_id_of_any_seed_job_Then_that_seed_job_is_returned(int id)
        {
            Job job;

            using (var context = DSCContextFactory.InMemoryContext())
            {
                var repository = new Models.JobRepository(context);

                job = repository.GetById(id);
            }

            var seedDataJob = SeedData.Jobs().FirstOrDefault(j => j.Name.EndsWith(id.ToString()));
            Assert.True(Comparator.JobsAreIdentical(seedDataJob, job), "Jobs are not identical");
        }

    }
}
