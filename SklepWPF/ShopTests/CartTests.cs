using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SklepWPF;

namespace ShopTests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void Test_ProductIsAddedToCart()
        {
            Kosz kosz = new Kosz();
            int ile = 3;
            double cena = 2.40;
            Produkt produkt = new Produkt("ProduktSpozywczy", "100", "Sałata", cena.ToString(), "szt");
            kosz.dodajProdukt(produkt, ile);

            double oczekiwanaSuma = 7.20;
            int oczekiwanaIlosc = 1;

            Assert.AreEqual(oczekiwanaSuma, kosz.Suma, 0.001, "Sumy nie są równe");
            Assert.AreEqual(oczekiwanaIlosc, kosz.Produkty.Count, 0.001, "Liczba produktów w koszyku nie jest równa");
        }

        [TestMethod]
        public void Test_TwoSameProductsAreAddedToCartAsOne()
        {
            Kosz kosz = new Kosz();
            int ile = 3;
            double cena = 2.40;
            Produkt produkt = new Produkt("ProduktSpozywczy", "100", "Sałata", cena.ToString(), "szt");
            kosz.dodajProdukt(produkt, ile);
            kosz.dodajProdukt(produkt, ile);

            double oczekiwanaSuma = 14.40;
            int oczekiwanaIlosc = 1;

            Assert.AreEqual(oczekiwanaSuma, kosz.Suma, 0.001, "Sumy nie są równe");
            Assert.AreEqual(oczekiwanaIlosc, kosz.Produkty.Count, 0.001, "Liczba produktów w koszyku nie jest równa");
        }

        [TestMethod]
        public void Test_ProductIsRemovedFromCart()
        {
            Kosz kosz = new Kosz();
            int ile = 3;
            double cena = 2.40;
            Produkt produkt = new Produkt("ProduktSpozywczy", "100", "Sałata", cena.ToString(), "szt");
            kosz.dodajProdukt(produkt, ile);

            double oczekiwanaSuma = 0;
            int oczekiwanaIlosc = 0;

            kosz.usunProdukt(produkt);
            
            Assert.AreEqual(oczekiwanaSuma, kosz.Suma, 0.001, "Sumy nie są równe");
            Assert.AreEqual(oczekiwanaIlosc, kosz.Produkty.Count, 0.001, "Liczba produktów w koszyku nie jest równa");
        }

        [TestMethod]
        public void Test_AllProductsAreRemovedFromCart()
        {
            Kosz kosz = new Kosz();
            int ile = 3;
            double cena1 = 2.40;
            double cena2 = 1.20;
            Produkt produkt1 = new Produkt("ProduktSpozywczy", "100", "Sałata", cena1.ToString(), "szt");
            Produkt produkt2 = new Produkt("ProduktSpozywczy", "110", "Koperek", cena2.ToString(), "szt");
            kosz.dodajProdukt(produkt1, ile);
            kosz.dodajProdukt(produkt2, ile);

            double oczekiwanaSuma = 0;
            int oczekiwanaIlosc = 0;

            kosz.usunWszystkieProdukty();

            Assert.AreEqual(oczekiwanaSuma, kosz.Suma, 0.001, "Sumy nie są równe");
            Assert.AreEqual(oczekiwanaIlosc, kosz.Produkty.Count, 0.001, "Liczba produktów w koszyku nie jest równa");
        }
    }
}
