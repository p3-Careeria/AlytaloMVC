using AlytaloMVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AlytaloMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LuoTalo()
        {
            return View();
        }
        [HttpPost]
        public JsonResult LuoUusiTalo()
        {
            bool success = false;
            string error = "";

            int length = (int)Request.InputStream.Length;
            byte[] buffer = new byte[length];
            int bytesRead = Request.InputStream.Read(buffer, 0, length);
            string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            // Onko tarpeellinen?: if data = "null" tjsp -> return 

            Debug.WriteLine("json: " + data);
            TaloViewModel input = JsonConvert.DeserializeObject<TaloViewModel>(data);
            AlytaloEntities entity = new AlytaloEntities();

            try
            {
                Talo newTalo = new Talo();
                newTalo.Nimi = input.Nimi;
                newTalo.Osoite = input.Osoite;

                entity.Talo.Add(newTalo);
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

            var result = new { success = success, error = error };
            return Json(result);
        }

        public ActionResult Talot()
        {
            AlytaloEntities entity = new AlytaloEntities();

            List<TaloViewModel> model = new List<TaloViewModel>();
            List<Talo> talot = entity.Talo.ToList();

            if (!talot.Any())
            {
                return Content("<script language='javascript' type='text/javascript'>" +
                                "alert('Luo tietokantaan ensin talo');" +
                                "window.location.href ='/home/LuoTalo' ;"+
                                "</script>");

            } else
            {



            }
            return View();


        }


    }
}