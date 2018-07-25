using ReiDoAlmoco.Models.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReiDoAlmoco.Persistencia
{
    public sealed class MemoryDataSingleton
    {
        private static volatile MemoryDataSingleton _instance;
        private static readonly object _syncLock = new object();

        //Construtor privado
        private MemoryDataSingleton()
        {
            if( this.Candidatos == null)
            {
                this.Candidatos = new List<Candidato>();
            }

            if (this.Votos == null)
            {
                this.Votos = new List<Voto>();
            }
        }

        public static MemoryDataSingleton Instance
        {
            get
            {
                if (_instance != null) return _instance;

                lock (_syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new MemoryDataSingleton();
                    }
                }
                return _instance;
            }
        }


        //Propriedades representando as classes do Model

        public ICollection<Candidato> Candidatos { get; set; }
        public ICollection<Voto> Votos { get; set; }

        public int CandidatosIdSequence { get; set; }
        public int VotosIdSequence { get; set; }

    }
}
