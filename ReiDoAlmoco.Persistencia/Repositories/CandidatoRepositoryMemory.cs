using ReiDoAlmoco.Models.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReiDoAlmoco.Persistencia.Repositories
{
    public class CandidatoRepositoryMemory : ICandidatoRepository<Candidato>
    {
        private MemoryDataSingleton dados;

        //Construtor
        public CandidatoRepositoryMemory()
        {
            dados = MemoryDataSingleton.Instance;
        }

        public void Alterar(Candidato entity)
        {
            throw new NotImplementedException();
        }

        public Candidato BuscarCandidatoPorEmail(string email)
        {
            foreach (Candidato c in dados.Candidatos)
            {
                if (c.CandidatoEmail == email)
                {
                    return c;
                }
            }
            return null;
        }

        public Candidato BuscarPorId(int id)
        {
            foreach (Candidato candidato in dados.Candidatos)
            {
                if (candidato.CandidatoId == id)
                {
                    return candidato;
                }
            }
            return null;
        }

        public void Deletar(int id)
        {
            throw new NotImplementedException();
        }

        public void Inserir(Candidato entity)
        {
            dados.CandidatosIdSequence++;
            entity.CandidatoId = dados.CandidatosIdSequence;
            dados.Candidatos.Add(entity);
        }

        public ICollection<Candidato> ListarTodos()
        {
            return dados.Candidatos;
        }
    }
}
