using System;
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
            var cidadesFrias = new List<CidadeViewModel>();
            var cidadesQuentes = new List<CidadeViewModel>();
            var todasAsCidades = new List<Cidade>();

            using (var contexto = new PasseBemContexto())
            {
                contexto.Configuration.LazyLoadingEnabled = false;
                contexto.Configuration.ProxyCreationEnabled = false;

                cidadesFrias = contexto.PrevisaoClima
                    .AsNoTracking()
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
                    .AsNoTracking()
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

                todasAsCidades = contexto.Cidade
                    .AsNoTracking()
                    .ToList();



                contexto.Configuration.LazyLoadingEnabled = true;
                contexto.Configuration.ProxyCreationEnabled = true;
            }

            ViewBag.CidadesFrias = cidadesFrias;
            ViewBag.CidadesQuentes = cidadesQuentes;
            ViewBag.SelectListDetodasAsCidades = todasAsCidades;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public PartialViewResult ObterClimaSemanal(int idDaCidadeSelecionada)
        {
            var climaSemanalParaCidadeSelecionada = new List<ClimaSemanalParaCidadeSelecionadaViewModel>();

            using (var contexto = new PasseBemContexto())
            {
                contexto.Configuration.LazyLoadingEnabled = false;
                contexto.Configuration.ProxyCreationEnabled = false;

                climaSemanalParaCidadeSelecionada = contexto.PrevisaoClima
                    .AsNoTracking()
                    .Include("Cidade")
                    .OrderBy(x => x.DataPrevisao)
                    .Where(x => x.CidadeId == idDaCidadeSelecionada)
                    .Take(7)
                    .Select(x => new ClimaSemanalParaCidadeSelecionadaViewModel()
                    {
                        NomeDaCidade = x.Cidade.Nome,
                        DiaDaSemana = x.DataPrevisao.ToString(),
                        TempoClimatico = x.Clima,
                        TemperaturaMinima = x.TemperaturaMinima.Value,
                        TemperaturaMaxima = x.TemperaturaMaxima.Value
                    })
                    .ToList();

                contexto.Configuration.LazyLoadingEnabled = true;
                contexto.Configuration.ProxyCreationEnabled = true;
            }

            return PartialView("_ClimaSemanalParaCidadeSelecionada", climaSemanalParaCidadeSelecionada);
        }
    }
}