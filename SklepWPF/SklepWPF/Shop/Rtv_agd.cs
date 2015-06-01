using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepWPF
{
    public class Rtv_agd : Produkt
    {
        public Rtv_agd() : base() { }

        private string _gwarancja;
        public string Gwarancja
        { get { return _gwarancja; } set { _gwarancja = value; } }

        private string _nrSeryjny;
        public string NrSeryjny
        { get { return _nrSeryjny; } set { _nrSeryjny = value; } }

        public override string getInfo()
        {
            return string.Format("NAZWA: {0}\nCENA: {1} zł za {2}\nGWARANCJA: {3} lat(a)\nNR SERYJNY: {4}\n", Nazwa, Cena, Rodzaj, Gwarancja, NrSeryjny);
        }
    }
}
