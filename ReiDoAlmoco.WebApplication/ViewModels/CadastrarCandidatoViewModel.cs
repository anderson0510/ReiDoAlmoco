using Microsoft.AspNetCore.Http;
using ReiDoAlmoco.Models.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReiDoAlmoco.WebApplication.ViewModels
{
    public class CadastrarCandidatoViewModel
    {
        public Candidato Candidato { get; set; }

        [Display(Name = "Foto de Perfil")]
        public IFormFile FotoPerfil { get; set; }
    }
}
