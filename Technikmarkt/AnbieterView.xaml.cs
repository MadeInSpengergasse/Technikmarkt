using System;
using System.Collections.Generic;
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
    /// Interaction logic for AnbieterView.xaml
    /// </summary>
    public partial class AnbieterView : Window {

        TechnikmarktEntities db = new TechnikmarktEntities();

        public AnbieterView() {
            InitializeComponent();
        }

       
         private void speichern_Click(object sender, RoutedEventArgs e) {
             fehler.Text = "";
             
             try {
                var p1 = (p_produkt)listbox_produkte.SelectedItem;
                if (p1.p_gtin >= 1000000000000) {
                    
                    int anzzeilen = db.SaveChanges();
                    fehler.Text = anzzeilen + " Datensätze hinzugefügt";
                }
                else {
                    fehler.Text = "GTIN must have 13 digits";
                }
             }
             catch (Exception e1) {
                 fehler.Text = e1.Message;   // zeige alle Inner Exceptins
                 for (var ex = e1.InnerException; ex != null; ex = ex.InnerException)
                     fehler.Text = fehler.Text + " / " + ex.Message;
             }
         }

         private void neu_Click(object sender, RoutedEventArgs e) {
            var p1 = new p_produkt();
            p1.p_name = "Demo Name";
            //emin was 420 blazed
            //p1.p_a_anbietername = (((sender as Button).Parent as StackPanel).Children[5] as TextBlock).Text == null ? "" : (((sender as Button).Parent as StackPanel).Children[5] as TextBlock).Text;
            p1.p_a_anbietername = produkt_anbieter.Text;
            db.p_produkt.Add(p1);
 
             ((p_produkt)listbox_produkte.SelectedItem).a_anbieter.p_produkt.Add(p1);  // setze die Klasse durch zuweisen zum nav. Property

             listbox_produkte.Items.Refresh();  // nötig, weil das navigational property seit ER 5 nicht mehr observable ist

         }

        private void loesch_Click(object sender, RoutedEventArgs e) {
            if (listbox_produkte.SelectedItem != null) {
                var p1 = (p_produkt)listbox_produkte.SelectedItem;

                // Folgezeile ungeeignet, setzt NUR  S_K_Klasse Fremdschlüssel auf NULL
                //((klassen)liklassen.SelectedItem).schuelers.Remove(sl);

                db.p_produkt.Remove(p1);

                listbox_produkte.Items.Refresh();
            }
        }

    }
}
