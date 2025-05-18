using Examen.ApplicationCore.Domain;
using System;
using System.Collections.Generic;

namespace Examen.ApplicationCore.Interfaces
{
    public interface IPatientService : IService<Patient>
    {
        Dictionary<Bilan, List<Analyse>> ObtenirAnalysesAnormalesParBilan(string codePatient);
    }
}
