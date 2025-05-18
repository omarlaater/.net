using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        public ICollection<Bilan> Bilans { get; set; }
    }
}
