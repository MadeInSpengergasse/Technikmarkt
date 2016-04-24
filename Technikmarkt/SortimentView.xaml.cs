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
            //liklassen.ItemsSource = (from k in  db.klassens
            //                         select k).ToList();

            var erg = db.p_produkt.Where(c => c.p_a_anbietername == ((a_anbieter)DataContext).a_anbietername);
            erg.Load();                         // Variante wo man Klassen auch editieren könnte
            listbox_produkte.ItemsSource=erg.ToList();  // local ist Observable stellt die vorher mit Load geladenen Daten dar
        }

        private void speichern_Click(object sender, RoutedEventArgs e) {
            fehler.Text="";

            try {
                var p1 = (p_produkt) listbox_produkte.SelectedItem;
                if(p1.p_gtin>=1000000000000) {

                    int anzzeilen = db.SaveChanges();
                    fehler.Text=anzzeilen+" Datensatz geändert.";
                } else {
                    fehler.Text="GTIN must have 13 digits!";
                }
            } catch(Exception e1) {
                fehler.Text=e1.Message;   // zeige alle Inner Exceptins
                for(var ex = e1.InnerException; ex!=null; ex=ex.InnerException)
                    fehler.Text=fehler.Text+" / "+ex.Message;
            }
        }

        private void neu_Click(object sender, RoutedEventArgs e) {
            var p1 = new p_produkt();
            p1.p_name="Demo Name";
            //emin was 420 blazed
            //p1.p_a_anbietername = (((sender as Button).Parent as StackPanel).Children[5] as TextBlock).Text == null ? "" : (((sender as Button).Parent as StackPanel).Children[5] as TextBlock).Text;
            p1.p_a_anbietername=produkt_anbieter.Text;
            db.p_produkt.Add(p1);

            ((List<p_produkt>) listbox_produkte.ItemsSource).Add(p1);  // setze die Klasse durch zuweisen zum nav. Property

            listbox_produkte.Items.Refresh();
        }

        private void loesch_Click(object sender, RoutedEventArgs e) {
            if(listbox_produkte.SelectedItem!=null) {
                var p1 = (p_produkt) listbox_produkte.SelectedItem;

                //((klassen)liklassen.SelectedItem).schuelers.Remove(sl);

                try {
                    var entry = db.Entry(p1);
                    if(entry.State==EntityState.Detached)
                        db.p_produkt.Attach(p1);
                    db.p_produkt.Remove(p1);

                    int anzzeilen = db.SaveChanges();
                    fehler.Text=anzzeilen+" Datensatz gelöscht!";

                } catch(Exception e1) {
                    fehler.Text=e1.Message;   // zeige alle Inner Exceptins
                    for(var ex = e1.InnerException; ex!=null; ex=ex.InnerException)
                        fehler.Text=fehler.Text+" / "+ex.Message;
                }

                ((List<p_produkt>) listbox_produkte.ItemsSource).Remove(p1);
                listbox_produkte.Items.Refresh();
            }
        }
    }
}
