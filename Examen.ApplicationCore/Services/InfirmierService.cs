using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using System;
using System.Linq;

namespace Examen.ApplicationCore.Services
{
    public class InfirmierService : Service<Infirmier>, IInfirmierService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InfirmierService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public double CalculerPourcentageInfirmiersParSpecialite(Specialite specialite)
        {
            
            var tousLesInfirmiers = GetMany().ToList();
            
            if (tousLesInfirmiers.Count == 0)
                return 0;

           
            int infirmiersDeSpecialite = 0;
            
            foreach (var infirmier in tousLesInfirmiers)
            {
                
                var bilans = _unitOfWork.Repository<Bilan>()
                    .GetMany(b => b.CodeInfirmier == infirmier.CodeInfirmier);
                
                
                bool aSpecialite = false;
                
                foreach (var bilan in bilans)
                {
                   
                    var analyses = _unitOfWork.Repository<Analyse>()
                        .GetMany(a => a.BilanCodeInfirmier == bilan.CodeInfirmier &&
                                      a.BilanCodePatient == bilan.CodePatient &&
                                      a.BilanDatePrelevement == bilan.DatePrelevement);
                    
                    
                    
                    switch (specialite)
                    {
                        case Specialite.Hematologie:
                            aSpecialite = analyses.Any(a => a.TypeAnalyse.Contains("sang") || 
                                                           a.TypeAnalyse.Contains("hémato") ||
                                                           a.TypeAnalyse.Contains("hemato"));
                            break;
                        case Specialite.Biochimie:
                            aSpecialite = analyses.Any(a => a.TypeAnalyse.Contains("biochimi") || 
                                                           a.TypeAnalyse.Contains("enzyme") ||
                                                           a.TypeAnalyse.Contains("protéine") ||
                                                           a.TypeAnalyse.Contains("proteine"));
                            break;
                        case Specialite.Autre:
                            aSpecialite = analyses.Any(a => !a.TypeAnalyse.Contains("sang") && 
                                                           !a.TypeAnalyse.Contains("hémato") &&
                                                           !a.TypeAnalyse.Contains("hemato") &&
                                                           !a.TypeAnalyse.Contains("biochimi") &&
                                                           !a.TypeAnalyse.Contains("enzyme") &&
                                                           !a.TypeAnalyse.Contains("protéine") &&
                                                           !a.TypeAnalyse.Contains("proteine"));
                            break;
                    }
                    
                    if (aSpecialite)
                        break;
                }
                
                if (aSpecialite)
                    infirmiersDeSpecialite++;
            }
            
            
            return (double)infirmiersDeSpecialite / tousLesInfirmiers.Count * 100;
        }
    }
}
