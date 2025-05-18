using System;
using System.Linq;
using Examen.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Examen.ApplicationCore.Domain;
using System.Collections.Generic;

namespace Examen.UI.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            using var context = new ApplicationDbContext();
            context.Database.EnsureCreated();

            try
            {
                System.Console.WriteLine("Affichage du contenu de la base de données...\n");

                
                System.Console.WriteLine("\nDétails des Laboratoires :");
                var laboratoires = context.Laboratoires.Include(l => l.Analyses).ToList();
                foreach (var labo in laboratoires)
                {
                    System.Console.WriteLine($"\nLaboratoire: {labo.Intitule}");
                    System.Console.WriteLine($"Localisation: {labo.Localisation}");
                    System.Console.WriteLine($"Nombre d'analyses: {labo.Analyses.Count}");
                }

               
                System.Console.WriteLine("\nDétails des Infirmiers :");
                var infirmiers = context.Infirmiers.Include(i => i.Bilans).ToList();
                foreach (var inf in infirmiers)
                {
                    System.Console.WriteLine($"\nInfirmier: {inf.Nom}");
                    System.Console.WriteLine($"Code: {inf.CodeInfirmier}");
                    System.Console.WriteLine($"Nombre de bilans: {inf.Bilans.Count}");
                }

                // Afficher les détails des patients
                System.Console.WriteLine("\nDétails des Patients :");
                var patients = context.Patients.Include(p => p.Bilans).ToList();
                foreach (var pat in patients)
                {
                    System.Console.WriteLine($"\nPatient: {pat.Nom} {pat.Prenom}");
                    System.Console.WriteLine($"Code: {pat.CodePatient}");
                    System.Console.WriteLine($"Email: {pat.EmailPatient}");
                    System.Console.WriteLine($"Informations: {pat.Informations}");
                    System.Console.WriteLine($"Nombre de bilans: {pat.Bilans.Count}");
                }

                
                System.Console.WriteLine("\nDétails des Bilans :");
                var bilans = context.Bilans.Include(b => b.Infirmier)
                                          .Include(b => b.Patient)
                                          .Include(b => b.Analyses)
                                          .ToList();
                foreach (var bilan in bilans)
                {
                    System.Console.WriteLine($"\nBilan du {bilan.DatePrelevement:dd/MM/yyyy}");
                    System.Console.WriteLine($"Patient: {bilan.Patient.Nom} {bilan.Patient.Prenom}");
                    System.Console.WriteLine($"Infirmier: {bilan.Infirmier.Nom}");
                    System.Console.WriteLine($"Email Médecin: {bilan.EmailMedecin}");
                    System.Console.WriteLine($"Payé: {(bilan.Paye ? "Oui" : "Non")}");
                    System.Console.WriteLine($"Nombre d'analyses: {bilan.Analyses.Count}");
                }

                
                System.Console.WriteLine("\nDétails des Analyses :");
                var analyses = context.Analyses.Include(a => a.Laboratoire)
                                              .Include(a => a.Bilan)
                                              .ToList();
                foreach (var analyse in analyses)
                {
                    System.Console.WriteLine($"\nAnalyse ID: {analyse.AnalyseId}");
                    System.Console.WriteLine($"Type: {analyse.TypeAnalyse}");
                    System.Console.WriteLine($"Laboratoire: {analyse.Laboratoire.Intitule}");
                    System.Console.WriteLine($"Durée résultat: {analyse.DureeResultat}h");
                    System.Console.WriteLine($"Prix: {analyse.PrixAnalyse:C}");
                    System.Console.WriteLine($"Valeur: {analyse.ValeurAnalyse} (Normal: {analyse.ValeurMinNormale}-{analyse.ValeurMaxNormale})");
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Une erreur s'est produite : {ex.Message}");
                if (ex.InnerException != null)
                {
                    System.Console.WriteLine($"Détails : {ex.InnerException.Message}");
                }
            }
        }
    }
}
