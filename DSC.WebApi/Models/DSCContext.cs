using Microsoft.EntityFrameworkCore;
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace DSC.WebApi.Models
{
    public class DSCContext : DbContext
    {
        public DSCContext(DbContextOptions<DSCContext> options) : base(options)
        {

        }
        public DbSet<Job> Jobs { get; set; }
    }
}
