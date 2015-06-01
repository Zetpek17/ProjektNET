using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SklepWPF
{
    public class Produkt : IInformacyjne
    {
        public Produkt() {}

        public Produkt(string typ, string id, string nazwa, string cena, string rodzaj)
        {
            this._typ = typ;
            this._id = id;
            this._nazwa = nazwa;
            this._cena = cena;
            this._rodzaj = rodzaj;
        }

        private string _typ;
        [XmlAttribute("Typ")]
        public string Typ
        { get { return _typ; } set { _typ = value; } }
        
        private string _id;
        public string Id
        { get { return _id; } set { _id = value; } }

        private string _nazwa;
        public string Nazwa
        { get { return _nazwa; } set { _nazwa = value; } }

        private string _cena;
        public string Cena
        { get { return _cena; } set { _cena = value; } }

        private string _rodzaj;
        public string Rodzaj
        { get { return _rodzaj; } set { _rodzaj = value; } }

        public virtual string getInfo()
        {
            return string.Format("{0}, cena: {1} zł za {2}", Nazwa, Cena, Rodzaj);
        }
    }
}
