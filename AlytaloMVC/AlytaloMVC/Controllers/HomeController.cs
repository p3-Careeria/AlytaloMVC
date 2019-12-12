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
        
            int length = (int)Request.InputStream.Length;
            byte[] buffer = new byte[length];
            int bytesRead = Request.InputStream.Read(buffer, 0, length);
            string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Debug.WriteLine("json: " + data);

            var result = new { success = "something", error = "nope" };
            return Json(result);
        }
    }
}