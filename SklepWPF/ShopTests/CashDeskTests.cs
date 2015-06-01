using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SklepWPF;
using System.Threading;

namespace ShopTests
{
    [TestClass]
    public class CashDeskTests
    {
        [TestMethod]
        public void Test_CheckIfPinIsCorrect()
        {
            Kasa kasa = new Kasa();
            Assert.IsFalse(kasa.sprawdzPIN("asdc"), "Nieprawidłowy PIN ('asdc') został uznany za poprawny");
            Assert.IsFalse(kasa.sprawdzPIN("560"), "Nieprawidłowy PIN ('560') został uznany za poprawny");
            Assert.IsFalse(kasa.sprawdzPIN("283953"), "Nieprawidłowy PIN ('283953') został uznany za poprawny");
            Assert.IsTrue(kasa.sprawdzPIN("1234"), "Prawidłowy PIN ('1234') został uznany za niepoprawny");

        }

        [TestMethod]
        public void Test_CheckIfEnoughMoney()
        {
            Kasa kasa = new Kasa();
            Portfel portfel = new Portfel();
            portfel.Kwota = 65.30;
            Kosz kosz = new Kosz();
            Produkt produkt = new Produkt("Rtv_agd", "200", "Mysz bezprzewodowa", "50.00", "szt");
            kosz.dodajProdukt(produkt, 1);

            Assert.IsTrue(kasa.sprawdzKoszt(kosz, portfel), "Pieniądze w portfelu nie wystarczą na zakupy");
        }
    }
}
