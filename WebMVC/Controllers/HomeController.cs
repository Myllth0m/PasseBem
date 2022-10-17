using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FakerDotNet;
using WebMVC.Data;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var cidadesFrias = new List<CidadeViewModel>();
            var cidadesQuentes = new List<CidadeViewModel>();

            using (var contexto = new PasseBemContexto())
            {
                contexto.Configuration.LazyLoadingEnabled = false;
                contexto.Configuration.ProxyCreationEnabled = false;

                cidadesFrias = contexto.PrevisaoClima
                    .Include("Cidade")
                    .Include("Cidade.Estado")
                    .OrderBy(x => x.TemperaturaMinima)
                    .ThenBy(x => x.TemperaturaMaxima)
                    .Where(x => x.DataPrevisao == DateTime.Today)
                    .Take(3)
                    .Select(x => new CidadeViewModel()
                    {
                        Nome = x.Cidade.Nome,
                        UF = x.Cidade.Estado.UF,
                        TemperaturaMinima = x.TemperaturaMinima.Value,
                        TemperaturaMaxima = x.TemperaturaMaxima.Value
                    })
                    .ToList();

                cidadesQuentes = contexto.PrevisaoClima
                    .Include("Cidade")
                    .Include("Cidade.Estado")
                    .OrderByDescending(x => x.TemperaturaMinima)
                    .ThenByDescending(x => x.TemperaturaMaxima)
                    .Where(x => x.DataPrevisao == DateTime.Today)
                    .Take(3)
                    .Select(x => new CidadeViewModel()
                    {
                        Nome = x.Cidade.Nome,
                        UF = x.Cidade.Estado.UF,
                        TemperaturaMinima = x.TemperaturaMinima.Value,
                        TemperaturaMaxima = x.TemperaturaMaxima.Value
                    })
                    .ToList();

                contexto.Configuration.LazyLoadingEnabled = true;
                contexto.Configuration.ProxyCreationEnabled = true;
            }

            ViewBag.CidadesFrias = cidadesFrias;
            ViewBag.CidadesQuentes = cidadesQuentes;
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            //using (var contexto = new PasseBemContexto())
            //{
            //    var listaDePrevisoes = new List<PrevisaoClima>();

            //    for (int i = 1; i < 9; i++)
            //    {
            //        for (int j = 0; j < 9; j++)
            //        {
            //            var clima = Faker.Random.Element(new string[] { "instavel", "tempestuoso", "chuvoso", "nublado", "ensolarado" });
            //            var tempMin = Faker.Random.Element(new decimal[] { 8m, 9m, 10m, 11m, 12m, 13m, 14m, 15m, 16m, 17m, 18m, 19m, 20m, 21m, 22m, 23m, 24m, 25m, 26m, 27m, 28m, 29m });
            //            var tempMax = Faker.Random.Element(new decimal[] { 29m, 30m, 31m, 32m, 33m, 34m, 35m, 36m, 37m, 38m, 39m, 40m, 41m });

            //            listaDePrevisoes.Add(new PrevisaoClima()
            //            {
            //                CidadeId = i,
            //                Clima = clima,
            //                DataPrevisao = DateTime.Now.AddDays(j),
            //                TemperaturaMinima = tempMin,
            //                TemperaturaMaxima = tempMax
            //            });
            //        }
            //    }

            //    contexto.PrevisaoClima.AddRange(listaDePrevisoes);
            //    contexto.SaveChanges();
            //}

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}