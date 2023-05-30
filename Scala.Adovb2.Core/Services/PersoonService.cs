using Scala.Adovb2.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scala.Adovb2.Core.Services
{
    public class PersoonService
    {
        public List<Persoon> GetPersonen(string soortfilter = null)
        {
            List<Persoon> personen = new List<Persoon>();
            string sql;
            sql = "select * from personen ";
            if(soortfilter != null)
            {
                sql += $"where soort = '{soortfilter}' ";
            }
            sql += " order by naam, voornaam";
            DataTable dt = DBServices.ExecuteSelect(sql);
            foreach(DataRow dr in dt.Rows)
            {
                string id = dr["id"].ToString();
                string naam = dr["naam"].ToString();
                string voornaam = dr["voornaam"].ToString();
                string adres = dr["adres"].ToString();
                string gemeente = dr["gemeente"].ToString();
                DateTime geboortedatum =DateTime.Parse(dr["geboortedatum"].ToString());
                string soort = dr["soort"].ToString();
                personen.Add(new Persoon(id, naam, voornaam, adres, gemeente, geboortedatum, soort));
            }
            return personen;
        }
        public bool PersoonToevoegen(Persoon persoon)
        {
            string sql;
            sql = "insert into personen (id, naam, voornaam, adres, gemeente, geboortedatum, soort) values (";
            sql += $"'{persoon.Id}' , ";
            sql += $"'{Helper.HandleQuotes(persoon.Naam)}' , ";
            sql += $"'{Helper.HandleQuotes(persoon.Voornaam)}' , ";
            sql += $"'{Helper.HandleQuotes(persoon.Adres)}' , ";
            sql += $"'{Helper.HandleQuotes(persoon.Gemeente)}' , ";
            sql += $"'{persoon.Geboortedatum.ToString("yyyy-MM-dd")}' , ";
            sql += $"'{Helper.HandleQuotes(persoon.Soort)}') ";
            return DBServices.ExecuteCommand(sql);
        }
        public bool PersoonWijzigen(Persoon persoon)
        {
            string sql;
            sql = "update personen set ";
            sql += $" naam = '{Helper.HandleQuotes(persoon.Naam)}' , ";
            sql += $" voornaam = '{Helper.HandleQuotes(persoon.Voornaam)}' , ";
            sql += $" adres = '{Helper.HandleQuotes(persoon.Adres)}' , ";
            sql += $" gemeente = '{Helper.HandleQuotes(persoon.Gemeente)}' , ";
            sql += $" geboortedatum = '{persoon.Geboortedatum.ToString("yyyy-MM-dd")}' , ";
            sql += $" soort = '{Helper.HandleQuotes(persoon.Soort)}' ";
            sql += $" where id = '{persoon.Id}' ";
            return DBServices.ExecuteCommand(sql);
        }
        public bool PersoonVerwijderen(Persoon persoon)
        {
            string sql;
            sql = "delete from personen ";
            sql += $" where id = '{persoon.Id}' ";
            return DBServices.ExecuteCommand(sql);
        }
    }
}
