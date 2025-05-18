using System;
using System.Collections.Generic;

namespace Examen.ApplicationCore.Domain
{
    public class Bilan
    {
        public string CodeInfirmier { get; set; }
        public string CodePatient { get; set; }
        public DateTime DatePrelevement { get; set; }
        public string EmailMedecin { get; set; }
        public bool Paye { get; set; }

        public Infirmier Infirmier { get; set; }
        public Patient Patient { get; set; }

        public ICollection<Analyse> Analyses { get; set; }
    }
}
