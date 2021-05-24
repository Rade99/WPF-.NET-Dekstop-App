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
        /*
                [SetUp]
                public void FunctionDevijacija()
                {
                    IFunctions testingObject = new FunctionDevijacija();

                }

                [Test]
                public void FunctionDevijacijaListaMerenjaPraznaExceptionNullGetMinTest() // nem pojma
                {
                    testingObject = new FunctionDevijacija();
                    List<IMerenje> list = null;

                    Assert.Throws<ListaMerenjaPraznaException>(() =>
                    {
                        testingObject.Execute(list);
                    });

                }


                [Test]
                public void FunctionMinListaMerenjaPraznaExceptionTest() // nem pojma
                {
                    Assert.Throws<ListaMerenjaPraznaException>(() =>
                    {
                        testingObject = new FunctionDevijacija();
                        List<IMerenje> list = new List<IMerenje>();
                        testingObject.Execute(list);
                    });

                }

                [Test]
                public void FunctionDevijacijaPositiveTest() // nem pojma
                {
                    Mock<IMerenje> merenjeamoq1 = new Mock<IMerenje>();
                    Mock<IMerenje> merenjeamoq2 = new Mock<IMerenje>();
                    Mock<IMerenje> merenjeamoq3 = new Mock<IMerenje>();
                    Mock<IMerenje> merenjeamoq4 = new Mock<IMerenje>();


                    merenjeamoq1.Setup(t => t.potrosnja).Returns(13);
                    merenjeamoq1.Setup(t => t.sifraPodrucija).Returns("voj");
                    merenjeamoq1.Setup(t => t.nazivPodrucija).Returns("Vojvodina");
                    merenjeamoq2.Setup(t => t.sifraPodrucija).Returns("voj");
                    merenjeamoq2.Setup(t => t.potrosnja).Returns(12);
                    merenjeamoq2.Setup(t => t.nazivPodrucija).Returns("Vojvodina");
                    merenjeamoq3.Setup(t => t.potrosnja).Returns(14);
                    merenjeamoq3.Setup(t => t.sifraPodrucija).Returns("voj");
                    merenjeamoq3.Setup(t => t.nazivPodrucija).Returns("Vojvodina");
                    merenjeamoq4.Setup(t => t.sifraPodrucija).Returns("voj");
                    merenjeamoq4.Setup(t => t.potrosnja).Returns(11);
                    merenjeamoq4.Setup(t => t.nazivPodrucija).Returns("Vojvodina");




                    testingObject = new FunctionDevijacija();
                    List<IMerenje> list = new List<IMerenje>() { merenjeamoq1.Object, merenjeamoq2.Object,merenjeamoq3.Object,merenjeamoq4.Object };

                    Assert.DoesNotThrow(() => testingObject.Execute(list));


                }

                [Test]
                public void FunctionDevijacijaSifraIsNotInDBSet()
                {
                    Mock<IMerenje> merenjeamoq1 = new Mock<IMerenje>();
                    Mock<IMerenje> merenjeamoq2 = new Mock<IMerenje>();


                    merenjeamoq1.Setup(t => t.potrosnja).Returns(13);
                    merenjeamoq1.Setup(t => t.sifraPodrucija).Returns("sok");
                    merenjeamoq1.Setup(t => t.nazivPodrucija).Returns("Sokobanja");
                    merenjeamoq2.Setup(t => t.sifraPodrucija).Returns("voj");
                    merenjeamoq2.Setup(t => t.potrosnja).Returns(12);
                    merenjeamoq2.Setup(t => t.nazivPodrucija).Returns("Vojvodina");




                    testingObject = new FunctionDevijacija();
                    List<IMerenje> list = new List<IMerenje>() { merenjeamoq1.Object, merenjeamoq2.Object };


                    Assert.Throws<SifraNijePronadjenaException>(() =>
                    {
                        testingObject.Execute(list);
                    });
                }


                [Test]
                public void FunctionDevijacijaNegativnaPotrosnjaTest()
                {

                    Mock<IMerenje> merenjeamoq1 = new Mock<IMerenje>();

                    merenjeamoq1.Setup(t => t.sifraPodrucija).Returns("voj");
                    merenjeamoq1.Setup(t => t.potrosnja).Returns(-12);


                    testingObject = new FunctionDevijacija();
                    List<IMerenje> list = new List<IMerenje>() { merenjeamoq1.Object };

                    Assert.Throws<NegativnaPotrosnjaException>(() =>
                    {
                        testingObject.Execute(list);
                    });
                }*/

    }

}
