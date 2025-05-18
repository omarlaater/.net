using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examen.ApplicationCore.Domain
{
    public class Infirmier
    {
        [Key]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Le code infirmier doit contenir exactement 5 caractères.")]
        public string CodeInfirmier { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire")]
        [StringLength(50)]
        public string Nom { get; set; }

        [Display(Name = "Nom complet")]
        public string NomComplet => Nom;

        public Specialite Specialite { get; set; }

        [ForeignKey("Laboratoire")]
        public int LaboratoireId { get; set; }
        public virtual Laboratoire Laboratoire { get; set; }

        public ICollection<Bilan> Bilans { get; set; }
    }
}
