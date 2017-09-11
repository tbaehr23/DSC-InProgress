using DSC.Database.Domain;

namespace DSC.WebApi.Models
{
    internal static class Mapper
    {
        public static void Map(Job jobToSave, Job existingJob)
        {
            existingJob.Name = jobToSave.Name;
            existingJob.IsCompleted = jobToSave.IsCompleted;
        }
    }
}
