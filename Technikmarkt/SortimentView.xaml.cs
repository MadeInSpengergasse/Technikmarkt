using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace Technikmarkt {
    /// <summary>
    /// Interaction logic for SortimentView.xaml
    /// </summary>
    public partial class SortimentView : Window {

        TechnikmarktEntities db = new TechnikmarktEntities();

        public SortimentView() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            var erg = db.p_produkt.Where(c => c.p_a_anbietername == ((a_anbieter)DataContext).a_anbietername);
            erg.Load();
            listbox_produkte.ItemsSource=erg.ToList(); 
        }

        private void save_all_changes_click(object sender, RoutedEventArgs e) {
            fehler.Text="";

            int changes = 0;

            try {
                    changes = db.SaveChanges();
            } catch(Exception e1) {}

            if(changes==1) {
                fehler.Text="1 Datensatz in der Datenbank wurde geändert.";
            } else {
                fehler.Text=changes+" Datensätze in der Datenbank wurden geändert.";
            }
        }

        private void new_item_click(object sender, RoutedEventArgs e) {
            fehler.Text="";

            var p1 = new p_produkt();
            p1.p_name="Demo Name";
            
            p1.p_a_anbietername=produkt_anbieter.Text;
            db.p_produkt.Add(p1);

            ((List<p_produkt>) listbox_produkte.ItemsSource).Add(p1);

            listbox_produkte.Items.Refresh();
        }

        private void delete_current_item_click(object sender, RoutedEventArgs e) {
            fehler.Text="";

            if(listbox_produkte.SelectedItem!=null) {
                var p1 = (p_produkt) listbox_produkte.SelectedItem;

                int changes=0;

                try {
                    db.p_produkt.Remove(p1);

                    changes = db.SaveChanges();
                } catch(Exception e1) {}

                if(changes==0) {
                    fehler.Text="1 lokaler Datensatz wurde gelöscht.";
                } else if(changes==1) {
                    fehler.Text="1 Datensatz in der Datenbank wurde gelöscht.";
                } else {
                    fehler.Text=changes+" Datensätze in der Datenbank wurden gelöscht.";
                }

                ((List<p_produkt>) listbox_produkte.ItemsSource).Remove(p1);
                listbox_produkte.Items.Refresh();
            }
        }
    }
}
