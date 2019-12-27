using AlytaloMVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace AlytaloMVC.Controllers
{
    public class HomeController : Controller
    {
        private bool tapahtumiaTKssa;
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LuoOminaisuus()
        {
            return View();
        }
        [HttpPost]
        public JsonResult LisaaOminaisuus()
        {
            bool success = false;
            string error = "";
            string ominaisuus = "";

            AlytaloEntities entity = new AlytaloEntities();

            try
            {
                using (Stream receiveStream = Request.InputStream)
                {
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        ominaisuus = readStream.ReadToEnd();
                    }
                }

                ominaisuus = ominaisuus.Substring(1, ominaisuus.Length - 2);

                Ominaisuudet uusi = new Ominaisuudet();
                uusi.Nimi = ominaisuus;
                entity.Ominaisuudet.Add(uusi);
                entity.SaveChanges();
                success = true;
            }
            catch (Exception e)
            {
                error = e.GetType().Name + ": " + e.Message;
            }
            finally
            {
                entity.Dispose();
            }
            var x = new { ominaisuus = ominaisuus, success = success, error = error };
            return Json(x);
        }
        public ActionResult Tapahtumat()
        {
            AlytaloEntities entity = new AlytaloEntities();
            List<TapahtumatViewModel> model = new List<TapahtumatViewModel>();
            List<Loki> tiedot = entity.Loki.ToList();

            if (!tiedot.Any())
            {
                tapahtumiaTKssa = false;
                return Content("<script language='javascript' type='text/javascript'>" +
                                "alert('Tietokanta on tyhjä. Luo ensin ominaisuus tai säädä aikaisemmin luotua ominaisuutta ominaisuudet-välilehdeltä.');" +
                               "window.location.href ='/home/LuoOminaisuus' ;" +
                                "</script>");
            }
            else
            {
                tapahtumiaTKssa = true;
                foreach (Loki tieto in tiedot)
                {
                    TapahtumatViewModel view = new TapahtumatViewModel();
                    view.Id = tieto.Id;
                    view.OminaisuudenId = tieto.OminaisuusId;
                    view.Tapahtuma = tieto.Tapahtuma;
                    view.Ajankohta = tieto.Ajakohta.Value;
                    model.Add(view);
                }
            }
            entity.Dispose();
            return View(model);

        }
        public ActionResult DeleteTapahtuma(int? id)
        {
            AlytaloEntities entity = new AlytaloEntities();

            if (id != null)
            {
                Loki tieto = entity.Loki.Find(id);
                entity.Loki.Remove(tieto);
                entity.SaveChanges();

            }

            entity.Dispose();
            Tapahtumat();

            if (tapahtumiaTKssa)
            {
                return View("Tapahtumat");
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>" +
                    "alert('Tietokanta on tyhjä. Luo ensin ominaisuus tai säädä aikaisemmin luotua ominaisuudet-välilehdeltä.');" +
                    "window.location.href ='/home/LuoOminaisuus' ;" +
                    "</script>");
            }
        }

    }
}
