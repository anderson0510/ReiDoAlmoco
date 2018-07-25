using ReiDoAlmoco.Models.Model;
using ReiDoAlmoco.Persistencia.UnitsOfWork;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ReiDoAlmoco.RegrasDeNegocio
{
    public class VotacaoRN
    {
        private UnitOfWork unit;

        //Construtor
        public VotacaoRN()
        {
            unit = new UnitOfWork();
        }


        public bool VotacaoAberta(DateTime horaAtual)
        {
            return horaAtual.Hour >= 10 && horaAtual.Hour <= 12 ? true : false;
        }

        public bool VotacaoHojeEncerrada(DateTime horaAtual)
        {
            return horaAtual.Hour > 12 ? true : false;
        }

        public void RegistraVoto(int candidatoId, DateTime horaAtual)
        {
            try
            {
                //Valida se a horário do voto está dentro do range.
                if (VotacaoAberta(horaAtual))
                {
                    Voto voto = new Voto()
                    {
                        Timestamp = horaAtual,
                        CandidatoId = candidatoId,
                        Candidato = unit.CandidatoRepository.BuscarPorId(candidatoId)
                    };
                    unit.VotoRepository.Inserir(voto);
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }

            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível salvar o voto.");
            }
        }

        public Candidato RetornaReiDoAlmoco(DateTime horaAtual)
        {
            ICollection<Candidato> candidatos = unit.CandidatoRepository.ListarTodos();
            ICollection<Voto> votos = unit.VotoRepository.VotosHoje(horaAtual);

            Candidato reiAtual = null;
            int reiAtualQtdVotos = 0;

            foreach (Candidato rei in candidatos)
            {
                int qtdVotos = 0;

                foreach (Voto voto in votos)
                {
                    if (voto.CandidatoId == rei.CandidatoId)
                    {
                        qtdVotos++;
                    }
                }

                if(reiAtualQtdVotos < qtdVotos)
                {
                    reiAtual = rei;
                    reiAtualQtdVotos = qtdVotos;
                }
            }
            
            return reiAtual;
        }

        public void ListaReisUltimasSemanas()
        {
            ICollection<Voto> dados = unit.VotoRepository.ListarTodos();
            //DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            //DateTime date1 = new DateTime(2011, 1, 1);
            //Calendar cal = dfi.Calendar;

            //Console.WriteLine("{0:d}: Week {1} ({2})", date1,
            //                  cal.GetWeekOfYear(date1, dfi.CalendarWeekRule,
            //                                    dfi.FirstDayOfWeek),
            //                  cal.ToString().Substring(cal.ToString().LastIndexOf(".") + 1));

        }

    }
}
