using Common.DAO;
using Common.Functionality;
using DAO;
using DAO.Exceptions;
using Functionality;
using Functionality.Exceptions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalityTest
{
    [TestFixture]
    class FunctionDevijacijaTest
    {

        Mock<IDAOMerenje> dAOMerenjamoq = new Mock<IDAOMerenje>();
        Mock<IDAORezultat> dAORezultatmoq = new Mock<IDAORezultat>();
        IFunctions testingObject;

        [SetUp]
        public void FunctionMin()
        {
            testingObject = new FunctionDevijacija();

        }

        [Test]
        public void FunctionDevijacijaPositiveTest()
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

            IMerenje m3 = new Merenje();
            m3.dan = Convert.ToDateTime(datum);
            m3.nazivPodrucija = "Sokobanja";
            m3.vreme = Convert.ToDateTime(datum);
            m3.sifraPodrucija = "sok";
            m3.potrosnja = 190;


            IMerenje m4 = new Merenje();
            m4.dan = Convert.ToDateTime(datum);
            m4.nazivPodrucija = "Sokobanja";
            m4.vreme = Convert.ToDateTime(datum);
            m4.sifraPodrucija = "sok";
            m4.potrosnja = 320;

            List<IMerenje> merenja = new List<IMerenje>() { m1, m2,m3,m4 };

            dAOMerenjamoq.Setup(t => t.GetPoslednjeMerenje("sok", Convert.ToDateTime(datum))).Returns(Convert.ToDateTime(datum));


            Assert.IsTrue(testingObject.Execute(merenja, dAOMerenjamoq.Object, dAORezultatmoq.Object));

        }

        [Test]
        public void FunctionDevijacijaPositiveReturnFalseTest()
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


            List<IMerenje> merenja = new List<IMerenje>() { m1};

            dAOMerenjamoq.Setup(t => t.GetPoslednjeMerenje("sok", Convert.ToDateTime(datum))).Returns(Convert.ToDateTime(datum));


            Assert.IsFalse(testingObject.Execute(merenja, dAOMerenjamoq.Object, dAORezultatmoq.Object));

        }

        [Test]
        public void FunctionDevijacijaListaMerenjaPraznaExceptionNullTest()
        {

            Assert.Throws<ListaMerenjaPraznaException>(() =>
            {
                List<IMerenje> list = null;
                testingObject.Execute(list, dAOMerenjamoq.Object, dAORezultatmoq.Object);
            });

        }

        [Test]
        public void FunctionDevijacijaListaMerenjaPraznaExceptionTest()
        {
            Assert.Throws<ListaMerenjaPraznaException>(() =>
            {
                List<IMerenje> list = new List<IMerenje>();
                testingObject.Execute(list, dAOMerenjamoq.Object, dAORezultatmoq.Object);
            });

        }

        [Test]
        public void FunctionDevijacijanNegativnaPotrosnjaTest()
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
