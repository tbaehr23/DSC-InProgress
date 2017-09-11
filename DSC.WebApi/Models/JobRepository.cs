using System.Collections.Generic;
using System.Linq;
using DSC.Database;
using DSC.Database.Domain;

namespace DSC.WebApi.Models
{
    public class JobRepository : IJobRepository
    {
        private DSCContext _context;

        public JobRepository(DSCContext context)
        {
            _context = context;

            DSCContextFactory.InitializeDatabase(_context);
        }

        public IEnumerable<Job> GetList()
        {
            return _context.Jobs.ToArray();
        }

        public Job GetById(int id)
        {
            return _context.Jobs.Find(id);
        }

        public Job Save(Job jobToSave)
        {
            if (jobToSave.Id == 0)
            {
                _context.Add(jobToSave);
            }
            else
            {
                Mapper.Map(jobToSave, _context.Jobs.Find(jobToSave.Id));
            }

            _context.SaveChanges();

            return jobToSave;
        }

        public Job Delete(Job jobToDelete)
        {
            _context.Remove(_context.Jobs.Find(jobToDelete.Id));
            _context.SaveChanges();

            return jobToDelete;
        }
    }
}
