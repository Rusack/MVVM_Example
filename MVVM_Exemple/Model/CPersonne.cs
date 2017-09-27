using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Exemple
{

    // Modèle
    public class CPersonne : INotifyPropertyChanged
    {
        #region Données membres
        private int _ID;
        private string _Nom, _Pre;
        private DateTime? _Nai;
        #endregion
        #region Constructeurs
        public CPersonne()
        { }
        public CPersonne(string Nom_, string Pre_, DateTime? Nai_)
        {
            Nom = Nom_;
            Pre = Pre_;
            Nai = Nai_;
        }
        public CPersonne(int ID_, string Nom_, string Pre_, DateTime? Nai_)
         : this(Nom_, Pre_, Nai_)
        { ID = ID_; }
        #endregion

        // Dans les accesseurs, à chaque fois qu'une propriété est set, il faut appeler NotifyPropertyChanged pour que les autres composants soient signalés de ce changement
        #region Accesseurs
        public int ID
        {
            get { return _ID; }
            set { _ID = value;
                // Dans le setter on va ajouter un appel à NotifyPropertyChanged en lui passant le nom de la propriété
                NotifyPropertyChanged("ID");
            }
        }
        public string Nom
        {
            get { return _Nom; }
            set { _Nom = value;
                NotifyPropertyChanged("Nom");
                }
        }
        public string Pre
        {
            get { return _Pre; }
            set { _Pre = value;
                NotifyPropertyChanged("Pre");
                }
        }
        public DateTime? Nai
        {
            get { return _Nai; }
            set { _Nai = value;
                NotifyPropertyChanged("Nai");
                }
        }
        #endregion

        // IMPORTANT : Le modèle doit implémenter l'interface INotifyPropertyChanged, l'implémentation de cette interface sert à signaler les changement de données dans le modèle à d'autres composants
        // en particulier des composants d'UI via le binding
        #region Implementation INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion
    }
}
