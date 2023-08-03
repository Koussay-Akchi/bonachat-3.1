using System.Linq;
using bonachat;
using bonachat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace bonachat { 
public class SourceBonController : Controller
{

    public IActionResult Index()
    {
            List<SourceBon> Sources = new List<SourceBon>();

            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=root;Database=bon_achat;";

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                string sqlQuery = "SELECT * FROM bon_achat.Source_bon order by \"Code\" asc;";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlQuery, con))
                {
                    using ( NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            SourceBon SourceBon = new SourceBon();
                            SourceBon.Code = dr["Code"].ToString();
                            SourceBon.Libelle = dr["Libelle"].ToString();
                            Sources.Add(SourceBon);
                        }
                    }
                }
            }

            return View(Sources);
        }
}

}