using Examen.ApplicationCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace Examen.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Infirmier> Infirmiers { get; set; }
        public DbSet<Bilan> Bilans { get; set; }
        public DbSet<Analyse> Analyses { get; set; }
        public DbSet<Laboratoire> Laboratoires { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(LocalDB)\MSSQLLocalDB;Database=LaboBenMansourGhada;Trusted_Connection=True;");
        }
    }
}
