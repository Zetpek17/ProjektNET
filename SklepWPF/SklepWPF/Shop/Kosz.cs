using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepWPF
{
    public class Kosz
    {

        public Kosz() {
            _produkty = new List<Produkt>();
            _ileProduktow = new List<double>();
            _suma = 0;
        }

        private List<Produkt> _produkty;
        public List<Produkt> Produkty
        { get { return _produkty; } set { _produkty = value; } }

        private List<double> _ileProduktow;
        public List<double> IleProduktow
        { get { return _ileProduktow; } set { _ileProduktow = value; } }

        private double _suma;
        public double Suma
        { get { return _suma; } set { _suma = value; } }
        
        public void dodajProdukt(Produkt produkt, double ile)
        {
            if (_produkty.Exists(x => x == produkt))
            {
                int pos = _produkty.IndexOf(_produkty.Find(x => x == produkt));
                _ileProduktow[pos] += ile;
                _suma += Convert.ToDouble(produkt.Cena) * ile;
            }
            else
            {
                _produkty.Add(produkt);
                _ileProduktow.Add(ile);
                _suma += Convert.ToDouble(produkt.Cena) * ile;
            }
        }

        public void usunProdukt(Produkt produkt)
        {
            int pos = _produkty.IndexOf(_produkty.Find(x => x == produkt));
            double ile = _ileProduktow[pos];
            double cena = Convert.ToDouble(_produkty[pos].Cena);
            _suma -= cena * ile;
            _ileProduktow.RemoveAt(pos);
            _produkty.RemoveAt(pos);
        }

        public void usunWszystkieProdukty()
        {
            _suma = 0;
            _ileProduktow.Clear();
            _produkty.Clear();
        }
    }
}
