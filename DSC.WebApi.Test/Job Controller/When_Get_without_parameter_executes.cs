﻿using System.Collections.Generic;
using System.Linq;
using DSC.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;
// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace

namespace DSC.WebApi.Test.JobController
{
    public class When_Get_without_parameter_executes
    {
        private readonly IActionResult _result;
        private readonly IEnumerable<Job> _jobs;

        public When_Get_without_parameter_executes()
        {
            using (var context = DSCContextFactory.InMemoryContext())
            {
                var controller = new Controllers.JobController(context);

                _result = controller.Get();

                var data = ((ObjectResult)_result).Value;
                _jobs = ((IEnumerable<Job>)data).ToList();
            }
        }

        [Fact]
        public void Then_an_ObjectResult_is_returned()
        {
            Assert.IsType<ObjectResult>(_result);
        }

        [Fact]
        public void Then_a_complete_list_of_seed_jobs_is_contained_in_the_ObjectResult()
        {
            Assert.True(_jobs.Any(), "There are not any jobs");

            Assert.All(_jobs, j => Assert.IsType<Job>(j));

            Assert.True(_jobs.Count() == 2, "There are not 2 jobs");
        }
    }
}