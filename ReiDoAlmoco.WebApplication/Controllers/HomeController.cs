using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReiDoAlmoco.Models.Model;
using ReiDoAlmoco.RegrasDeNegocio;
using ReiDoAlmoco.WebApplication.Models;
using ReiDoAlmoco.WebApplication.ViewModels;

namespace ReiDoAlmoco.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private CadastroCandidatoRN ccrn = new CadastroCandidatoRN();
        private VotacaoRN vrn = new VotacaoRN();
        

        [HttpGet]
        public IActionResult Index()
        {
            HomeViewModel viewModel = new HomeViewModel();

            viewModel.VotacaoAberta = vrn.VotacaoAberta(DateTime.Now);
            viewModel.VotacaoHojeEncerrada = vrn.VotacaoHojeEncerrada(DateTime.Now);
            viewModel.Candidatos = ccrn.ListarCandidatos();

            if (vrn.VotacaoHojeEncerrada(DateTime.Now))
            {
                viewModel.ReiDeHoje = vrn.RetornaReiDoAlmoco(DateTime.Now,true);
            }

            viewModel.ReisUltimasSemanasList = vrn.ListaReisUltimasSemanas();
            viewModel.ReisMenosAmadosList = vrn.ListaReisMenosAmados();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult RegistraVoto(int id)
        {
            //Valida se o id recebido não é nulo.
            if (id > 0)
            {
                try
                {
                    vrn.RegistraVoto(id, DateTime.Now);
                    return StatusCode(200);
                }
                catch (Exception e)
                {
                    return StatusCode(500);
                }
            }
            else
            {
                return StatusCode(500);
            }

        }

        public IActionResult InsertFakeData()
        {
            vrn.TesteInsertData();
            return Redirect("/Home");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
