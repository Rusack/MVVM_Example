using MVVM_Exemple.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MVVM_Exemple
{
    /// <summary>
    /// Vue pour la fenêtre personne, le code behind de la vue doit être quasi vide, juste faire le lien avec le Vue modèle
    /// </summary>
    public partial class VPersonne : Window
    {
        // Déclare une variable ViewModel qui servira de point d'accès au Vue Modèle par la vue
        private VMPersonne viewModel;

        public VPersonne()
        {
            InitializeComponent();
            viewModel = new VMPersonne();
            // Faire le lien effectif entre vue et vue modèle (changer le data context de la vue pour mettre le vue modèle)
            this.DataContext = viewModel;
        }
    }
}
