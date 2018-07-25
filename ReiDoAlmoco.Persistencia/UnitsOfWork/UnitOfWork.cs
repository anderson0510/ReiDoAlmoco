using ReiDoAlmoco.Models.Model;
using ReiDoAlmoco.Persistencia.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReiDoAlmoco.Persistencia.UnitsOfWork
{
    public class UnitOfWork
    {
        //Construtor
        public UnitOfWork()
        {

        }


        #region Candidato
        private ICandidatoRepository<Candidato> _candidatoRepository;

        public ICandidatoRepository<Candidato> CandidatoRepository
        {
            get
            {
                if(_candidatoRepository == null)
                {
                    _candidatoRepository = new CandidatoRepositoryMemory();
                }
                return _candidatoRepository;
            }
        }
        #endregion

        #region Voto
        private IVotoRepository<Voto> _votoRepository;

        public IVotoRepository<Voto> VotoRepository
        {
            get
            {
                if (_votoRepository == null)
                {
                    _votoRepository = new VotoRepositoryMemory();
                }
                return _votoRepository;
            }
        }
        #endregion
    }
}
