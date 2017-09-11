using System.Linq;
using DSC.Database.Domain;
using Xunit;

namespace DSC.WebApi.Test.Job_Repository
{
    [Collection("DSC")]
    public class When_Delete_executes
    {
        [Theory, InlineData(1), InlineData(2)]
        public void With_id_of_any_seed_job_Then_that_seed_job_is_deleted_and_is_returned(int id)
        {
            Job deletedJob;
            Job missingJob;

            int initialJobCount;
            int newJobCount;

            using (var context = DSCContextFactory.InMemoryContext())
            {
                var repository = new Models.JobRepository(context);

                initialJobCount = context.Jobs.Count();

                deletedJob = repository.Delete(context.Jobs.Find(id));

                newJobCount = context.Jobs.Count();

                missingJob = context.Jobs.Find(id);
            }

            Assert.True(newJobCount == initialJobCount - 1, "Job count is incorrect");

            Assert.Null(missingJob);

            Assert.IsType<Job>(deletedJob);
        }

    }
}
