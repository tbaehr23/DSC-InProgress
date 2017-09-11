using System.Linq;
using DSC.Database.Domain;
using Xunit;

namespace DSC.WebApi.Test.Job_Repository
{
    [Collection("DSC")]
    public class When_Save_execures
    {
        [Fact]
        public void With_a_new_job_Then_a_new_job_is_added_and_returned_with_a_new_id_value()
        {
            var newJob = new Job {Name = "new job", IsCompleted = false};
            Job savedJob;

            int initialJobCount;
            int newJobCount;

            using (var context = DSCContextFactory.InMemoryContext())
            {
                var repository = new Models.JobRepository(context);

                initialJobCount = context.Jobs.Count();

                savedJob = repository.Save(newJob);

                newJobCount = context.Jobs.Count();
            }

            Assert.True(newJobCount == initialJobCount + 1, "Job count is incorrect");

            Assert.True(Comparator.JobsAreIdentical(newJob, savedJob), "Jobs are not identical");

            Assert.True(savedJob.Id == newJobCount, "Job has incorrect Id value");

        }

        [Theory, InlineData(1), InlineData(2)]
        public void With_a_existing_job_Then_an_existing_job_is_edited_and_returned(int id)
        {
            Job existingJob;
            Job savedJob;

            using (var context = DSCContextFactory.InMemoryContext())
            {
                var repository = new Models.JobRepository(context);

                existingJob = context.Jobs.Find(id);

                existingJob.Name = existingJob.Name + "{mod}";
                existingJob.IsCompleted = !existingJob.IsCompleted;

                savedJob = repository.Save(existingJob);
            }

            Assert.True(existingJob == savedJob, "Jobs are not equal");
        }

    }
}
