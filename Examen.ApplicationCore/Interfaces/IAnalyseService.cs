using Examen.ApplicationCore.Domain;
using System;

namespace Examen.ApplicationCore.Interfaces
{
    public interface IAnalyseService : IService<Analyse>
    {
        DateTime CalculerDateRecuperationBilan(string codeInfirmier, string codePatient, DateTime datePrelevement);
    }
}
