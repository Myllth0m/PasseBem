using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace WebMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var estados = new List<Estado>();
            
            using (var contexto = new ClimaTempoSimplesEntities())
            {
                estados = contexto.Estado.ToList();
            }

            ViewBag.Estados = estados.ToString();
            
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