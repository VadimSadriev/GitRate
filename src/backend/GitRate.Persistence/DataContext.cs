using Microsoft.EntityFrameworkCore;

namespace GitRate.Persistence
{
    /// <summary>
    /// Main GitRate data context
    /// </summary>
    public class DataContext : DbContext
    {
        /// <summary>
        /// Main GitRate data context
        /// </summary>
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
    }
}