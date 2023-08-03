using System.Linq;
using bonachat;
using bonachat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace bonachat { 
public class EtatBonController : Controller
{

    public IActionResult Index()
    {
            List<EtatBon> etats = new List<EtatBon>();

            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=root;Database=bon_achat;";

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                string sqlQuery = "SELECT * FROM bon_achat.etat_bon order by \"Code\" asc;";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlQuery, con))
                {
                    using ( NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            EtatBon etatBon = new EtatBon();
                            etatBon.Code = dr["code"].ToString();
                            etatBon.Libelle = dr["libelle"].ToString();
                            etatBon.Description = dr["description"].ToString();
                            etatBon.LibelleParDefaut = dr["libelleParDefaut"].ToString();
                            etatBon.Couleur = dr["couleur"].ToString();
                            etats.Add(etatBon);
                        }
                    }
                }
            }

            return View(etats);
        }

        public IActionResult UpdateLibelle(string NouvelleLibelle, string Code)
        {

            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=root;Database=bon_achat;";
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();

                string sqlQuery = "UPDATE bon_achat.etat_bon SET \"Libelle\" = @nouvelleLibelle WHERE \"Code\" = @code;";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("nouvelleLibelle", NouvelleLibelle);
                    cmd.Parameters.AddWithValue("Code", int.Parse(Code));

                    cmd.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult LibelleParDefaut(int Code)
        {
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=root;Database=bon_achat;";
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();

                string sqlQuery = "UPDATE bon_achat.etat_bon SET \"Libelle\" = \"LibelleParDefaut\" WHERE \"Code\" = @code;";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("Code", Code);

                    cmd.ExecuteNonQuery();
                }
            }

            // Redirect back to the same page to display the updated data.
            return RedirectToAction("Index");
        }



    }

}