using System;

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
        IEnumerable<a_anbieter> lB_anbieter_IS_orig;
        IEnumerable<h_haendler> lB_haendler_IS_orig;

        public MainWindow() {
            InitializeComponent();

            lB_anbieter_IS_orig =(IEnumerable<a_anbieter>) Listbox_anbieter.ItemsSource;
            lB_haendler_IS_orig = (IEnumerable<h_haendler>) Listbox_haendler.ItemsSource;
        }

        private void suchfeld_button_click(object sender, RoutedEventArgs e) {
            if(!string.IsNullOrWhiteSpace(suchfeld.Text)) {
                string name = suchfeld.Text.Trim().ToLower();

                IEnumerable<a_anbieter> anbieter = from a in db.a_anbieter where a.a_anbietername.ToLower().StartsWith(name) select a;
                Listbox_anbieter.ItemsSource=anbieter.ToList();

                IEnumerable<h_haendler> haendler = from h in db.h_haendler where h.h_haendlername.ToLower().StartsWith(name) select h;
                Listbox_haendler.ItemsSource=haendler.ToList();
            } else {
                Listbox_anbieter.ItemsSource=lB_anbieter_IS_orig.ToList();
                Listbox_haendler.ItemsSource=lB_haendler_IS_orig.ToList();
            }
        }
    }
}