using DSC.WebApi.Models;
using Microsoft.EntityFrameworkCore;
// ReSharper disable InconsistentNaming

namespace DSC.WebApi
{
    public static class DSCContextFactory
    {
        public static DSCContext InMemoryContext()
        {
            var optionInMemory = new DbContextOptionsBuilder<DSCContext>().UseInMemoryDatabase().Options;

            return new DSCContext(optionInMemory);
        }
    }
}
