using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Examen.ApplicationCore.Services
{
    public class PatientService : Service<Patient>, IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public Dictionary<Bilan, List<Analyse>> ObtenirAnalysesAnormalesParBilan(string codePatient)
        {
            
            var patient = Get(p => p.CodePatient == codePatient);
            if (patient == null)
                return new Dictionary<Bilan, List<Analyse>>();

            
            int anneeEnCours = DateTime.Now.Year;
            var bilans = _unitOfWork.Repository<Bilan>()
                .GetMany(b => b.CodePatient == codePatient && 
                             b.DatePrelevement.Year == anneeEnCours)
                .ToList();

            var resultat = new Dictionary<Bilan, List<Analyse>>();

            foreach (var bilan in bilans)
            {
                
                var analyses = _unitOfWork.Repository<Analyse>()
                    .GetMany(a => a.BilanCodeInfirmier == bilan.CodeInfirmier &&
                                 a.BilanCodePatient == bilan.CodePatient &&
                                 a.BilanDatePrelevement == bilan.DatePrelevement)
                    .ToList();

                var analysesAnormales = analyses.Where(a => 
                    a.ValeurAnalyse > a.ValeurMaxNormale || 
                    a.ValeurAnalyse < a.ValeurMinNormale)
                    .ToList();

                
                if (analysesAnormales.Any())
                {
                    resultat.Add(bilan, analysesAnormales);
                }
            }

            return resultat;
        }
    }
}
