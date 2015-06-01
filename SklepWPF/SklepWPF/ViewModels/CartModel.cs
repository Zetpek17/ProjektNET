using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using SklepWPF;

namespace SklpeWPF.ViewModels
{
    public class CartModel
    {
        private ObservableCollection<string> _stringsCollection;
        public ObservableCollection<string> StringsCollection
        {
            get { return _stringsCollection; }
            set { _stringsCollection = value; }
        }

        public CartModel()
        {
            this.StringsCollection = new ObservableCollection<string>();
        }

        internal ObservableCollection<string> getCollection(Kosz kosz)
        {
            this.StringsCollection.Clear();
            foreach (Produkt p in kosz.Produkty)
            {
                int pos = kosz.Produkty.IndexOf(kosz.Produkty.Find(x => x == p));
                this.StringsCollection.Add(string.Format("{0} (cena za {1}: {2} zł) x {3}, razem: {4} zł", p.Nazwa, p.Rodzaj, p.Cena, kosz.IleProduktow[pos], (kosz.IleProduktow[pos] * Convert.ToDouble(p.Cena)).ToString("0.00")));
            }
            return this.StringsCollection;
        }
    }
}
