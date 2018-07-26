using ReiDoAlmoco.Models.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReiDoAlmoco.Persistencia.Repositories
{
    public class VotoRepositoryMemory : IVotoRepository<Voto>
    {
        private MemoryDataSingleton dados;

        //Construtor
        public VotoRepositoryMemory()
        {
            dados = MemoryDataSingleton.Instance;
        }

        public void Alterar(Voto entity)
        {
            throw new NotImplementedException();
        }

        public Voto BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Voto> BuscarVotosEntreDatas(DateTime de, DateTime ate)
        {
            ICollection<Voto> resultado = new List<Voto>();

            foreach (Voto v in dados.Votos)
            {
                if(v.Timestamp >= de && v.Timestamp <= ate)
                {
                    resultado.Add(v);
                }
            }
            return resultado;
        }

        public void Deletar(int id)
        {
            throw new NotImplementedException();
        }

        public void Inserir(Voto entity)
        {
            dados.VotosIdSequence++;
            entity.VotoId = dados.VotosIdSequence;
            dados.Votos.Add(entity);
        }

        public ICollection<Voto> ListarTodos()
        {
            return dados.Votos;
        }

        public ICollection<Voto> VotosHoje(DateTime hoje)
        {
            ICollection<Voto> resultado = new List<Voto>();

            foreach (Voto v in dados.Votos)
            {
                if(v.Timestamp.Day == hoje.Day && v.Timestamp.Month == hoje.Month && v.Timestamp.Year == hoje.Year)
                {
                    resultado.Add(v);
                }
            }

            return resultado;
        }
    }
}
