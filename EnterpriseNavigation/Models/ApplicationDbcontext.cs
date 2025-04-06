using Microsoft.EntityFrameworkCore;

namespace EnterpriseNavigation.Models
{
    public class ApplicationDbcontext : DbContext

    {
        public ApplicationDbcontext(DbContextOptions options):
            base(options)
        {
            
        }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Category>  categories { get; set; }
    }
}
