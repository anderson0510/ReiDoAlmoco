using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ReiDoAlmoco.Models.Model
{
    public class Candidato
    {
        public int CandidatoId { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public String CandidatoNome { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Campo obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato inválido.")]
        public String CandidatoEmail { get; set; }
        
        public String CandidatoImgPath { get; set; }
    }
}
