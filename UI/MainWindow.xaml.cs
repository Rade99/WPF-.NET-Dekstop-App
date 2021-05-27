using DAO;
using Functionality;
using Interlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<string> izmerenaPodrucja;
        List<string> svaPodrucjaTemp;
      
        Task t;
        Medjusloj medju = new Medjusloj();

        public MainWindow()
        {
            InitializeComponent();
            OsveziComboBoxove();
        }
        private void OsveziComboBoxove()
        {
            svaPodrucjaTemp = medju.GetSvaPodrucjaMedju();
            izmerenaPodrucja = medju.GetIzmerenaPodrucjaMedju();

            ComboSifra.ItemsSource = izmerenaPodrucja;
            ComboNazivPodrucja.ItemsSource = svaPodrucjaTemp;
        }

        private bool ValidacijaZaIzvrsenjeFun()
        {
            bool rez = true;

            if (ComboSifra.SelectedIndex == -1)
            {
                rez = false;
                ComboSifra.BorderBrush = Brushes.Red;
                ComboSifra.BorderThickness = new Thickness(3);
                MessageBox.Show("Selektuj podrucje");
            }
            else
            {
                ComboSifra.BorderBrush = Brushes.Green;
                ComboSifra.BorderThickness = new Thickness(3);
            }

            if (DatePick.SelectedDate == null)
            {
                rez = false;
                DatePick.BorderBrush = Brushes.Red;
                DatePick.BorderThickness = new Thickness(3);
                MessageBox.Show("Selektuj Datum");
            }
            else
            {
                DatePick.BorderBrush = Brushes.Green;
                DatePick.BorderThickness = new Thickness(3);
            }

            if(rez == false)
            {
                return false;
            }
            else if(medju.GetMerenjaPoDanuIVremenuMedju((DateTime)DatePick.SelectedDate, medju.GetSifreByNazivPodrucjaMedju(ComboSifra.SelectedItem.ToString())).Count == 0)
            {
                rez = false;
                DatePick.BorderBrush = Brushes.Red;
                DatePick.BorderThickness = new Thickness(3);
                System.Windows.MessageBox.Show("Funkcija se nije izvrsila jer ne postoje uneta merenja za odabrani datum", "Informacija", MessageBoxButton.OK, MessageBoxImage.Warning);
            }


            return rez;
        }

        private bool ValidacijaZaDodavanjeMerenja()
        {
            bool rez = true;

            if (ComboNazivPodrucja.SelectedIndex == -1)
            {
                rez = false;
                ComboNazivPodrucja.BorderBrush = Brushes.Red;
                ComboNazivPodrucja.BorderThickness = new Thickness(3);
                MessageBox.Show("Selektuj podrucje");
                return false;
            }
            else
            {
                ComboNazivPodrucja.BorderBrush = Brushes.Green;
                ComboNazivPodrucja.BorderThickness = new Thickness(3);
            }

            if (DateTimePicker.Value == null)
            {
                rez = false;
                DateTimePicker.BorderBrush = Brushes.Red;
                DateTimePicker.BorderThickness = new Thickness(3);
                MessageBox.Show("Selektuj Datum");
               
            }
            else
            {
                DateTimePicker.BorderBrush = Brushes.Green;
                DateTimePicker.BorderThickness = new Thickness(3);
            }

            if(rez == false)
            {
                return false;
            }
            else if(MerenjePrePoslednjeg() == false)
            {
                rez = false;
            }

       
            if (textBoxPotrosnja.Text.Trim().Equals(""))
            {
                rez = false;
                textBoxPotrosnja.BorderBrush = Brushes.Red;
                textBoxPotrosnja.BorderThickness = new Thickness(3);
                MessageBox.Show("Unesite Potrosnju");
            }
            else
            {
                textBoxPotrosnja.BorderBrush = Brushes.Green;
                textBoxPotrosnja.BorderThickness = new Thickness(3);

                try
                {
                    if(ValidacijaPotrosnje(float.Parse(textBoxPotrosnja.Text.Trim()))==false)
                    {
                        rez = false;
                    }
                }
                catch
                {
                    textBoxPotrosnja.BorderBrush = Brushes.Red;
                    textBoxPotrosnja.BorderThickness = new Thickness(3);
                    System.Windows.MessageBox.Show("Potrosnja je van brojnog opsega ili nije broj", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                    rez = false;

                }
            }


            return rez;
        }

        private bool MerenjePrePoslednjeg()
        {

            if(medju.GetMerenjaPoSifriPodrucijaMedju(medju.GetSifreByNazivPodrucjaMedju(ComboNazivPodrucja.SelectedItem.ToString()), (DateTime)DateTimePicker.Value).Count==0)
            {
                return true;
            }

            if (DateTimePicker.Value < medju.GetPoslednjeMerenjeMedju(medju.GetSifreByNazivPodrucjaMedju(ComboNazivPodrucja.SelectedItem.ToString()), (DateTime)DateTimePicker.Value))
            {
                DateTimePicker.BorderBrush = Brushes.Red;
                DateTimePicker.BorderThickness = new Thickness(3);
                System.Windows.MessageBox.Show("Izabrali ste merenje pre poslednjeg", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                DateTimePicker.BorderBrush = Brushes.Green;
                DateTimePicker.BorderThickness = new Thickness(3);
            }

            return true;

        }

        private bool ValidacijaPotrosnje(float potrosnja)
        {
            if(potrosnja <0)
            {
                textBoxPotrosnja.BorderBrush = Brushes.Red;
                textBoxPotrosnja.BorderThickness = new Thickness(3);
                System.Windows.MessageBox.Show("Uneli ste negativnu potrosnju", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

       

        private bool ValidacijaZaDodavanjePodrucja()
        {
            bool rez = true;
            bool ispravnasifra = true;
            bool ispravannaziv = true;


            if (TextBoxSifraGeoPodrucja.Text.Trim().Equals("") || !System.Text.RegularExpressions.Regex.IsMatch(TextBoxSifraGeoPodrucja.Text, @"^[a-zA-Z]+$"))
            {
                TextBoxSifraGeoPodrucja.BorderBrush = Brushes.Red;
                TextBoxSifraGeoPodrucja.BorderThickness = new Thickness(3);
                System.Windows.MessageBox.Show("Sifra nije dobro unesena, koristiti samo slova", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                rez = false;
                ispravnasifra = false;
            }
            else
            {
                TextBoxSifraGeoPodrucja.BorderBrush = Brushes.Green;
                TextBoxSifraGeoPodrucja.BorderThickness = new Thickness(3);
            }

            

            foreach (var item in medju.GetSveSifreMedju()) 
            {

                if (TextBoxSifraGeoPodrucja.Text.Trim() == item)
                {
                    TextBoxSifraGeoPodrucja.BorderBrush = Brushes.Red;
                    TextBoxSifraGeoPodrucja.BorderThickness = new Thickness(3);
                    System.Windows.MessageBox.Show("Sifra vec postoji", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                    rez = false;
                    ispravnasifra = false;
                }
            }

            if (ispravnasifra)
            {
                TextBoxSifraGeoPodrucja.BorderBrush = Brushes.Green;
                TextBoxSifraGeoPodrucja.BorderThickness = new Thickness(3);
            }

            if (TextBoxNazivGeoPodrucja.Text.Trim().Equals("") || !System.Text.RegularExpressions.Regex.IsMatch(TextBoxNazivGeoPodrucja.Text, @"^([a-zA-Z]+)(\ ?)([a-zA-Z]+)$"))
            {
                TextBoxNazivGeoPodrucja.BorderBrush = Brushes.Red;
                TextBoxNazivGeoPodrucja.BorderThickness = new Thickness(3);
                System.Windows.MessageBox.Show("Naziv podrucja nije dobro unesen, koristiti samo slova", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                rez = false;
                ispravannaziv = false;
            }
            else
            {
                TextBoxNazivGeoPodrucja.BorderBrush = Brushes.Green;
                TextBoxNazivGeoPodrucja.BorderThickness = new Thickness(3);
            }

           

            foreach (var item in medju.GetSvaPodrucjaMedju())
            {
                if (TextBoxNazivGeoPodrucja.Text.Trim() == item)
                {
                    TextBoxNazivGeoPodrucja.BorderBrush = Brushes.Red;
                    TextBoxNazivGeoPodrucja.BorderThickness = new Thickness(3);
                    System.Windows.MessageBox.Show("Podrucje vec postoji", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                    rez = false;
                    ispravannaziv = false;
                }
            }

            if (ispravannaziv)
            {
                TextBoxNazivGeoPodrucja.BorderBrush = Brushes.Green;
                TextBoxNazivGeoPodrucja.BorderThickness = new Thickness(3);
            }



            return rez;
        }

        private void OcistiPoljazaFun()
        {
            ComboSifra.SelectedIndex = -1;
            DatePick.SelectedDate = null;

        }

        private void DodajPodrucjeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ValidacijaZaDodavanjePodrucja())
            {
                medju.UpisNovogPodrucjaMedju(TextBoxSifraGeoPodrucja.Text.Trim(), TextBoxNazivGeoPodrucja.Text.Trim());
                OsveziComboBoxove();
                System.Windows.MessageBox.Show("Podrucje Uspesno Dodato!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
                TextBoxSifraGeoPodrucja.Text = String.Empty;
                TextBoxNazivGeoPodrucja.Text = String.Empty;
            }
        }

        private void DodajMerenjeBtn_Click(object sender, RoutedEventArgs e)
        {

            if (ValidacijaZaDodavanjeMerenja())
            {
                medju.UpisNovogMerenjaMedju(medju.GetSifreByNazivPodrucjaMedju(ComboNazivPodrucja.SelectedItem.ToString()), (DateTime)DateTimePicker.Value, float.Parse(textBoxPotrosnja.Text.Trim()));
                OsveziComboBoxove();
                System.Windows.MessageBox.Show("Merenje Uspesno Dodato!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
                ComboNazivPodrucja.SelectedIndex = -1;
                DateTimePicker.Value = null;
                textBoxPotrosnja.Text = String.Empty;
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            var rez = new Rezultati();
            rez.Show();
        }

        private void Fun3Btn_Click(object sender, RoutedEventArgs e)
        {
            if (ValidacijaZaIzvrsenjeFun())
            {
                medju.InvokeSingleMedju(DatePick.SelectedDate.Value.Date, medju.GetSifreByNazivPodrucjaMedju(ComboSifra.SelectedItem.ToString()), 3);
                OcistiPoljazaFun();
                System.Windows.MessageBox.Show("Fun3 Uspesno Izvrsena!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Fun2Btn_Click(object sender, RoutedEventArgs e)
        {
            if (ValidacijaZaIzvrsenjeFun())
            {
                medju.InvokeSingleMedju(DatePick.SelectedDate.Value.Date, medju.GetSifreByNazivPodrucjaMedju(ComboSifra.SelectedItem.ToString()), 2);
                OcistiPoljazaFun();
                System.Windows.MessageBox.Show("Fun2 Uspesno Izvrsena!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Fun1Btn_Click(object sender, RoutedEventArgs e)
        {
            if (ValidacijaZaIzvrsenjeFun())
            {
                medju.InvokeSingleMedju(DatePick.SelectedDate.Value.Date, medju.GetSifreByNazivPodrucjaMedju(ComboSifra.SelectedItem.ToString()), 1);
                OcistiPoljazaFun();
                System.Windows.MessageBox.Show("Fun1 Uspesno Izvrsena!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

       

        private void ResidentCheck_Click(object sender, RoutedEventArgs e)
        {
            var ts = new CancellationTokenSource();
            CancellationToken ct = ts.Token;

 
            if (ResidentCheck.IsChecked == true)
            {
                t = new Task(() =>
                {
                    while(true)
                    {
                        medju.InvokeResidentMedju();
                    }
                   
                }, ct);
                t.Start();

            }
            else
            {
                ts.Cancel();
            }
        

        }
    }
}
