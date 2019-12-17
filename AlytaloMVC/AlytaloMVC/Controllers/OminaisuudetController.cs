using AlytaloMVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace AlytaloMVC.Controllers
{
    public class OminaisuudetController : Controller
    {
        // GET: Ominaisuudet
        public ActionResult Index()
        {
            AlytaloEntities entities = new AlytaloEntities();


            var taulut = new OminaisuusViewModel
            {
                Saunat = entities.Sauna.ToList(),
                Termostaatit = entities.Termostaatti.ToList(),
                Valot = entities.Valot.ToList()

            };

            entities.Dispose();

            if (!taulut.Saunat.Any() && !taulut.Termostaatit.Any() && !taulut.Valot.Any())
            {
                return Content("<script language='javascript' type='text/javascript'>" +
                     "alert('Luo tietokantaan ensin ominaisuus');" +
                     "window.location.href ='/home/LuoOminaisuus' ;" +
                     "</script>");
            }
            return View(taulut);

        }

        public ActionResult SaunatPartial()
        {
            //AlytaloEntities entities = new AlytaloEntities();
            //var saunat = new OminaisuusViewModel { Saunat = entities.Sauna.ToList() };
            //entities.Dispose(); 
            Debug.WriteLine("SAUNAPARTIAL");
            return PartialView(); 
        }
        public ActionResult TermoPartial()
        {
            return PartialView();
        }
        public ActionResult ValotPartial()
        {
            return PartialView();
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


     }
}


