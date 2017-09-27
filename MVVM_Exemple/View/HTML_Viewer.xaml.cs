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
using System.IO;

namespace MVVM_Exemple.View
{
    /// <summary>
    /// Interaction logic for HTML_Viewer.xaml
    /// </summary>
    public partial class HTML_Viewer : Window
    {
        public HTML_Viewer()
        {
            InitializeComponent();
        }

        public void AfficherRapport()
        {
            string curDir = Directory.GetCurrentDirectory();

            reportViewer.Navigate(String.Format("file:///{0}/report.html", curDir));
        }
    }
}
