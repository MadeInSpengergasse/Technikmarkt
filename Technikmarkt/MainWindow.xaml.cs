using System;

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace Technikmarkt {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window {

        TechnikmarktEntities db = new TechnikmarktEntities();

        IEnumerable<a_anbieter> lB_anbieter_IS_orig;
        IEnumerable<h_haendler> lB_haendler_IS_orig;
        Wrapper searchResult = new Wrapper();


        public MainWindow() {
            InitializeComponent();

            /*FrameworkElement.StyleProperty.OverrideMetadata(typeof(Window), new FrameworkPropertyMetadata {
                DefaultValue=FindResource(typeof(Window))
            });*/

            lB_anbieter_IS_orig = (IEnumerable<a_anbieter>) Listbox_anbieter.ItemsSource;
            lB_haendler_IS_orig = (IEnumerable<h_haendler>) Listbox_haendler.ItemsSource;
        }

        private void suchfeld_button_click(object sender, RoutedEventArgs e) {
            if(!string.IsNullOrWhiteSpace(suchfeld.Text)) {
                toggleNavigation(true);
                toggleSpinner(true);

                string name = suchfeld.Text.Trim().ToLower();
                TaskScheduler ui = TaskScheduler.FromCurrentSynchronizationContext();

                Task.Factory.StartNew(() => {
                    Wrapper ah = new Wrapper();

                    List<a_anbieter> a1 = (from a in db.a_anbieter where a.a_anbietername.ToLower().StartsWith(name) select a).ToList();
                    List<h_haendler> h1 = (from h in db.h_haendler where h.h_haendlername.ToLower().StartsWith(name) select h).ToList();

                    ah.Anbieter=a1;
                    ah.Haendler=h1;

                    // Simulate lengthy database lookups
                    Thread.Sleep(1000);

                    return ah;
                }).ContinueWith((task => {
                    searchResult=task.Result;
                    Listbox_anbieter.ItemsSource=searchResult.Anbieter;
                    Listbox_haendler.ItemsSource=searchResult.Haendler;
                    toggleSpinner(false);
                }), ui);
            } else {
                restoreDefaultView();
            }
        }

        private void back_button_click(object sender, RoutedEventArgs e) {
            restoreDefaultView();
        }

        private void toggleSpinner(bool on) {
            if(on) { Listbox_anbieter.ItemsSource=Listbox_haendler.ItemsSource=null; spinner.Visibility=spinner2.Visibility=Visibility.Visible; } else { spinner.Visibility=spinner2.Visibility=Visibility.Hidden; }
        }

        private void toggleNavigation(bool backNav) {
            if(backNav) { home.Visibility=Visibility.Hidden; back.Visibility=Visibility.Visible; } else { back.Visibility=Visibility.Hidden; home.Visibility=Visibility.Visible; }
        }

        private void restoreDefaultView() {
            toggleNavigation(false);
            Listbox_anbieter.ItemsSource=lB_anbieter_IS_orig.ToList();
            Listbox_haendler.ItemsSource=lB_haendler_IS_orig.ToList();
        }

        private struct Wrapper {
            private static List<a_anbieter> anbieter;
            private static List<h_haendler> haendler;

            public List<a_anbieter> Anbieter {
                get { return anbieter; }
                set { anbieter=value; }
            }

            public List<h_haendler> Haendler {
                get { return haendler; }
                set { haendler=value; }
            }
        }
    }
}