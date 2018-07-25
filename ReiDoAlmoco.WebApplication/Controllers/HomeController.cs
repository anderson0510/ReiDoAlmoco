using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReiDoAlmoco.Models.Model;
using ReiDoAlmoco.WebApplication.Models;
using ReiDoAlmoco.WebApplication.ViewModels;

namespace ReiDoAlmoco.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            HomeViewModel viewModel = new HomeViewModel();
            viewModel.VotacaoAberta = true;

            viewModel.Candidatos = new List<Candidato>() {
            new Candidato(){
                CandidatoId = 111,
                CandidatoNome = "Dona maria",
                CandidatoImgPath = "/images/food-img-01.jpg"
            } };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult RegistraVoto(int id)
        {
            return StatusCode(200);
        }
        

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
