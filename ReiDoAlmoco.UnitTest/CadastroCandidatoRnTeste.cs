using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReiDoAlmoco.Models.Model;
using ReiDoAlmoco.Persistencia.UnitsOfWork;
using ReiDoAlmoco.RegrasDeNegocio;
using System.Collections;
using System.Collections.Generic;

namespace ReiDoAlmoco.UnitTest
{
    [TestClass]
    public class CadastroCandidatoRnTeste
    {
        public CadastroCandidatoRnTeste()
        {
            CadastroCandidatoRN ccrn = new CadastroCandidatoRN();

            ccrn.CadastraCandidato(new Candidato()
            {
                CandidatoNome = "UnitTest",
                CandidatoEmail = "unit.test@gmail.com",
            });
        }

        #region Metodo_EmailUnico()
        [TestMethod]
        public void Email_unico()
        {
            //Arrange
            CadastroCandidatoRN ccrn = new CadastroCandidatoRN();           

            //Act

            bool resultado = ccrn.EmailUnico("unit.test20@gmail.com");

            //Assert
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void Email_repetido()
        {
            //Arrange
            CadastroCandidatoRN ccrn = new CadastroCandidatoRN();            

            //Act

            bool resultado = ccrn.EmailUnico("unit.test@gmail.com");

            //Assert
            Assert.IsFalse(resultado);
        }
        #endregion

        #region Metodo_CadastraCandidato(Candidato dados)
        [TestMethod]
        public void Cadastra_Candidato_Valido()
        {
            //Arrange
            CadastroCandidatoRN ccrn = new CadastroCandidatoRN();

            //Act
            ccrn.CadastraCandidato(new Candidato()
            {
                CandidatoNome = "UnitTest2",
                CandidatoEmail = "unit.test2@gmail.com",
            });

            Candidato resultado = new UnitOfWork().CandidatoRepository.BuscarPorId(2);

            //Assert
            Assert.AreEqual(resultado.CandidatoNome, "UnitTest2");
        }
        #endregion

        #region ListarCandidatos()
        [TestMethod]
        public void Listar_Candidatos()
        {
            //Arrange
            CadastroCandidatoRN ccrn = new CadastroCandidatoRN();

            //Act            

            ICollection<Candidato> lista = ccrn.ListarCandidatos();

            //Assert
            Assert.AreEqual(lista.Count, 1);
        }
        #endregion
    }
}
