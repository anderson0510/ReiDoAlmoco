
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReiDoAlmoco.RegrasDeNegocio;
using ReiDoAlmoco.WebApplication.ViewModels;

namespace ReiDoAlmoco.WebApplication.Controllers
{
    public class CandidatosController : Controller
    {
        [HttpGet]
        public IActionResult Cadastrar()
        {
            CadastrarCandidatoViewModel viewModel = new CadastrarCandidatoViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cadastrar(CadastrarCandidatoViewModel dados)
        {
            if (ModelState.IsValid)
            {
                //Valida se o e-mail informado é único.
                CadastroCandidatoRN ccrn = new CadastroCandidatoRN();
                if (!ccrn.EmailUnico(dados.Candidato.CandidatoEmail))
                {
                    ModelState.AddModelError("Candidato.CandidatoEmail", "E-mail já utilizado.");
                    return View(dados);
                }



                return Redirect("/Home");
            }
            else
            {
                return View(dados);
            }
            
        }
    }
}