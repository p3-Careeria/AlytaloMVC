using AlytaloMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Windows.Threading;

namespace AlytaloMVC.Controllers
{
    public class OminaisuudetController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetOminaisuudet()
        {

            AlytaloEntities entities = new AlytaloEntities();

            var taulut = new OminaisuusViewModel
            {
                Saunat = entities.Sauna.ToList(),
                Termostaatit = entities.Termostaatti.ToList(),
                Valot = entities.Valot.ToList()

            };
            if (!taulut.Saunat.Any() && !taulut.Termostaatit.Any() && !taulut.Valot.Any())
            {
                return Content("<script language='javascript' type='text/javascript'>" +
                     "alert('Luo tietokantaan ensin ominaisuus');" +
                     "window.location.href ='/home/LuoOminaisuus' ;" +
                     "</script>");
            }

            entities.Dispose();
            SaunaLampo();
            TermoLampo();
            return PartialView("OminaisuudetPartial", taulut);

        }
        public ActionResult PoistaOminaisuus(int? id)
        {
            if (id != null)
            {
                AlytaloEntities entities = new AlytaloEntities();
                Ominaisuudet ominaisuudet = entities.Ominaisuudet.Find(id);
                entities.Ominaisuudet.Remove(ominaisuudet);
                entities.SaveChanges();
                entities.Dispose();

            }

            Index();
            return Content("<script language='javascript' type='text/javascript'>" +
                            "window.location.href ='/Ominaisuudet' ;" +
                              "</script>");
        }
        public ActionResult SetSauna(int? id)
        {
            AlytaloEntities entities = new AlytaloEntities();
            Sauna sauna = entities.Sauna.Find(id);
            if (sauna == null)
            {
                return HttpNotFound();
            }
            else
            {
                Loki uusiKirjaus = new Loki();
                uusiKirjaus.OminaisuusId = sauna.OminaisuusId.Value;
                uusiKirjaus.Ajakohta = DateTime.Now;

                if (!sauna.Kaynnissa.Value)
                {
                    uusiKirjaus.Tapahtuma = "Sauna käynnistetty";
                    sauna.Kaynnissa = true;

                }
                else if (sauna.Kaynnissa.Value)
                {
                    uusiKirjaus.Tapahtuma = "Sauna sammutettu";
                    sauna.Kaynnissa = false;
                }
                entities.Loki.Add(uusiKirjaus);
                entities.SaveChanges();
            }
            return View("Index");
        }
        private void SaunaLampo()
        {
            AlytaloEntities entities = new AlytaloEntities();
            foreach (Sauna sauna in entities.Sauna)
            {
                if (sauna.Kaynnissa.Value && sauna.Lampo < 100)
                {
                    int uusiLampo = sauna.Lampo.Value + 1;
                    sauna.Lampo = uusiLampo;
                }
                else if (!sauna.Kaynnissa.Value && sauna.Lampo > TalonLampotila())
                {
                    int uusiLampo = sauna.Lampo.Value - 1;
                    sauna.Lampo = uusiLampo;
                }
                else
                {
                    sauna.Lampo = TalonLampotila();
                }
            }
            entities.SaveChanges();
            entities.Dispose();
        }
        public ActionResult SaadaTermo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlytaloEntities entities = new AlytaloEntities();
            Termostaatti termostaatti = entities.Termostaatti.Find(id);
            if (termostaatti == null)
            {
                return HttpNotFound();
            }
            entities.Dispose();
            return View(termostaatti);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaadaTermo([Bind(Include = "TermoId, OminaisuusId, Lampo, Tavoite")] Termostaatti termostaatti)
        {

            AlytaloEntities entities = new AlytaloEntities();


            if (ModelState.IsValid)
            {

                entities.Entry(termostaatti).State = EntityState.Modified;

                Loki uusiKirjaus = new Loki
                {
                    OminaisuusId = termostaatti.OminaisuusId.Value,
                    Tapahtuma = "Termostaatille asetettu uusi tavoitelämpötila",
                    Ajakohta = DateTime.Now
                };
                entities.Loki.Add(uusiKirjaus);
                entities.SaveChanges();
            }

            entities.Dispose();
            return View("Index");
        }
        private void TermoLampo()
        {
            AlytaloEntities entities = new AlytaloEntities();
            foreach (Termostaatti termostaatti in entities.Termostaatti)
            {
                if (termostaatti.Lampo.Value < termostaatti.Tavoite.Value)  // heitä tämä toisto parametrilliseen metodiin jossa parametriks - tai +1, palautus arvoksiko int (?)  
                {                                                           // mites Or || tähän kohtaan jolloin loki kirjaantuu vain kerran? 
                    int uusiLampo = termostaatti.Lampo.Value + 1;
                    termostaatti.Lampo = uusiLampo;

                    if (termostaatti.Lampo.Value == termostaatti.Tavoite.Value)
                    {
                        Loki uusiKirjaus = new Loki
                        {
                            OminaisuusId = termostaatti.OminaisuusId.Value,
                            Tapahtuma = "Termostaatin tavoitelämpötila "+termostaatti.Lampo + "°C saavutettu",
                            Ajakohta = DateTime.Now
                        };
                        entities.Loki.Add(uusiKirjaus);
                    }
                }
                else if (termostaatti.Lampo.Value > termostaatti.Tavoite.Value)
                {
                    int uusiLampo = termostaatti.Lampo.Value - 1;
                    termostaatti.Lampo = uusiLampo;

                    if (termostaatti.Lampo.Value == termostaatti.Tavoite.Value)
                    {
                        Loki uusiKirjaus = new Loki
                        {
                            OminaisuusId = termostaatti.OminaisuusId.Value,
                            Tapahtuma = "Termostaatin tavoitelämpötila saavutettu",
                            Ajakohta = DateTime.Now
                        };
                        entities.Loki.Add(uusiKirjaus);
                    }
                }


            }
            entities.SaveChanges();
            entities.Dispose();
        }
        public ActionResult SaadaValo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlytaloEntities entities = new AlytaloEntities();
            Valot valo = entities.Valot.Find(id);
            if (valo == null)
            {
                return HttpNotFound();
            }
            entities.Dispose();
            return View(valo);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaadaValo([Bind(Include = "ValotId, OminaisuusId, Kaynnissa, Teho")] Valot valo)
        {
            AlytaloEntities entities = new AlytaloEntities();

            if (ModelState.IsValid)
            {
                Loki uusikirjaus = new Loki();
                uusikirjaus.OminaisuusId = valo.OminaisuusId.Value;
                uusikirjaus.Ajakohta = DateTime.Now;

                if (valo.Teho.Value > 0)
                {
                    valo.Kaynnissa = true;
                    uusikirjaus.Tapahtuma = "Valon teho muutettu arvoon " + valo.Teho.Value.ToString();

                }
                else
                {
                    valo.Kaynnissa = false;
                    uusikirjaus.Tapahtuma = "Valo sammutettu";
                 
                }
                entities.Loki.Add(uusikirjaus);
            }
            entities.Entry(valo).State = EntityState.Modified;
            entities.SaveChanges();

            return View("Index");


        }
        private int TalonLampotila()
        {
            int i = 0;
            int lampotila = 0;
            AlytaloEntities entities = new AlytaloEntities();
            foreach (Termostaatti termostaatti in entities.Termostaatti)
            {
                if (termostaatti.Lampo.HasValue)
                {
                    lampotila += termostaatti.Lampo.Value;
                    i++;
                }
            }
            if (i != 0)
            {
                return lampotila /= i;
            }
            return 20;
        }
    }
}
