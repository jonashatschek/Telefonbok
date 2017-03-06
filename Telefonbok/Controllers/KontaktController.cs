using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Telefonbok.Models;
using Telefonbok.KontaktService;


namespace Telefonbok.Controllers
{
    public class KontaktController : Controller
    {
        //GET: /Kontakt/
        //private static IEnumerable<Kontakt> lagring;
        //private ITelefonbokService _client;
        TelefonbokServiceClient _client = new TelefonbokServiceClient();

        public ActionResult Index()
        {
            
            //_client.HämtaAllaKontakter();
            //lagring = Lagring.HämtaAllaKontakter();
            List <Kontakt> resultatLista = new List<Kontakt>();

            foreach (var kontakt in _client.HämtaAllaKontakter().Elements("Kontakt"))
            {
                resultatLista.Add(Kontakt.FromXml(kontakt));
            }
            return View(resultatLista);
        }

        //GET 
        //[HttpGet]
        public ActionResult LaggTillKontakt()
        {
            Kontakt kontakt = new Kontakt();
            return View(kontakt);
        }

        [HttpPost]
        public ActionResult LaggTillKontakt(Kontakt kontakt)
        {

            XElement returKontakt = kontakt.ToXml();
            _client.LäggTillKontakt(returKontakt);
            //_client.LäggTillKontakt(kontakt.ToXml());
            //Lagring.LäggTillKontakt(kontakt);
            return RedirectToAction("LaggTillKontakt");
        }

        [HttpPost]
        public ActionResult AndraKontakt(Kontakt kontakt)
        {
            Lagring.ÄndraKontakt(kontakt);
            return RedirectToAction("AndraKontakt");
            
        }
        
        public ActionResult AndraKontakt()
        {           
            return View();
        }      

        [HttpPost]
        public ActionResult SokKontakter(string sökning)
        {
           var kontakter =  Lagring.SökEfterKontakt(sökning);
            return View(kontakter);
        }

        public ActionResult SokKontakter()
        {
            var kontakter = Lagring.SökEfterKontakt("");
            return View(kontakter);
        }

        [HttpGet]
        public ActionResult TaBortKontakt(Kontakt kontakt)
        {
            Lagring.TaBortKontakt(kontakt.Id);
            return RedirectToAction("Index");
        }

        public ActionResult TaBortKontakt()
        {
            return View();
        }
    }
}
