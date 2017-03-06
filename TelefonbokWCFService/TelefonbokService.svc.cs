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
                allaKontakter = XElement.Load(appDataFolder + "telefobok.xml");

            }

        }

        public XElement HämtaAllaKontakter()
        {
            return allaKontakter;
        }

        //Använd den XML som ges till metoden, ladda in hela XML-trädet
        //lägg till noden och spara sedan ned XML-trädet.

        public void LäggTillKontakt(XElement kontakt)
        {
            allaKontakter.Add(kontakt);
            SaveAllaKontakter();
        }

        //Ladda in alla kontakter från XML-trädet och använd sedan LINQ för filtrering.
        public XElement SökKontakter(XElement kriterier)
        {
            throw new NotImplementedException();
        }

        //Ladda in XML-trädet, leta upp rätt nod, ta bort noden och spara ner trädet
        public void TaBortKontakt(int id)
        {
            throw new NotImplementedException();
        }

        //Ladda in XML-trädet, leta upp rätt nod, byt ut dess innehåll mot det nya innehållet
        //(ReplaceContent) och spara sedan ned XML-trädet.
        public void ÄndraKontakt(XElement ändraKontakt)
        {
            
        }

        public void SaveAllaKontakter()
        {
            allaKontakter.Save(appDataFolder + "telefonbok.xml");
        }
    }
}
