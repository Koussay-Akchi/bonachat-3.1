using System.Linq;
using bonachat;
using bonachat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace bonachat
{
    public class BonAchatsElectroniquesController : Controller
    {

        public IActionResult Index()
        {
            List<BonAchatElectronique> bons = new List<BonAchatElectronique>();

            // Manually specify the connection string here.
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=root;Database=bon_achat;";

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                string sqlQuery = "select * from bon_achat.bon_achat_electronique;";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlQuery, con))
                {
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            BonAchatElectronique bon_achat = new BonAchatElectronique();
                            bon_achat.Id = Convert.ToInt32(dr["id"]);
                            bon_achat.IdUtilisateur = Convert.ToInt32(dr["idUtilisateur"]);
                            bon_achat.SoldeCarteUtilisateurAvant = Convert.ToDecimal(dr["SoldeCarteUtilisateurAvant"]);
                            bon_achat.SoldeCarteUtilisateurApres = Convert.ToDecimal(dr["SoldeCarteUtilisateurApres"]);
                            bon_achat.DateGeneration = Convert.ToDateTime(dr["DateGeneration"]);
                            bon_achat.CodeEtat = dr["CodeEtat"].ToString();
                            bon_achat.CodeType = dr["CodeType"].ToString();
                            bon_achat.ValeurInitiale = Convert.ToDecimal(dr["ValeurInitiale"]);
                            bon_achat.ValeurRestante = Convert.ToDecimal(dr["ValeurRestante"]);
                            bon_achat.EmailBeneficiaire = dr["EmailBeneficiaire"].ToString();
                            bon_achat.TelephoneBeneficiaire = dr["TelephoneBeneficiaire"].ToString();
                            bon_achat.CodeSource = dr["CodeSource"].ToString();
                            bon_achat.Couleur = GetCouleurByCode(bon_achat.CodeEtat);
                            bons.Add(bon_achat);
                        }
                    }
                }
            }

            return View(bons);
        }

        private string GetCouleurByCode(string codeEtat)
        {
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=root;Database=bon_achat;";
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                string sqlQuery = "SELECT \"Couleur\" FROM bon_achat.etat_bon WHERE \"Code\" = @code";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("code", int.Parse(codeEtat));
                    return cmd.ExecuteScalar().ToString();
                }
            }
        }

    }
}