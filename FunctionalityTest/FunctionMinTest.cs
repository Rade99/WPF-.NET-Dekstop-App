using Common.DAO;
using Common.Functionality;
using Functionality.Exceptions;
using Functionality;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Exceptions;
using DAO;

namespace FunctionalityTest
{
    [TestFixture]
    class FunctionMinTest
    {

        Mock<IDAOMerenje> dAOMerenjamoq = new Mock<IDAOMerenje>();
        Mock<IDAORezultat> dAORezultatmoq = new Mock<IDAORezultat>();
        IFunctions testingObject;

        [SetUp]
        public void FunctionMin()
        {
             testingObject = new FunctionMin();
            
        }

        [Test]
        public void FunctionMinPositiveTest()
        {
            string datum = "2005-05-05 22:12 PM";

            dAORezultatmoq.Setup(t => t.PostojiMerenjeZaDatuSifruTogDana("sok", Convert.ToDateTime(datum))).Returns(false);

            IMerenje m1 = new Merenje();
            m1.dan = Convert.ToDateTime(datum);
            m1.nazivPodrucija = "Sokobanja";
            m1.vreme = Convert.ToDateTime(datum);
            m1.sifraPodrucija = "sok";
            m1.potrosnja = 300;


            IMerenje m2 = new Merenje();
            m2.dan = Convert.ToDateTime(datum);
            m2.nazivPodrucija = "Sokobanja";
            m2.vreme = Convert.ToDateTime(datum);
            m2.sifraPodrucija = "sok";
            m2.potrosnja = 200;

            List<IMerenje> merenja = new List<IMerenje>() {m1,m2};

            dAOMerenjamoq.Setup(t => t.GetPoslednjeMerenje("sok", Convert.ToDateTime(datum))).Returns(Convert.ToDateTime(datum));


            Assert.IsTrue(testingObject.Execute(merenja, dAOMerenjamoq.Object, dAORezultatmoq.Object));
           
        }

        [Test]
        public void FunctionMinPositiveReturnFalseTest()
        {
            string datum = "2005-05-05 22:12 PM";

           
            dAORezultatmoq.Setup(t => t.PostojiMerenjeZaDatuSifruTogDana("sok", Convert.ToDateTime(datum))).Returns(true);
            dAOMerenjamoq.Setup(t => t.PostojiMerenjeZaDatuSifruTogDana("sok", Convert.ToDateTime(datum))).Returns(false);

            IMerenje m1 = new Merenje();
            m1.dan = Convert.ToDateTime(datum);
            m1.nazivPodrucija = "Sokobanja";
            m1.vreme = Convert.ToDateTime(datum);
            m1.sifraPodrucija = "sok";
            m1.potrosnja = 300;


            IMerenje m2 = new Merenje();
            m2.dan = Convert.ToDateTime(datum);
            m2.nazivPodrucija = "Sokobanja";
            m2.vreme = Convert.ToDateTime(datum);
            m2.sifraPodrucija = "sok";
            m2.potrosnja = 200;

            List<IMerenje> merenja = new List<IMerenje>() { m1, m2 };

            dAOMerenjamoq.Setup(t => t.GetPoslednjeMerenje("sok", Convert.ToDateTime(datum))).Returns(Convert.ToDateTime(datum));


            Assert.IsFalse(testingObject.Execute(merenja, dAOMerenjamoq.Object, dAORezultatmoq.Object));

        }

        [Test]
        public void FunctionMinListaMerenjaPraznaExceptionNullTest() 
        {
          
            Assert.Throws<ListaMerenjaPraznaException>(() =>
            {
                testingObject = new FunctionMin();
                List<IMerenje> list = null;
                testingObject.Execute(list,dAOMerenjamoq.Object,dAORezultatmoq.Object);
            });
           
        }
           
        [Test]
        public void FunctionMinListaMerenjaPraznaExceptionTest() 
        {
            Assert.Throws<ListaMerenjaPraznaException>(() =>
            {
                testingObject = new FunctionMin();
                List<IMerenje> list = new List<IMerenje>();
                testingObject.Execute(list, dAOMerenjamoq.Object, dAORezultatmoq.Object);
            });

        }
                 
        [Test]
        public void FunctionMinNegativnaPotrosnjaTest()
        {

            Mock<IMerenje> merenjeamoq1 = new Mock<IMerenje>();

            merenjeamoq1.Setup(t => t.sifraPodrucija).Returns("voj");
            merenjeamoq1.Setup(t => t.potrosnja).Returns(-12);

            testingObject = new FunctionMin();
            List<IMerenje> list = new List<IMerenje>() { merenjeamoq1.Object };

            Assert.Throws<NegativnaPotrosnjaException>(() =>
            {
                testingObject.Execute(list, dAOMerenjamoq.Object, dAORezultatmoq.Object);
            });
        }

    }



}
