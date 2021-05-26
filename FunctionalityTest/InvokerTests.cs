using Common.DAO;
using Common.Functionality;
using Common.ResidentContract;
using DAO;
using DAO.Exceptions;
using Functionality;
using Functionality.Exceptions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace FunctionalityTest
{
    [TestFixture]
    public class InvokerTests
    {
       
        private IInvoker testingObject;
        DateTime datetime;
        Mock<ICsvInput> csvParamsmoq;
        Mock<IDAOMerenje> dAOMerenjamoq = new Mock<IDAOMerenje>();
        Mock<IDAORezultat> dAORezultatmoq = new Mock<IDAORezultat>();


        [SetUp]
        public void Functionality()
        {
            
            testingObject = new Invoker();
            datetime = DateTime.Now;
        }

        
        [Test]
        public void InvokeResidentFun1PositiveTest() 
        {
            string datum = "2005-05-05 22:12 PM";

            

            dAORezultatmoq.Setup(t => t.PostojiMerenjeZaDatuSifruTogDana("voj", Convert.ToDateTime(datum))).Returns(false);

            csvParamsmoq = new Mock<ICsvInput>();

            csvParamsmoq.Setup(t => t.csvPodrucja).Returns(new List<string> { "voj" });
            csvParamsmoq.Setup(t => t.csvFunkcije).Returns(new Dictionary<int, int> { { 1, 1 }, { 2, 0 }, { 3, 0 } });

            Merenje m1 = new Merenje();
            m1.dan = Convert.ToDateTime(datum);
            m1.nazivPodrucija = "Vojvodina";
            m1.vreme = Convert.ToDateTime(datum);
            m1.sifraPodrucija = "voj";
            m1.potrosnja = 200;

            List<IMerenje> listamerenja = new List<IMerenje>(){m1 };
            

            dAOMerenjamoq.Setup(t => t.GetMerenjaPoDanuIVremenu(DateTime.Now.Date, "voj")).Returns(listamerenja);
            dAOMerenjamoq.Setup(t => t.GetPoslednjeMerenje("voj", Convert.ToDateTime(datum))).Returns(Convert.ToDateTime(datum));

            testingObject.InvokeResident(dAOMerenjamoq.Object, csvParamsmoq.Object, dAORezultatmoq.Object);

            Assert.Multiple(() =>
            {
                foreach (var bol in testingObject.UspenoIzvrseneFunkcije)
                {
                    Assert.IsTrue(bol);
                }
            });


        }

        [Test]
        public void InvokeResidentFun2PositiveTest() 
        {

            string datum = "2005-05-05 22:12 PM";

            dAORezultatmoq.Setup(t => t.PostojiMerenjeZaDatuSifruTogDana("voj", Convert.ToDateTime(datum))).Returns(false);

            csvParamsmoq = new Mock<ICsvInput>();

            csvParamsmoq.Setup(t => t.csvPodrucja).Returns(new List<string> { "voj" });
            csvParamsmoq.Setup(t => t.csvFunkcije).Returns(new Dictionary<int, int> { { 1, 0 }, { 2, 1 }, { 3, 0 } });

            Merenje m1 = new Merenje();
            m1.dan = Convert.ToDateTime(datum);
            m1.nazivPodrucija = "Vojvodina";
            m1.vreme = Convert.ToDateTime(datum);
            m1.sifraPodrucija = "voj";
            m1.potrosnja = 200;

            List<IMerenje> listamerenja = new List<IMerenje>() { m1 };


            dAOMerenjamoq.Setup(t => t.GetMerenjaPoDanuIVremenu(DateTime.Now.Date, "voj")).Returns(listamerenja);//jer prosledjujem datetimenow
            dAOMerenjamoq.Setup(t => t.GetPoslednjeMerenje("voj", Convert.ToDateTime(datum))).Returns(Convert.ToDateTime(datum));

            testingObject.InvokeResident(dAOMerenjamoq.Object, csvParamsmoq.Object, dAORezultatmoq.Object);

            Assert.Multiple(() =>
            {
                foreach (var bol in testingObject.UspenoIzvrseneFunkcije)
                {
                    Assert.IsTrue(bol);
                }
            });
        }

        [Test]
        public void InvokeResidentFun3InvokeSinglePositiveTest() 
        {

            string datum = "2005-05-05 22:12 PM";

            dAORezultatmoq.Setup(t => t.PostojiMerenjeZaDatuSifruTogDana("voj", Convert.ToDateTime(datum))).Returns(false);

            csvParamsmoq = new Mock<ICsvInput>();

            csvParamsmoq.Setup(t => t.csvPodrucja).Returns(new List<string> { "voj" });
            csvParamsmoq.Setup(t => t.csvFunkcije).Returns(new Dictionary<int, int> { { 1, 0 }, { 2, 0 }, { 3, 1 } });

            Merenje m1 = new Merenje();
            m1.dan = Convert.ToDateTime(datum);
            m1.nazivPodrucija = "Vojvodina";
            m1.vreme = Convert.ToDateTime(datum);
            m1.sifraPodrucija = "voj";
            m1.potrosnja = 200;

            List<IMerenje> listamerenja = new List<IMerenje>() { m1 };


            dAOMerenjamoq.Setup(t => t.GetMerenjaPoDanuIVremenu(DateTime.Now.Date, "voj")).Returns(listamerenja);
            dAOMerenjamoq.Setup(t => t.GetPoslednjeMerenje("voj", Convert.ToDateTime(datum))).Returns(Convert.ToDateTime(datum));

            testingObject.InvokeResident(dAOMerenjamoq.Object, csvParamsmoq.Object, dAORezultatmoq.Object);

            Assert.Multiple(() =>
            {
                foreach (var bol in testingObject.UspenoIzvrseneFunkcije)
                {
                    Assert.IsTrue(bol);
                }
            });
        }

        [Test]
        public void InvokeResidentMultipleFunctionsPositiveTest()
        {

            string datum = "2005-05-05 22:12 PM";

            dAORezultatmoq.Setup(t => t.PostojiMerenjeZaDatuSifruTogDana("voj", Convert.ToDateTime(datum))).Returns(false);
            dAORezultatmoq.Setup(t => t.PostojiMerenjeZaDatuSifruTogDana("sok", Convert.ToDateTime(datum))).Returns(false);


            csvParamsmoq = new Mock<ICsvInput>();

            csvParamsmoq.Setup(t => t.csvPodrucja).Returns(new List<string> { "voj","sok","ras"});
            csvParamsmoq.Setup(t => t.csvFunkcije).Returns(new Dictionary<int, int> { { 1, 1 }, { 2, 1 }, { 3, 1 } });

            Merenje m1 = new Merenje();
            m1.dan = Convert.ToDateTime(datum);
            m1.nazivPodrucija = "Vojvodina";
            m1.vreme = Convert.ToDateTime(datum);
            m1.sifraPodrucija = "voj";
            m1.potrosnja = 200;

            Merenje m2 = new Merenje();
            m2.dan = Convert.ToDateTime(datum);
            m2.nazivPodrucija = "Sokobanja";
            m2.vreme = Convert.ToDateTime(datum);
            m2.sifraPodrucija = "sok";
            m2.potrosnja = 300;




            dAOMerenjamoq.Setup(t => t.GetMerenjaPoDanuIVremenu(DateTime.Now.Date, "voj")).Returns(new List<IMerenje>() {m1});
            dAOMerenjamoq.Setup(t => t.GetPoslednjeMerenje("voj", Convert.ToDateTime(datum))).Returns(Convert.ToDateTime(datum));
            dAOMerenjamoq.Setup(t => t.GetMerenjaPoDanuIVremenu(DateTime.Now.Date, "sok")).Returns(new List<IMerenje>() {m2});
            dAOMerenjamoq.Setup(t => t.GetPoslednjeMerenje("sok", Convert.ToDateTime(datum))).Returns(Convert.ToDateTime(datum));
            dAOMerenjamoq.Setup(t => t.GetMerenjaPoDanuIVremenu(DateTime.Now.Date, "ras")).Returns(new List<IMerenje>());

            testingObject.InvokeResident(dAOMerenjamoq.Object, csvParamsmoq.Object, dAORezultatmoq.Object);

            Assert.Multiple(() =>
            {
                foreach (var bol in testingObject.UspenoIzvrseneFunkcije)
                {
                    Assert.IsTrue(bol);
                }
            });
        }

        [Test]
        public void CheckIfGetMerenjaPoDanuIVremenuIsCalledTest()
        {
            string datum = "2005-05-05 22:12 PM";

            dAORezultatmoq.Setup(t => t.PostojiMerenjeZaDatuSifruTogDana("voj", Convert.ToDateTime(datum))).Returns(false);

            csvParamsmoq = new Mock<ICsvInput>();

            csvParamsmoq.Setup(t => t.csvPodrucja).Returns(new List<string> { "voj" });
            csvParamsmoq.Setup(t => t.csvFunkcije).Returns(new Dictionary<int, int> { { 1, 0 }, { 2, 0 }, { 3, 1 } });


            Merenje m1 = new Merenje();
            m1.dan = Convert.ToDateTime(datum);
            m1.nazivPodrucija = "Vojvodina";
            m1.vreme = Convert.ToDateTime(datum);
            m1.sifraPodrucija = "voj";
            m1.potrosnja = 200;

            dAOMerenjamoq.Setup(t => t.GetMerenjaPoDanuIVremenu(DateTime.Now.Date, "voj")).Returns(new List<IMerenje>() {m1});

            testingObject.InvokeResident(dAOMerenjamoq.Object, csvParamsmoq.Object, dAORezultatmoq.Object);

            dAOMerenjamoq.Verify(t => t.GetMerenjaPoDanuIVremenu(DateTime.Now.Date, "voj"), Times.Once);
        }


        [Test]
        public void InvokeSingleFun1PositiveTest()
        {
            string datum = "2009-02-02 22:12 PM";

            dAORezultatmoq.Setup(t => t.PostojiMerenjeZaDatuSifruTogDana("kop", Convert.ToDateTime(datum))).Returns(false);

            Merenje m1 = new Merenje();
            m1.dan = Convert.ToDateTime(datum);
            m1.nazivPodrucija = "Kopaonik";
            m1.vreme = Convert.ToDateTime(datum);
            m1.sifraPodrucija = "kop";
            m1.potrosnja = 320;

            List<IMerenje> listamerenja = new List<IMerenje>() {m1};


            dAOMerenjamoq.Setup(t => t.GetMerenjaPoDanuIVremenu(Convert.ToDateTime(datum), "kop")).Returns(listamerenja);
            dAOMerenjamoq.Setup(t => t.GetPoslednjeMerenje("kop", Convert.ToDateTime(datum))).Returns(Convert.ToDateTime(datum));

            testingObject.InvokeSingle(Convert.ToDateTime(datum),"kop",1,dAOMerenjamoq.Object, dAORezultatmoq.Object);

            Assert.Multiple(() =>
            {
                foreach (var bol in testingObject.UspenoIzvrseneFunkcije)
                {
                    Assert.IsTrue(bol);
                }
            });
        }

        [Test]
        public void InvokeSingleFun2PositiveTest()
        {
            string datum = "2009-02-02 22:12 PM";

            dAORezultatmoq.Setup(t => t.PostojiMerenjeZaDatuSifruTogDana("kop", Convert.ToDateTime(datum))).Returns(false);

            Merenje m1 = new Merenje();
            m1.dan = Convert.ToDateTime(datum);
            m1.nazivPodrucija = "Kopaonik";
            m1.vreme = Convert.ToDateTime(datum);
            m1.sifraPodrucija = "kop";
            m1.potrosnja = 320;

            List<IMerenje> listamerenja = new List<IMerenje>() { m1 };


            dAOMerenjamoq.Setup(t => t.GetMerenjaPoDanuIVremenu(Convert.ToDateTime(datum), "kop")).Returns(listamerenja);
            dAOMerenjamoq.Setup(t => t.GetPoslednjeMerenje("kop", Convert.ToDateTime(datum))).Returns(Convert.ToDateTime(datum));

            testingObject.InvokeSingle(Convert.ToDateTime(datum), "kop", 2, dAOMerenjamoq.Object, dAORezultatmoq.Object);

            Assert.Multiple(() =>
            {
                foreach (var bol in testingObject.UspenoIzvrseneFunkcije)
                {
                    Assert.IsTrue(bol);
                }
            });
        }


        [Test]
        public void InvokeSingleFun3PositiveTest()
        {
            string datum = "2009-02-02 22:12 PM";

            dAORezultatmoq.Setup(t => t.PostojiMerenjeZaDatuSifruTogDana("kop", Convert.ToDateTime(datum))).Returns(false);

            Merenje m1 = new Merenje();
            m1.dan = Convert.ToDateTime(datum);
            m1.nazivPodrucija = "Kopaonik";
            m1.vreme = Convert.ToDateTime(datum);
            m1.sifraPodrucija = "kop";
            m1.potrosnja = 320;

            List<IMerenje> listamerenja = new List<IMerenje>() { m1 };


            dAOMerenjamoq.Setup(t => t.GetMerenjaPoDanuIVremenu(Convert.ToDateTime(datum), "kop")).Returns(listamerenja);
            dAOMerenjamoq.Setup(t => t.GetPoslednjeMerenje("kop", Convert.ToDateTime(datum))).Returns(Convert.ToDateTime(datum));

            testingObject.InvokeSingle(Convert.ToDateTime(datum), "kop", 3, dAOMerenjamoq.Object, dAORezultatmoq.Object);

            Assert.Multiple(() =>
            {
                foreach (var bol in testingObject.UspenoIzvrseneFunkcije)
                {
                    Assert.IsTrue(bol);
                }
            });
        }

        [Test]
        public void InvokeSingleWrongFunNumberTest()
        {
            string datum = "2009-02-02 22:12 PM";


            Assert.Throws<NeposotojeciBrojFunkcijeException>(() =>
            {             
                    testingObject.InvokeSingle(Convert.ToDateTime(datum), "kop", -2, dAOMerenjamoq.Object, dAORezultatmoq.Object);
                
            });
        }

        [Test]
        public void InvokerSetProperty()
        {
            testingObject.UspenoIzvrseneFunkcije = new List<bool>();
            Assert.IsEmpty(testingObject.UspenoIzvrseneFunkcije);
        }

    }
}
