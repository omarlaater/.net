using Examen.ApplicationCore.Domain;
using System;

namespace Examen.ApplicationCore.Interfaces
{
    public interface IInfirmierService : IService<Infirmier>
    {
        double CalculerPourcentageInfirmiersParSpecialite(Specialite specialite);
    }
}
