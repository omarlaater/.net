using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Examen.ApplicationCore.Services
{
    public class AnalyseService : Service<Analyse>, IAnalyseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnalyseService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public DateTime CalculerDateRecuperationBilan(string codeInfirmier, string codePatient, DateTime datePrelevement)
        {
           
            var bilan = _unitOfWork.Repository<Bilan>().Get(
                b => b.CodeInfirmier == codeInfirmier && 
                     b.CodePatient == codePatient && 
                     b.DatePrelevement == datePrelevement);

            if (bilan == null)
                return DateTime.MinValue; 
            var analyses = GetMany(
                a => a.BilanCodeInfirmier == codeInfirmier && 
                     a.BilanCodePatient == codePatient && 
                     a.BilanDatePrelevement == datePrelevement)
                .ToList();

            if (!analyses.Any())
                return datePrelevement; 
            int dureeMaximale = analyses.Max(a => a.DureeResultat);
            
          
            return datePrelevement.AddDays(dureeMaximale);
        }
    }
}
