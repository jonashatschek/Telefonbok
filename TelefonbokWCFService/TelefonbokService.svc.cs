using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Hosting;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace TelefonbokWCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TelefonbokService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TelefonbokService.svc or TelefonbokService.svc.cs at the Solution Explorer and start debugging.
    public class TelefonbokService : ITelefonbokService
    {
        private string appDataFolder = HostingEnvironment.MapPath("/App_Data/");
        private XElement allaKontakter;
        private static int id = 0;

        public TelefonbokService()
        {
            try
            {               
                allaKontakter = XElement.Load(appDataFolder + "telefonbok.xml");
            }
            catch (FileNotFoundException)
            {             
                File.Create(appDataFolder + "telefonbok.xml").Dispose();
                XElement rootNode =
                    new XElement("Kontakter");

                rootNode.Save(appDataFolder+"telefonbok.xml");
                allaKontakter = XElement.Load(appDataFolder + "telefonbok.xml");

            }

        }

        public XElement HämtaAllaKontakter()
        {
            return allaKontakter;
        }


        public void LäggTillKontakt(XElement kontakt)
        {
            kontakt.Element("Id").Value = id.ToString();
            allaKontakter.Add(kontakt);
            id++;
            SaveAllaKontakter();
        }
        
        public XElement SökKontakter(XElement kriterier)
        {
            var res = allaKontakter.Elements("Kontakt").Where(c => c.Element("Förnamn").Value.Contains(kriterier.Value)
                                                            ||c.Element("Efternamn").Value.Contains(kriterier.Value)
                                                            ||c.Element("Telefonnummer").Value.Contains(kriterier.Value));

            XElement resultat = new XElement("Resultat", res);

            return resultat;
        }

        public void TaBortKontakt(int id)
        {
            foreach (var kontakt in allaKontakter.Elements("Kontakt").Where(c => c.Element("Id").Value == id.ToString())
            )
            {
                kontakt.Remove();
            }

            SaveAllaKontakter();
        }

        public void ÄndraKontakt(XElement ändraKontakt)
        {

            foreach (var kontakt in allaKontakter.Elements("Kontakt").Where(c => c.Element("Id").Value == ändraKontakt.Element("Id").Value))
            {
                kontakt.Element("Förnamn").Value = ändraKontakt.Element("Förnamn").Value;
                kontakt.Element("Efternamn").Value = ändraKontakt.Element("Efternamn").Value;
                kontakt.Element("Telefonnummer").Value = ändraKontakt.Element("Telefonnummer").Value;

            }

            SaveAllaKontakter();

        }

        public void SaveAllaKontakter()
        {
            allaKontakter.Save(appDataFolder + "telefonbok.xml");
        }
    }
}
