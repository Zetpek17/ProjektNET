using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SklepWPF
{
    public class Kasa
    {
        public bool sprawdzPIN(string pin)
        {
            if (Regex.IsMatch(pin, @"\b[0-9]{4}\b"))
                return true;
            return false;
        }

        public bool sprawdzKoszt(Kosz kosz, Portfel portfel)
        {
            if (kosz.Suma <= portfel.Kwota)
                return true;
            return false;
        }
    }
}
