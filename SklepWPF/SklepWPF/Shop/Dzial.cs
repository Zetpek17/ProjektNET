using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SklepWPF
{
    public class Dzial
    {
        private string _idDzial;
        public string IdDzial
        { get { return _idDzial; } set { _idDzial = value; } }

        private string _nazwaDzial;
        public string NazwaDzial
        { get { return _nazwaDzial; } set { _nazwaDzial = value; } }
        
        private List<Produkt> _produkty;
        public List<Produkt> Produkty
        { get { return _produkty; } set { _produkty = value; } }
    }
}
