using Examen.ApplicationCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Examen.Infrastructure.Configurations
{
    public class LaboratoireConfiguration : IEntityTypeConfiguration<Laboratoire>
    {
        public void Configure(EntityTypeBuilder<Laboratoire> builder)
        {
            builder.Property(l => l.Localisation)
                   .HasColumnName("AdresseLabo")
                   .HasMaxLength(50);
        }
    }
}
