using SklepWPF.CurrencyServiceReference;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepWPF
{
    public class Paragon
    {
        public bool drukuj(Sklep sklep, Kosz koszyk, char coPlatnosc, Portfel portfel, string waluta)
        {
            DateTime dt = DateTime.Now;
            using (TextWriter zapis = new StreamWriter(@"paragon.txt"))
            {
                zapis.WriteLine(" " + sklep.NazwaSklep + "\n" + sklep.AdresSklep + "\n      " + sklep.Nip + "\n");
                zapis.WriteLine(sklep.AdresSklep);
                zapis.WriteLine("      " + sklep.Nip);
                zapis.WriteLine("");
                zapis.WriteLine(String.Format("{0:d.M.yyyy}\n", dt));
                foreach (var produkt in koszyk.Produkty)
                {
                    int pos = koszyk.Produkty.IndexOf(koszyk.Produkty.Find(x => x == produkt));
                    zapis.WriteLine(produkt.Nazwa + "   " + koszyk.IleProduktow.ElementAt(pos) + "x" + produkt.Cena + " zł");
                }
                zapis.WriteLine("------------------------");
                zapis.WriteLine("SUMA: " + koszyk.Suma.ToString("0.00") + " zł");
                zapis.WriteLine("------------------------");
                zapis.WriteLine("");
                zapis.WriteLine("Sposób płatności:");

                if (coPlatnosc == 'g')
                {
                    zapis.WriteLine("Gotówka   " + portfel.Kwota.ToString("0.00") + " zł");
                    zapis.WriteLine("RESZTA   " + (portfel.Kwota - koszyk.Suma).ToString("0.00") + " zł");
                    return true;
                }
                else
                {
                    try
                    {
                        using (CurrencyConvertorSoapClient client = new CurrencyConvertorSoapClient("CurrencyConvertorSoap"))
                        {
                            double kurs = 1;

                            switch (waluta)
                            {
                                case "EUR":
                                    kurs = client.ConversionRate(Currency.PLN, Currency.EUR);
                                    break;
                                case "USD":
                                    kurs = client.ConversionRate(Currency.PLN, Currency.USD);
                                    break;
                                case "GBP":
                                    kurs = client.ConversionRate(Currency.PLN, Currency.GBP);
                                    break;
                                default:
                                    break;
                            }
                            zapis.WriteLine("Karta płatnicza   " + (Math.Round(kurs, 4) * koszyk.Suma).ToString("0.00") + " " + waluta);
                            if (waluta != "PLN")
                                zapis.WriteLine("(Kurs PLN -> " + waluta.ToString() + ": " + Math.Round(kurs, 4).ToString() + ")");
                            client.Close();
                            return true;
                        }
                    }
                    catch (Exception)
                    {
                        zapis.WriteLine("Karta płatnicza   " + koszyk.Suma.ToString("0.00") + " zł");
                        return false;
                    }
                }
            }   
        }
    }
}
