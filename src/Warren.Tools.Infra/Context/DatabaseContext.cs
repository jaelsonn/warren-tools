using Microsoft.EntityFrameworkCore;
using Warren.Tools.Domain.Entities;
using Warren.Tools.Domain.Models;

namespace Warren.Tools.Infra.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<BoletaEntity> Boletas { get; set; }
        public DbSet<Pu550Entity> Pu550 { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pu550Entity>(p =>
            {
                p.HasNoKey();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
