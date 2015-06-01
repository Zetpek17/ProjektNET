using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepWPF
{
    public class Portfel : IInformacyjne
    {
        private int _100zl, _50zl, _20zl, _10zl, _5zl, _2zl, _1zl, _50gr, _20gr, _10gr;
        private double _kwota;
        public double Kwota
        { get { return _kwota; } set { _kwota = value; } }

        public Portfel()
        {
            Random rand = new Random();
            _100zl = rand.Next(0, 2);
            _50zl = rand.Next(0, 2);
            _20zl = rand.Next(0, 4);
            _10zl = rand.Next(0, 4);
            _5zl = rand.Next(0, 5);
            _2zl = rand.Next(0, 5);
            _1zl = rand.Next(0, 5);
            _50gr = rand.Next(0, 10);
            _20gr = rand.Next(0, 10);
            _10gr = rand.Next(0, 10);
            _kwota = ((_100zl * 100) + (_50zl * 50) + (_20zl * 20) + (_10zl * 10) + (_5zl * 5) + (_2zl * 2) + (_1zl * 1) + (_50gr * 0.5) + (_20gr * 0.2) + (_10gr * 0.1));
        }

        public string getInfo()
        {
            return "W portfelu masz " + Kwota.ToString("#.00") + " zł:\n" +
                                _100zl + " x 100 zł\n" +
                                _50zl + " x 50 zł\n" +
                                _20zl + " x 20 zł\n" +
                                _10zl + " x 10 zł\n" +
                                _5zl + " x 5 zł\n" +
                                _2zl + " x 2 zł\n" +
                                _1zl + " x 1 zł\n" +
                                _50gr + " x 50 gr\n" +
                                _20gr + " x 20 gr\n" +
                                _10gr + " x 10 gr\n";
        }
    }
}
