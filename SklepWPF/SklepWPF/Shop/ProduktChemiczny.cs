using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SklepWPF
{
    public class ProduktChemiczny : Produkt
    {
        public ProduktChemiczny() : base() { }

        private string _waznosc;
        public string Waznosc
        { get { return _waznosc; } set { _waznosc = value; } }

        private string _sklad;
        public string Sklad
        { get { return _sklad; } set { _sklad = value; } }

        public override string getInfo()
        {
            return string.Format("NAZWA: {0}\nCENA: {1} zł za {2}\nWAŻNOŚĆ: {3} dni\nSKŁAD: {4}\n", Nazwa, Cena, Rodzaj, Waznosc, Sklad);
        }
    }
}
