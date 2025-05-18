using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Examen.ApplicationCore.Domain
{
    public class Laboratoire
    {
        [Key]
        public int LaboratoireId { get; set; }

        public string Intitule { get; set; }

        [MaxLength(50)]
        public string Localisation { get; set; }

        public ICollection<Analyse> Analyses { get; set; }
    }
}
