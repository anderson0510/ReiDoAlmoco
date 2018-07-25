using System;
using System.Collections.Generic;
using System.Text;

namespace ReiDoAlmoco.Models.Model
{
    public class Voto
    {
        public int VotoId { get; set; }

        public DateTime Timestamp { get; set; }

        public int CandidatoId { get; set; }

        public Candidato Candidato { get; set; }
    }
}
