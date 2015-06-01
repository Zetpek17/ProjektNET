using SklpeWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace SklepWPF
{
    public partial class MainWindow : Window
    {
        private static Portfel _portfel;
        private static Sklep _sklep;
        private static List<Produkt> _produkty;
        private static Kosz _kosz;
        private CartModel _cartModel;
        private Paragon _paragon;
        private Kasa _kasa;
        private string _waluta;
        private char _platnosc;
        private Thread _thread;

        public MainWindow()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-us");
            try
            {
                initMainWindow();
            }
            catch(Exception)
            {
                MessageBox.Show("Przepraszamy ale sklep jest w tej chwili niedostępny. Prosimy spróbować ponownie później.");
                Application.Current.Shutdown();
            }
        }


        //******************************************************
        //Wątek
        //******************************************************

        private void threadDrukuj()
        {
            _paragon = new Paragon();
            bool result = _paragon.drukuj(_sklep, _kosz, _platnosc, _portfel, _waluta);
            Thread.Sleep(2000);
            if (result)
                MessageBox.Show("Wydrukowano paragon.");
            else
                MessageBox.Show("Nie można połączyć się z kantorem. Waluta zmieniona na PLN.");
        }


        //******************************************************
        //Wczytanie komponentów strony
        //******************************************************

        private void initMainWindow()
        {
            _sklep = XMLDeserializer.DeserializeFromXML();
            _produkty = XMLDeserializer.DesrializeProducts();
            _portfel = new Portfel();
            _kosz = new Kosz();
            _cartModel = new CartModel();
            _kasa = new Kasa();
            _thread = new Thread(new ThreadStart(threadDrukuj));
            ViewPortfel.DataContext = _portfel.getInfo();
            ViewArticles.DataContext = showArticles();
            DepartmentsList.ItemsSource = showDepartments();
            setCart();
        }


        //******************************************************
        //Wybór działu
        //******************************************************

        private List<string> showDepartments()
        {
            List<string> dzialy = new List<string>();
            foreach (Dzial d in _sklep.Dzialy)
                dzialy.Add(d.NazwaDzial);
            return dzialy;
        }
        private void DepartmentsListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProductsList.ItemsSource = showProducts(DepartmentsList.SelectedItem.ToString());
        }


        //******************************************************
        //Wybór produktu
        //******************************************************

        private List<string> showProducts(string nazwaDzial)
        {
            List<string> produkty = new List<string>();
            foreach (Dzial d in _sklep.Dzialy)
                foreach (Produkt p in d.Produkty)
                    if (d.NazwaDzial == nazwaDzial)
                        produkty.Add(p.Nazwa);
            return produkty;
        }

        private void ProductsListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            productsQuantity.DataContext = "1";
            ProductStackPanel.Visibility = Visibility.Visible;
            Added.Visibility = Visibility.Hidden;
            try
            {
                ProductsInfo.DataContext = _produkty.Find(x => x.Nazwa == ProductsList.SelectedItem.ToString()).getInfo();
            }
            catch (Exception)
            {
                ProductsInfo.DataContext = String.Empty;
                ProductStackPanel.Visibility = Visibility.Hidden;
                Added.Visibility = Visibility.Hidden;
            }
        }


        //******************************************************
        //Wybór ilości produktu i dodanie do koszyka
        //******************************************************

        private void DecreaseNumberOnClick(object sender, RoutedEventArgs e)
        {

            if (_produkty.Find(x => x.Nazwa == ProductsList.SelectedItem.ToString()).Rodzaj == "szt")
            {
                if (Convert.ToInt16(productsQuantity.Text) > 1)
                    productsQuantity.DataContext = Convert.ToInt16(productsQuantity.Text) - 1;
            }
            else
            {
                if (Convert.ToDouble(productsQuantity.Text) > 0.1)
                    productsQuantity.DataContext = Convert.ToDouble(productsQuantity.Text) - 0.1;
            }
        }

        private void IncreaseNumberOnClick(object sender, RoutedEventArgs e)
        {
            if (_produkty.Find(x => x.Nazwa == ProductsList.SelectedItem.ToString()).Rodzaj == "szt")
                productsQuantity.DataContext = Convert.ToInt16(productsQuantity.Text) + 1;
            else
                productsQuantity.DataContext = Convert.ToDouble(productsQuantity.Text) + 0.1;
        }

        private void AddToCartOnClick(object sender, RoutedEventArgs e)
        {
            Produkt p = _produkty.Find(x => x.Nazwa == ProductsList.SelectedItem.ToString());
            _kosz.dodajProdukt(p, Convert.ToDouble(productsQuantity.Text));
            Added.Visibility = Visibility.Visible;
            ProductsInCartList.ItemsSource = _cartModel.getCollection(_kosz);
            setCart();
        }


        //******************************************************
        //Obsługa koszyka (usuwanie produktów)
        //******************************************************

        private void RemoveSelectedOnClick(object sender, RoutedEventArgs e)
        {
            List<int> selectedItemIndexes = new List<int>();
            foreach (object o in ProductsInCartList.SelectedItems)
                selectedItemIndexes.Add(ProductsInCartList.Items.IndexOf(o));
            List<Produkt> produkty = new List<Produkt>();
            foreach (int index in selectedItemIndexes)
                produkty.Add(_kosz.Produkty[index]);
            foreach (Produkt p in produkty)
                _kosz.usunProdukt(p);
            ProductsInCartList.ItemsSource = _cartModel.getCollection(_kosz);
            setCart();
        }

        private void RemoveAllOnClick(object sender, RoutedEventArgs e)
        {
            _kosz.usunWszystkieProdukty();
            _cartModel.StringsCollection.Clear();
            setCart();
        }

        private void setCart()
        {
            if (_kosz.Produkty.Count == 0)
            {
                ProductsInCartList.Visibility = Visibility.Collapsed;
                Sum.DataContext = "Twój koszyk jest pusty";
                RemoveSelected.Visibility = Visibility.Collapsed;
                RemoveAll.Visibility = Visibility.Collapsed;
                Buy.Visibility = Visibility.Hidden;
            }
            else
            {
                ProductsInCartList.Visibility = Visibility.Visible;
                Sum.DataContext = "\nRazem: " + _kosz.Suma.ToString("0.00");
                RemoveSelected.Visibility = Visibility.Visible;
                RemoveAll.Visibility = Visibility.Visible;
                Buy.Visibility = Visibility.Visible;
            }
        }


        //******************************************************
        //Wczytanie listy produktów dostępnych w sklepie
        //******************************************************

        private string showArticles()
        {
            string result = "";
            foreach (var dzial in _sklep.Dzialy)
            {
                result += string.Format("{0}) {1}\n    Lista produktów:\n", dzial.IdDzial, dzial.NazwaDzial);
                foreach (var produkt in dzial.Produkty)
                    result += string.Format("    {0}) {1}\n", produkt.Id, produkt.getInfo());
                result += "\n\n";
            }
            return result;
        }


        //******************************************************
        //Płatność
        //******************************************************

        private void ChooseOnClick(object sender, RoutedEventArgs e)
        {
            if((bool)Cash.IsChecked)
            {
                CardPay.Visibility = Visibility.Collapsed;
                Currency.Visibility = Visibility.Collapsed;
                if (_kasa.sprawdzKoszt(_kosz, _portfel))
                {
                    _waluta = "PLN";
                    _platnosc = 'g';
                    _thread.Start();
                    MessageBox.Show("Dziękujemy za zakupy. Zapraszamy ponownie!");
                    Application.Current.Shutdown();
                }
                else
                {
                    MessageBox.Show("Masz za mało pieniędzy. Usuń produkty z koszyka lub zapłać kartą.");
                }
            }
            else
            {
                CardPay.Visibility = Visibility.Visible;
            }
        }

        private void OkPINOnClick(object sender, RoutedEventArgs e)
        {
            string pin = PIN.Password;

            if (_kasa.sprawdzPIN(pin))
            {
                Currency.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBoxResult errorPIN = MessageBox.Show("Wprowadzony pin jest niepoprawny. Spróbuj jeszcze raz");
                Currency.Visibility = Visibility.Collapsed;            
            }
        }

        private void OkCurrencyOnClick(object sender, RoutedEventArgs e)
        {
            if ((bool)PLN.IsChecked)
                _waluta = "PLN";
            if ((bool)EUR.IsChecked)
                _waluta = "EUR";
            if ((bool)USD.IsChecked)
                _waluta = "USD";
            if ((bool)GBP.IsChecked)
                _waluta = "GBP";
            _platnosc = 'k';
            _thread.Start();
            MessageBox.Show("Dziękujemy za zakupy. Zapraszamy ponownie!");
            Application.Current.Shutdown();
        }
    }
}