using System.Linq;
using bonachat;
using bonachat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace bonachat { 
public class TypeBonController : Controller
{

    public IActionResult Index()
    {
            List<TypeBon> Types = new List<TypeBon>();

            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=root;Database=bon_achat;";

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                con.Open();
                string sqlQuery = "SELECT * FROM bon_achat.Type_bon order by \"Code\" asc;";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlQuery, con))
                {
                    using ( NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            TypeBon TypeBon = new TypeBon();
                            TypeBon.Code = dr["Code"].ToString();
                            TypeBon.Libelle = dr["Libelle"].ToString();
                            Types.Add(TypeBon);
                        }
                    }
                }
            }

            return View(Types);
        }
}

}