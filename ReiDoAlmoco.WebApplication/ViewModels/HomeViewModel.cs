using ReiDoAlmoco.Models.Model;
using ReiDoAlmoco.WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReiDoAlmoco.WebApplication.ViewModels
{
    public class HomeViewModel
    {
        public bool VotacaoAberta { get; set; }
        public bool VotacaoHojeEncerrada { get; set; }

        public Candidato ReiDeHoje { get; set; }

        public ICollection<Candidato> Candidatos { get; set; }

        public IDictionary<string, Candidato> ReisUltimasSemanasList { get; set; }
        public IDictionary<string, Candidato> ReisMenosAmadosList { get; set; }
    }
}
