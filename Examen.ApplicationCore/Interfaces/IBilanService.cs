using Examen.ApplicationCore.Domain;
using System;
using System.Collections.Generic;

namespace Examen.ApplicationCore.Interfaces
{
    public interface IBilanService : IService<Bilan>
    {
        double CalculerMontantTotalBilan(string codeInfirmier, string codePatient, DateTime datePrelevement);
    }
}
