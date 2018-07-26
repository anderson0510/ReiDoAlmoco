
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ReiDoAlmoco.RegrasDeNegocio;
using ReiDoAlmoco.WebApplication.ViewModels;

namespace ReiDoAlmoco.WebApplication.Controllers
{
    public class CandidatosController : Controller
    {
        private CadastroCandidatoRN ccrn = new CadastroCandidatoRN();
        private readonly IHostingEnvironment hostingEnvironment;
        public CandidatosController(IHostingEnvironment environment)
        {
            hostingEnvironment = environment;
        }



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

                
                if (dados.FotoPerfil != null)
                {                    
                    var uploads = Path.Combine(hostingEnvironment.WebRootPath, "images/user-pic");
                    var filePath = Path.Combine(uploads, dados.FotoPerfil.FileName);

                    dados.FotoPerfil.CopyTo(new FileStream(filePath, FileMode.Create));

                    dados.Candidato.CandidatoImgPath = "images/user-pic/" + dados.FotoPerfil.FileName;
                }
                else
                {
                    dados.Candidato.CandidatoImgPath = "images/user-pic/default-user.png";
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