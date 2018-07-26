using ReiDoAlmoco.Models.Model;
using ReiDoAlmoco.Persistencia.UnitsOfWork;
using ReiDoAlmoco.RegrasDeNegocio.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mail;
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

        public Candidato RetornaReiDoAlmoco(DateTime horaAtual, bool notificar)
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

            //Notificar o Rei, caso não tenha sido ainda
            if (notificar)
            {
                NotificarReiDoAlmoco(reiAtual);
            }

            return reiAtual;
        }

        public IDictionary<string, Candidato> ListaReisUltimasSemanas()
        {
            //Último sábado (loop vai voltando)
            DateTime ponteiroDiaSemana = DateTime.Now.AddDays(((int)DateTime.Now.DayOfWeek + 1) * -1);

            IDictionary<string, Candidato> reis = new Dictionary<string, Candidato>();
            
            //Loop pelas semanas
            while (unit.VotoRepository.VotosHoje(ponteiroDiaSemana).Count > 0)
            {
                //Reis da semana
                ICollection<Candidato> reisDaSemana = new List<Candidato>();
                for (int d = 0; d < 7; d++)
                {
                    reisDaSemana.Add(RetornaReiDoAlmoco(ponteiroDiaSemana,false));
                    ponteiroDiaSemana = ponteiroDiaSemana.AddDays(-1);
                }
                //Descobrir qual foi o mais votado
                Candidato ReiDaSemana = null;
                int vezesReiNaSemana = 0;

                //Loop pelos reis da semana
                foreach (Candidato candidato in unit.CandidatoRepository.ListarTodos())
                {
                    int countVezesReiNaSemana = 0;
                    foreach (Candidato rei in reisDaSemana)
                    {
                        if (rei.CandidatoId == candidato.CandidatoId)
                        {
                            countVezesReiNaSemana++;
                        }
                    }

                    //Ver se esse rei foi mais vezes ecolhido na semana
                    if (vezesReiNaSemana < countVezesReiNaSemana)
                    {
                        ReiDaSemana = candidato;
                        vezesReiNaSemana = countVezesReiNaSemana;
                    }

                }


                DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
                DateTime primeiroDiaMes = new DateTime(ponteiroDiaSemana.Year, ponteiroDiaSemana.Month, 1);
                Calendar cal = dfi.Calendar;
                int semanaDoMes = (cal.GetWeekOfYear(ponteiroDiaSemana.AddDays(6), dfi.CalendarWeekRule, dfi.FirstDayOfWeek) - cal.GetWeekOfYear(primeiroDiaMes, dfi.CalendarWeekRule, dfi.FirstDayOfWeek) + 1);
                
                //Adicionar o resultado da semana na lista
                reis.Add(semanaDoMes + "° semana do mês " + ponteiroDiaSemana.AddDays(6).Month, ReiDaSemana);
            }
            
            return reis;
        }

        public IDictionary<string, Candidato> ListaReisMenosAmados()
        {
            //Último sábado (loop vai voltando)
            DateTime ponteiroDiaSemana = DateTime.Now.AddDays(((int)DateTime.Now.DayOfWeek + 1) * -1);

            IDictionary<string, Candidato> reis = new Dictionary<string, Candidato>();

            while (unit.VotoRepository.VotosHoje(ponteiroDiaSemana).Count > 0)
            {
                ICollection<Voto> votosSemana = unit.VotoRepository.BuscarVotosEntreDatas(ponteiroDiaSemana.AddDays(-6), ponteiroDiaSemana);
                ICollection<Candidato> candidatos = unit.CandidatoRepository.ListarTodos();

                int qtdVotosReiMenosAmado = -1;
                Candidato reiMenosAmado = null;

                foreach (Candidato candidato in candidatos)
                {
                    int qtdVotos = 0;

                    foreach (Voto v in votosSemana)
                    {
                        if(v.CandidatoId == candidato.CandidatoId)
                        {
                            qtdVotos++;
                        }
                    }

                    //Verificação
                    if(qtdVotosReiMenosAmado == -1)
                    {
                        //Primeiro Rei
                        qtdVotosReiMenosAmado = qtdVotos;
                        reiMenosAmado = candidato;
                    }
                    else
                    {
                        if(qtdVotosReiMenosAmado > qtdVotos)
                        {
                            qtdVotosReiMenosAmado = qtdVotos;
                            reiMenosAmado = candidato;
                        }
                    }

                }

                //Rei menos amado da semana
                DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
                DateTime primeiroDiaMes = new DateTime(ponteiroDiaSemana.Year, ponteiroDiaSemana.Month, 1);
                Calendar cal = dfi.Calendar;
                int semanaDoMes = (cal.GetWeekOfYear(ponteiroDiaSemana.AddDays(6), dfi.CalendarWeekRule, dfi.FirstDayOfWeek) - cal.GetWeekOfYear(primeiroDiaMes, dfi.CalendarWeekRule, dfi.FirstDayOfWeek) + 1);

                //Adicionar o resultado da semana na lista
                reis.Add(semanaDoMes + "° semana do mês " + ponteiroDiaSemana.AddDays(6).Month, reiMenosAmado);

                //Atualiza contador do while
                ponteiroDiaSemana = ponteiroDiaSemana.AddDays(-7);
            }


            return reis;
        }

        private void NotificarReiDoAlmoco(Candidato rei)
        {
            try
            {
                MailMessage mensagem = new MailMessage();
                mensagem.To.Add(rei.CandidatoEmail);
                mensagem.Body = "Você foi eleito o Rei Do Almoço hoje dia: " + DateTime.Now.Day + "/" + DateTime.Now.Month;
                mensagem.Subject = "Rei Do Almoço";

                EnviarEmail ee = new EnviarEmail();
                ee.Enviar(mensagem);
            }
            catch (Exception e)
            {
                //Não enviou o e-mail
            }
        }

        public void TesteInsertData()
        {
            //Insere candidatos para teste
            Candidato c1 = new Candidato()
            {
                CandidatoId = 1,
                CandidatoNome = "Candidato 1",
                CandidatoImgPath = "images/user-pic/default-user.png",
                CandidatoEmail = "cand1@contoso.com.br"
            };

            Candidato c2 = new Candidato()
            {
                CandidatoId = 2,
                CandidatoNome = "Candidato 2",
                CandidatoImgPath = "images/user-pic/default-user.png",
                CandidatoEmail = "cand@contoso.com.br"
            };

            Candidato c3 = new Candidato()
            {
                CandidatoId = 3,
                CandidatoNome = "Candidato 3",
                CandidatoImgPath = "images/user-pic/default-user.png",
                CandidatoEmail = "cand3@contoso.com.br"
            };

            unit.CandidatoRepository.Inserir(c1);
            unit.CandidatoRepository.Inserir(c2);
            unit.CandidatoRepository.Inserir(c3);


            //Insere votos para teste

            #region Votos_ontem
            unit.VotoRepository.Inserir(new Voto() {
                CandidatoId = 1,
                Candidato = c1,
                Timestamp = new DateTime(2018,07,26,11,15,0) //Ontem
            });

            unit.VotoRepository.Inserir(new Voto()
            {
                CandidatoId = 2,
                Candidato = c2,
                Timestamp = new DateTime(2018, 07, 26, 11, 15, 0) //Ontem
            });

            unit.VotoRepository.Inserir(new Voto()
            {
                CandidatoId = 1,
                Candidato = c1,
                Timestamp = new DateTime(2018, 07, 26, 11, 15, 0) //Ontem
            });

            unit.VotoRepository.Inserir(new Voto()
            {
                CandidatoId = 1,
                Candidato = c1,
                Timestamp = new DateTime(2018, 07, 26, 11, 15, 0) //Ontem
            });

            unit.VotoRepository.Inserir(new Voto()
            {
                CandidatoId = 3,
                Candidato = c3,
                Timestamp = new DateTime(2018, 07, 26, 11, 15, 0) //Ontem
            });

            unit.VotoRepository.Inserir(new Voto()
            {
                CandidatoId = 2,
                Candidato = c2,
                Timestamp = new DateTime(2018, 07, 26, 11, 15, 0) //Ontem
            });
            unit.VotoRepository.Inserir(new Voto()
            {
                CandidatoId = 1,
                Candidato = c1,
                Timestamp = new DateTime(2018, 07, 26, 11, 15, 0) //Ontem
            });
            #endregion

            //Insere votos para listas
            

            //For dia 15 até 21
            for (int d = 15; d < 22; d++)
            {
                for (int v = 0; v < 3; v++)
                {
                    unit.VotoRepository.Inserir(new Voto()
                    {
                        CandidatoId = 2,
                        Candidato = c2,
                        Timestamp = new DateTime(2018, 07, d, 11, 15, 0)
                    });
                }

                for (int v = 0; v < 2; v++)
                {
                    unit.VotoRepository.Inserir(new Voto()
                    {
                        CandidatoId = 1,
                        Candidato = c1,
                        Timestamp = new DateTime(2018, 07, d, 11, 15, 0)
                    });
                }

                unit.VotoRepository.Inserir(new Voto()
                {
                    CandidatoId = 3,
                    Candidato = c3,
                    Timestamp = new DateTime(2018, 07, d, 11, 15, 0)
                });
            }

            //For dia 8 até 15
            for (int d = 8; d < 15; d++)
            {
                for (int v = 0; v < 3; v++)
                {
                    unit.VotoRepository.Inserir(new Voto()
                    {
                        CandidatoId = 3,
                        Candidato = c3,
                        Timestamp = new DateTime(2018, 07, d, 11, 15, 0)
                    });
                }

                for (int v = 0; v < 2; v++)
                {
                    unit.VotoRepository.Inserir(new Voto()
                    {
                        CandidatoId = 2,
                        Candidato = c2,
                        Timestamp = new DateTime(2018, 07, d, 11, 15, 0)
                    });
                }

                unit.VotoRepository.Inserir(new Voto()
                {
                    CandidatoId = 1,
                    Candidato = c1,
                    Timestamp = new DateTime(2018, 07, d, 11, 15, 0)
                });
            }
        }

    }
}
