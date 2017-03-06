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

        TelefonbokServiceClient _client = new TelefonbokServiceClient();

        //GET: /Kontakt/
        public ActionResult Index()
        {
            
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

            XElement kontaktAttLäggaIn = kontakt.ToXml();
            _client.LäggTillKontakt(kontaktAttLäggaIn);

            return RedirectToAction("LaggTillKontakt");
        }

        [HttpPost]
        public ActionResult AndraKontakt(Kontakt kontakt)
        {
            _client.ÄndraKontakt(kontakt.ToXml());
            return RedirectToAction("AndraKontakt");
            
        }
        
        public ActionResult AndraKontakt(int id)
        {
            Kontakt kontakt = new Kontakt()
            {
                Id = id
            };

            return View(kontakt);
        }      

        [HttpPost]
        public ActionResult SokKontakter(string sökning)
        {

            XElement sökn = new XElement("Sökning", sökning);

            List<Kontakt> resultatLista = new List<Kontakt>();

            foreach (var kontakt in _client.SökKontakter(sökn).Elements("Kontakt"))
            {
                resultatLista.Add(Kontakt.FromXml(kontakt));
            }

            return View(resultatLista);
        }

        public ActionResult SokKontakter()
        {
            var kontakter = Lagring.SökEfterKontakt("");
            return View(kontakter);
        }

        [HttpGet]
        public ActionResult TaBortKontakt(Kontakt kontakt)
        {
            _client.TaBortKontakt(kontakt.Id);
            return RedirectToAction("Index");
        }

        public ActionResult TaBortKontakt()
        {
            return View();
        }
    }
}
