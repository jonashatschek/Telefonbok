using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Xml.Linq;

namespace Telefonbok.Models
{
    public class Kontakt
    {
        public int Id { get; set; }
        public string Förnamn { get; set; }
        public string Efternamn { get; set; }
        public string Telefonnummer { get; set; }


        public XElement ToXml()
        {
            XElement contact = new XElement("Kontakt",
                new XElement("Id", this.Id),
                new XElement("Förnamn", this.Förnamn),
                new XElement("Efternamn", this.Efternamn),
                new XElement("Telefonnummer", this.Telefonnummer));

            return contact;
        }

        public static Kontakt FromXml(XElement xml)
        {
                Kontakt contact = new Kontakt()
                {
                    Id = (int)xml.Element("Id"),
                    Förnamn = xml.Element("Förnamn").Value,
                    Efternamn = xml.Element("Efternamn").Value,
                    Telefonnummer = xml.Element("Telefonnummer").Value
                };

                return contact;
        }
    }


}