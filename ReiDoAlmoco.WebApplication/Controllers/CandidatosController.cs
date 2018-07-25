
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
        private CadastroCandidatoRN ccrn = new CadastroCandidatoRN();
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
                if (!ccrn.EmailUnico(dados.Candidato.CandidatoEmail))
                {
                    ModelState.AddModelError("Candidato.CandidatoEmail", "E-mail já utilizado.");
                    return View(dados);
                }

                ccrn.CadastraCandidato(dados.Candidato);

                return Redirect("/Home");
            }
            else
            {
                return View(dados);
            }
            
        }
    }
}