using System;
using System.Linq;
using DSC.Database.Domain;
using DSC.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace DSC.WebApi.Test.Job_Controller
{
    [Collection("DSC")]
    public class When_Get_with_a_parameter_executes
    {
        [Theory,
         InlineData(1, typeof(ObjectResult)),
         InlineData(2, typeof(ObjectResult)),
         InlineData(3, typeof(NotFoundResult))]
        public void Then_correct_result_type_is_returned(int id, Type resultType)
        {
            using (var context = DSCContextFactory.InMemoryContext())
            {
                var controller = new Controllers.JobController(new Models.JobRepository(context));

                var result = controller.Get(id);

                Assert.IsType(resultType, result);
            }
        }

        [Theory, InlineData(1), InlineData(2)]
        public void With_a_valid_parameter_value_Then_a_job_is_contained_in_the_ObjectResult(int id)
        {
            using (var context = DSCContextFactory.InMemoryContext())
            {
                var controller = new Controllers.JobController(new Models.JobRepository(context));

                var result = controller.Get(id);

                var data = ((ObjectResult)result).Value as Job;

                Assert.IsType<Job>(data);

                var seedDataJob = SeedData.Jobs().FirstOrDefault(j => j.Name.EndsWith(id.ToString()));
                Assert.True(data.Name == seedDataJob.Name);
            }
        }
    }
}
