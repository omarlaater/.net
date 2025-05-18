using System.ComponentModel.DataAnnotations;

namespace Examen.ApplicationCore.Domain
{
    public class Analyse
    {
        [Key]
        public int AnalyseId { get; set; }
        public string TypeAnalyse { get; set; }
        public int DureeResultat { get; set; }
        public double PrixAnalyse { get; set; }
        public float ValeurAnalyse { get; set; }
        public float ValeurMinNormale { get; set; }
        public float ValeurMaxNormale { get; set; }

        public int? LaboratoireId { get; set; }
        public virtual Laboratoire Laboratoire { get; set; }

        public string BilanCodeInfirmier { get; set; }
        public string BilanCodePatient { get; set; }
        public DateTime BilanDatePrelevement { get; set; }
        public virtual Bilan Bilan { get; set; }
    }
}
