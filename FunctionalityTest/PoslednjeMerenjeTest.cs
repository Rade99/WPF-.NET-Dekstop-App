using Common.DAO;
using Common.Functionality;
using DAO;
using Functionality;
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
    class PoslednjeMerenjeTest
    {

        IPoslednjeMerenje testingObject;
        Mock<IDAOMerenje> dAOMerenjamoq = new Mock<IDAOMerenje>();
        Mock<IDAORezultat> dAORezultatmoq = new Mock<IDAORezultat>();

        [SetUp]
        public void Functionality()
        {

            testingObject = new PoslednjeMerenje();
        }

        [Test] 
        public void PoslednjeMerenjeFirstIfPassedPositiveTest() // nem pojma
        {
            string datum = "2005-05-05 22:12 PM";

            dAORezultatmoq.Setup(t => t.PostojiMerenjeZaDatuSifruTogDana("sok", Convert.ToDateTime(datum))).Returns(false);

            Assert.IsTrue(testingObject.ProveraPreUpisa("sok", Convert.ToDateTime(datum),1, dAOMerenjamoq.Object, dAORezultatmoq.Object));
        }

        [Test]
        public void PoslednjeMerenjeSecondIfPassedPositiveTest() // nem pojma
        {
            string datum = "2005-05-05 22:12 PM";

            dAORezultatmoq.Setup(t => t.PostojiMerenjeZaDatuSifruTogDana("sok", Convert.ToDateTime(datum))).Returns(true);

            dAOMerenjamoq.Setup(t => t.PostojiMerenjeZaDatuSifruTogDana("sok", Convert.ToDateTime(datum))).Returns(false);

            Assert.IsFalse(testingObject.ProveraPreUpisa("sok", Convert.ToDateTime(datum), 1, dAOMerenjamoq.Object, dAORezultatmoq.Object));
        }

        [Test]
        public void PoslednjeMerenjeThirdIfPassedPositiveTest() // nem pojma
        {
            string datum = "2005-05-05 22:12 PM";

            dAORezultatmoq.Setup(t => t.PostojiMerenjeZaDatuSifruTogDana("sok", Convert.ToDateTime(datum))).Returns(true);

            dAOMerenjamoq.Setup(t => t.PostojiMerenjeZaDatuSifruTogDana("sok", Convert.ToDateTime(datum))).Returns(true);

            dAORezultatmoq.Setup(t => t.ProveraPraznoPoljeZaPotrosnju("sok", Convert.ToDateTime(datum), 2)).Returns(false);

            Assert.IsTrue(testingObject.ProveraPreUpisa("sok", Convert.ToDateTime(datum), 2, dAOMerenjamoq.Object, dAORezultatmoq.Object));
        }

        [Test]
        public void AllIfPassedTest() // nem pojma
        {
            string datum = "2005-05-05 22:12 PM";

            dAORezultatmoq.Setup(t => t.PostojiMerenjeZaDatuSifruTogDana("sok", Convert.ToDateTime(datum))).Returns(true);

            dAOMerenjamoq.Setup(t => t.PostojiMerenjeZaDatuSifruTogDana("sok", Convert.ToDateTime(datum))).Returns(true);

            dAORezultatmoq.Setup(t => t.ProveraPraznoPoljeZaPotrosnju("sok", Convert.ToDateTime(datum), 2)).Returns(true);

            Assert.IsTrue(testingObject.ProveraPreUpisa("sok", Convert.ToDateTime(datum), 1, dAOMerenjamoq.Object, dAORezultatmoq.Object));
        }
    }
      
    
}
