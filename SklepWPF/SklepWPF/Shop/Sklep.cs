using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SklepWPF
{
    [XmlRoot]
    public class Sklep
    {
        private string _nazwaSklep;
        public string NazwaSklep
        { get { return _nazwaSklep; } set { _nazwaSklep = value; } }

        private string _adresSklep;
        public string AdresSklep
        { get { return _adresSklep; } set { _adresSklep = value; } }

        private string _nip;
        public string Nip
        { get { return _nip; } set { _nip = value; } }

        private List<Dzial> _dzialy;
        public List<Dzial> Dzialy
        { get { return _dzialy; } set { _dzialy = value; } }
    }
}
