using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Examen.ApplicationCore.Domain
{
    public class Patient
    {
        [Key]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Le code patient doit contenir exactement 5 caract�res.")]
        public string CodePatient { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire")]
        [StringLength(50)]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le pr�nom est obligatoire")]
        [StringLength(50)]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "L'email est obligatoire")]
        [EmailAddress(ErrorMessage = "Format d'email invalide")]
        public string EmailPatient { get; set; }

        [Display(Name = "Nom complet")]
        public string NomComplet => $"{Nom} {Prenom}";

        [Display(Name = "Informations suppl�mentaires")]
        [DataType(DataType.MultilineText)]
        public string Informations { get; set; }

        public ICollection<Bilan> Bilans { get; set; }
    }
}
