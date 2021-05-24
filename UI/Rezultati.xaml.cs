using Common.DAO;
using DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UI
{
    /// <summary>
    /// Interaction logic for Rezultati.xaml
    /// </summary>
    public partial class Rezultati : Window
    {

        public static BindingList<IRezultat> listBinding { get; set; }
        DAORezultat dAORezultat = new DAORezultat();
        public Rezultati()
        {

            List<IRezultat> rezultati = dAORezultat.GetRezultat();
            listBinding = new BindingList<IRezultat>(rezultati);
            DataContext = this;
            InitializeComponent();
        }

        private void IzlazBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            List<IRezultat> rezultati = dAORezultat.GetRezultat();
            listBinding = new BindingList<IRezultat>(rezultati);
            DataContext = this;
        }
    }
}
