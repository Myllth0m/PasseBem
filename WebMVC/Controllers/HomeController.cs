using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebMVC.Data;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var cidade = new Cidade();
            
            using (var contexto = new PasseBemContexto())
            {
                cidade = contexto.Cidade.FirstOrDefault();
            }

            ViewBag.Cidade = cidade.Nome;
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}