using ReiDoAlmoco.Models.Model;
using ReiDoAlmoco.Persistencia.UnitsOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReiDoAlmoco.RegrasDeNegocio
{
    public class CadastroCandidatoRN
    {
        private UnitOfWork unit;
        //Construtor
        public CadastroCandidatoRN()
        {
            unit = new UnitOfWork();
        }

        public bool EmailUnico(string email)
        {
            return unit.CandidatoRepository.BuscarCandidatoPorEmail(email) == null ? true : false;
        }

        public void CadastraCandidato(Candidato dados)
        {
            unit.CandidatoRepository.Inserir(dados);
        }

        public ICollection<Candidato> ListarCandidatos()
        {
            
            return unit.CandidatoRepository.ListarTodos();
        }
    }
}
