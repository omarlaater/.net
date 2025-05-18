using Examen.ApplicationCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Examen.Infrastructure.Configurations
{
    public class BilanConfiguration : IEntityTypeConfiguration<Bilan>
    {
        public void Configure(EntityTypeBuilder<Bilan> builder)
        {
            builder.HasKey(b => new { b.CodeInfirmier, b.CodePatient, b.DatePrelevement });

            builder.HasOne(b => b.Infirmier)
                   .WithMany(i => i.Bilans)
                   .HasForeignKey(b => b.CodeInfirmier);

            builder.HasOne(b => b.Patient)
                   .WithMany(p => p.Bilans)
                   .HasForeignKey(b => b.CodePatient);
        }
    }
}
