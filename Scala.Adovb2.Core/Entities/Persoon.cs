using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scala.Adovb2.Core.Entities
{
    public class Persoon
    {
        public string Id { get; set; }
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public string Adres { get; set; }
        public string Gemeente { get; set; }
        public DateTime Geboortedatum { get; set; }
        public string Soort { get; set; }

        public Persoon()
        {
            Id = Guid.NewGuid().ToString();
        }
        public Persoon(string naam, string voornaam, string adres, string gemeente,
            DateTime geboortedatum, string soort)
        {
            Id = Guid.NewGuid().ToString(); 
            Naam = naam;
            Voornaam = voornaam;
            Adres = adres;
            Gemeente = gemeente;
            Geboortedatum = geboortedatum;
            Soort = soort;
        }
        public Persoon(string id, string naam, string voornaam, string adres, string gemeente,
            DateTime geboortedatum, string soort)
        {
            Id = id;
            Naam = naam;
            Voornaam = voornaam;
            Adres = adres;
            Gemeente = gemeente;
            Geboortedatum = geboortedatum;
            Soort = soort;
        }
        public override string ToString()
        {
            return $"{Naam} {Voornaam}";
        }

    }
}
