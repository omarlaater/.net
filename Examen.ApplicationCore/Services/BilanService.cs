using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Examen.ApplicationCore.Services
{
    public class BilanService : Service<Bilan>, IBilanService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BilanService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public double CalculerMontantTotalBilan(string codeInfirmier, string codePatient, DateTime datePrelevement)
        {
            
            var bilan = Get(b => b.CodeInfirmier == codeInfirmier && 
                                b.CodePatient == codePatient && 
                                b.DatePrelevement == datePrelevement);

            if (bilan == null)
                return 0;

            
            var analyses = _unitOfWork.Repository<Analyse>().GetMany(
                a => a.BilanCodeInfirmier == codeInfirmier && 
                     a.BilanCodePatient == codePatient && 
                     a.BilanDatePrelevement == datePrelevement);

            
            double montantTotal = analyses.Sum(a => a.PrixAnalyse);

           
            int nombrePrelevements = _unitOfWork.Repository<Bilan>()
                .GetMany(b => b.CodePatient == codePatient)
                .Count();

           
            if (nombrePrelevements > 5)
            {
                montantTotal = montantTotal * 0.9; 
            }

            return montantTotal;
        }
    }
}
