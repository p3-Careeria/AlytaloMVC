using AlytaloMVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Threading;

namespace AlytaloMVC.Controllers
{
    public class OminaisuudetController : Controller
    {
        private int talonLampo = 20;

        // Tarvitsetko edes toista ajastinta? laitat alla olevaan metodikutsun joka 
        // lisää arvoja tietyn ehdon verran ... 

        // VITTTOOOO .. ei voi tallentaa uutta arvoa koska toi getOminaisuudet jää käyntiin 
        // säätö uuteen ikkunaan, jolloin toi getOminaisuudet pysähtyy siksi aikaa?? 
        // GET: Ominaisuudet
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
                if (sauna.Kaynnissa.Value && sauna.Lampo<100)
                {
                    int uusiLampo = sauna.Lampo.Value + 1;
                    sauna.Lampo = uusiLampo;
                } else if (!sauna.Kaynnissa.Value && sauna.Lampo>talonLampo) {
                    int uusiLampo = sauna.Lampo.Value - 1;
                    sauna.Lampo = uusiLampo;
                }

            }
            entities.SaveChanges();
            entities.Dispose(); 
        }


    }

}

