using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using netcore_data_access.Models;

namespace netcore_data_access.Repositories {
    public class RubberDuckRepository {
        private readonly RubberDuckContext context;

        public RubberDuckRepository(RubberDuckContext context) {
            this.context = context;

            if (!this.context.RubberDucks.Any()) {
                this.context.RubberDucks.AddRange(
                    new RubberDuck {
                        Name = "Basic duck",
                        Color = "Yellow"
                    },
                    new RubberDuck {
                        Name = "Basic duck with hat",
                        Color = "Yellow"
                    },
                    new RubberDuck {
                        Name = "Wizard duck",
                        Color = "Rainbow"
                    }
                );
                this.context.SaveChanges();
            }
        }

        public List<RubberDuck> Get() {
            return context.RubberDucks.OrderBy(p => p.Name).ToList();
        }

        public IAsyncEnumerable<RubberDuck> GetAsync() {
            return context.RubberDucks.OrderBy(p => p.Name).AsAsyncEnumerable();
        }

        public bool TryGet(int id, out RubberDuck product) {
            product = context.RubberDucks.Find(id);

            return product != null;
        }

        public async Task<int> AddAsync(RubberDuck product) {
            context.RubberDucks.Add(product);
            var rowsAffected = await context.SaveChangesAsync();
            return rowsAffected;
        }
    }
}