using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Technikmarkt.model;

namespace Technikmarkt.model {
    public class Viewmodel_Technikmarkt {

        TechnikmarktEntities db = new TechnikmarktEntities();

        public IEnumerable<a_anbieter> AlleAnbieter {
            get {
                // Only get the first 40 entries to avoid slowdowns at program start
                return (from a in db.a_anbieter
                        orderby a.a_anbietername
                        select a).Take(40).ToList();
            }
        }

        public IEnumerable<h_haendler> AlleHaendler {
            get {
                // Only get the first 40 entries to avoid slowdowns at program start
                return (from h in db.h_haendler
                        orderby h.h_haendlername
                        select h).Take(40).ToList();
            }
        }

        ICommand saveinsertstundeCommand;
        public ICommand SaveInsertCommand {
            get {
                if(saveinsertstundeCommand==null) {
                    saveinsertstundeCommand = new DelegateCommand(SaveExecuted, SaveCanExecute);
                }
                return saveinsertstundeCommand;
            }
        }

        public bool SaveCanExecute(object param) {
            if(param==null) {
                return false;
            }
            return true;
        }

        public void SaveExecuted(object param) {
            if(param is a_anbieter) {
                a_anbieter anbieter1 = param as a_anbieter;
                SortimentView sv = new SortimentView();
                sv.DataContext=anbieter1;
                sv.ShowDialog();
            }

            if(param is h_haendler) {
                try {
                    System.Diagnostics.Process.Start("http://www."+(param as h_haendler).h_haendlerwebseite);

                } catch(System.ComponentModel.Win32Exception noBrowser) {
                    if(noBrowser.ErrorCode==-2147467259) {
                        MessageBox.Show(noBrowser.Message);
                    }
                } catch(System.Exception other) {
                    MessageBox.Show(other.Message);
                }
            }
        }
    }
}

