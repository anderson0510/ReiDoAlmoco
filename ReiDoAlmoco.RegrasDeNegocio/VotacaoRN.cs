using System;
using System.Collections.Generic;
using System.Text;

namespace ReiDoAlmoco.RegrasDeNegocio
{
    public class VotacaoRN
    {
        //Construtor
        public VotacaoRN()
        {

        }


        public bool VotacaoAberta(DateTime horaAtual)
        {           
            return horaAtual.Hour >= 10 && horaAtual.Hour <= 12 ? true : false;
        }
    }
}
