using Microsoft.EntityFrameworkCore;
using netcore_data_access.Models;

namespace netcore_data_access {
    public class RubberDuckContext : DbContext {
        public RubberDuckContext(DbContextOptions<RubberDuckContext> options)
            : base(options) { }

        public DbSet<RubberDuck> RubberDucks { get; set; }
    }
}