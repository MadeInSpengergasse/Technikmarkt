using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Technikmarkt {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window {

        TechnikmarktEntities db = new TechnikmarktEntities();

        public MainWindow() {
            InitializeComponent();
        }

        private void suchfeld_button_click(object sender, RoutedEventArgs e) {
            if(!string.IsNullOrWhiteSpace(suchfeld.Text)) {
                string name = suchfeld.Text.Trim().ToLower();

                IEnumerable<a_anbieter> anbieter = from a in db.a_anbieter where a.a_anbietername.ToLower().StartsWith(name) select a;
                Listbox_anbieter.ItemsSource=anbieter.ToList();

                IEnumerable<h_haendler> haendler = from h in db.h_haendler where h.h_haendlername.ToLower().StartsWith(name) select h;
                Listbox_haendler.ItemsSource=haendler.ToList();
            } else {
                MessageBox.Show("Bitte geben sie einen zu suchenden Händler- oder Anbieternamen ein!", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}