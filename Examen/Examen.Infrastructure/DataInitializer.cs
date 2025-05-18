using Examen.ApplicationCore.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Examen.Infrastructure
{
    public static class DataInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Vérifier si la base de données est déjà alimentée
            if (context.Laboratoires.Any() || context.Infirmiers.Any() || context.Patients.Any() || context.Bilans.Any())
            {
                return; // La base de données a déjà été initialisée
            }

            // Ajouter un laboratoire
            var labo = new Laboratoire
            {
                Intitule = "Labo1",
                Localisation = "Tunis"
            };
            context.Laboratoires.Add(labo);
            context.SaveChanges();

            // Ajouter un infirmier
            var infirmier = new Infirmier
            {
                CodeInfirmier = "INF01",
                Nom = "Infirmier1",
                Specialite = Specialite.Hematologie,
                LaboratoireId = labo.LaboratoireId
            };
            context.Infirmiers.Add(infirmier);
            context.SaveChanges();

            // Ajouter un patient
            var patient = new Patient
            {
                CodePatient = "PAT01",
                Nom = "Patient",
                Prenom = "1",
                EmailPatient = "patient1@gmail.com",
                Informations = "Diabète"
            };
            context.Patients.Add(patient);
            context.SaveChanges();

            // Ajouter un bilan
            var bilan = new Bilan
            {
                CodeInfirmier = infirmier.CodeInfirmier,
                CodePatient = patient.CodePatient,
                DatePrelevement = DateTime.Now,
                EmailMedecin = "medecin@gmail.com",
                Paye = true
            };
            context.Bilans.Add(bilan);
            context.SaveChanges();

            // Ajouter une analyse
            var analyse = new Analyse
            {
                TypeAnalyse = "Analyse de sang",
                DureeResultat = 2,
                PrixAnalyse = 50.0,
                ValeurAnalyse = 120.5f,
                ValeurMinNormale = 80.0f,
                ValeurMaxNormale = 120.0f,
                LaboratoireId = labo.LaboratoireId,
                BilanCodeInfirmier = bilan.CodeInfirmier,
                BilanCodePatient = bilan.CodePatient,
                BilanDatePrelevement = bilan.DatePrelevement
            };
            context.Analyses.Add(analyse);
            context.SaveChanges();
        }
    }
}
