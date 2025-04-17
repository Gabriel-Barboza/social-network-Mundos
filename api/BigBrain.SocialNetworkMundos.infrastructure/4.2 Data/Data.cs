using BigBrain.SocialNetworkMundos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace BigBrain.SocialNetworkMundos.Infra.Data
{
    public class DataContext : DbContext

    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!;

    }
}
