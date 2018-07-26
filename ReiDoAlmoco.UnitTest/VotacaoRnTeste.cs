using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReiDoAlmoco.RegrasDeNegocio;
using System;

namespace ReiDoAlmoco.UnitTest
{
    [TestClass]
    public class VotacaoRnTeste
    {
        public VotacaoRnTeste()
        {
            //Carrega dados para teste
            VotacaoRN rn = new VotacaoRN();
            rn.TesteInsertData();
        }

        #region VotacaoAberta(DateTime horaAtual)
        [TestMethod]
        public void Votacao_Aberta()
        {
            //Arrange
            VotacaoRN vrn = new VotacaoRN();
            DateTime data = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 11, 00, 00);
            //Act
            bool resultado = vrn.VotacaoAberta(data);
            //Assert
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void Votacao_Encerrada()
        {
            //Arrange
            VotacaoRN vrn = new VotacaoRN();
            DateTime data = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 14, 00, 00);
            //Act
            bool resultado = vrn.VotacaoAberta(data);
            //Assert
            Assert.IsFalse(resultado);
        }
        #endregion

        #region VotacaoHojeEncerrada(DateTime horaAtual)
        [TestMethod]
        public void VotacaoHoje_Aberta()
        {
            //Arrange
            VotacaoRN vrn = new VotacaoRN();
            DateTime data = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 11, 00, 00);
            //Act
            bool resultado = vrn.VotacaoHojeEncerrada(data);
            //Assert
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void VotacaoHoje_Encerrada()
        {
            //Arrange
            VotacaoRN vrn = new VotacaoRN();
            DateTime data = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 14, 00, 00);
            //Act
            bool resultado = vrn.VotacaoHojeEncerrada(data);
            //Assert
            Assert.IsFalse(resultado);
        }
        #endregion
               

    }
}
