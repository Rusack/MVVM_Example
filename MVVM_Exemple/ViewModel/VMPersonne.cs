using MVVM_Exemple.Utilitaire;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
// To Add this using, you must add a reference to the framework System.web, Menu Project --> Add Reference --> Chose Framework --> Choose System.Web dll and click OK. Error will be removed
using System.Web.UI;
using System.IO;
using System.Reflection;
using MVVM_Exemple.View;

namespace MVVM_Exemple.ViewModel
{
    // Vue Modèle (chaque modèle à son vue modèle correspondant et sa vue correspondante)
    public class VMPersonne : INotifyPropertyChanged
    {

        #region Element hors modèle utilisé pour du binding

        // Utilisé pour décider de si un élement est enabled ou non, ici le stack panel qui contient les textbox
        private bool _StackPanelAccessible = false;

        public bool StackPanelAccessible
        {
            get { return _StackPanelAccessible; }
            set { _StackPanelAccessible = value;
                NotifyPropertyChanged("StackPanelAccessible");
            }
        }

        private ActionsPossibles _ActionEnCours = ActionsPossibles.VISUALISER;

        public ActionsPossibles ActionEnCours
        {
            get { return _ActionEnCours; }
            set { _ActionEnCours = value;
                NotifyPropertyChanged("ActionEnCours");
            }
        }
        #endregion
        #region Element du modèle à afficher
        // Notre liste de personne
        private ObservableCollection<CPersonne>  _Personnes = new ObservableCollection<CPersonne>();

        public ObservableCollection<CPersonne> Personnes
        {
            get { return _Personnes; }
            set { _Personnes = value;
                NotifyPropertyChanged("Personnes");
            }
        }

        // La personne qui est sélectionnée dans l'UI 
        private CPersonne _CurrentPersonne = new CPersonne();

        public CPersonne CurrentPersonne
        {
            get { return _CurrentPersonne; }
            set { _CurrentPersonne = value;
                NotifyPropertyChanged("CurrentPersonne");
            }
        }
        #endregion
        #region Accès à la couche gestion
        private GPersonne _GererPersonne;

        public GPersonne GererPersonne
        {
            get { return _GererPersonne; }
            set { _GererPersonne = value; }
        }

        #endregion
        #region Commandes
        // Afin de binder des actions à des boutons en WPF, on utilisera des commandes, cela permet d'isoler l'action du bouton dans le vue modèle plutôt que le laisser dans 
        //le code behind de la vue.
        // En effet, WPF repose sur le principe d'une indépendance la plus grande entre les couches, donc limiter les interactions de la vue avec les autres couches au minimum
        // Une commande peut être bindé à des événements de la vue, et sera déclarée dans le vueModèle comme les autres objets bindés.

        // On défini une nouvelle commande, mais on ne dit pas encore ce qu'elle fera, cela se fait dans le constructeur de la commande
        private RelayCommand _Commande_RecupererTousEnregistrements;
        public RelayCommand Commande_RecupererTousEnregistrements
        {
            get
            {return _Commande_RecupererTousEnregistrements;}
            set
            {_Commande_RecupererTousEnregistrements = value;}
        }

        private RelayCommand _Commande_ValiderEnregistrement;
        public RelayCommand Commande_ValiderEnregistrement
        {
            get
            {return _Commande_ValiderEnregistrement;}
            set
            {_Commande_ValiderEnregistrement = value;}
        }

        private RelayCommand _Commande_AjouterPersonne;
        public RelayCommand Commande_AjouterPersonne
        {
            get
            {return _Commande_AjouterPersonne;}
            set
            {_Commande_AjouterPersonne = value;}
        }

        private RelayCommand _Commande_ModifierPersonne;
        public RelayCommand Commande_ModifierPersonne
        {
            get
            {return _Commande_ModifierPersonne;}
            set
            { _Commande_ModifierPersonne = value;}
        }
        private RelayCommand _Commande_SupprimerPersonne;

        public RelayCommand Commande_SupprimerPersonne
        {
            get { return _Commande_SupprimerPersonne; }
            set { _Commande_SupprimerPersonne = value; }
        }

        private RelayCommand _Commande_GenererRapportHTML;

        public RelayCommand Commande_GenererRapportHTML
        {
            get { return _Commande_GenererRapportHTML; }
            set { _Commande_GenererRapportHTML = value; }
        }

        #endregion
        #region Constructeur
        public VMPersonne()
        {

            // Initialise l'objet de gestion des données (Accès aux autres couches pour récupérer les données)
            // La chaine de connexion est placée dans une classe de configuration pour plus de simplicité
            GererPersonne = new GPersonne(ExempleConfiguration.ChaineDeConnexion);

            // Définit l'action de notre commande en lui donnant la fonction à exécuter, ici récupérer tous les enregistrements
            Commande_RecupererTousEnregistrements = new RelayCommand(RecupererTousEnregistrements);

            Commande_ValiderEnregistrement = new RelayCommand(ValiderEnregistrement);
            Commande_AjouterPersonne = new RelayCommand(AjouterPersonne);
            Commande_ModifierPersonne = new RelayCommand(ModifierPersonne);
            Commande_SupprimerPersonne = new RelayCommand(SupprimerPersonne);
            Commande_GenererRapportHTML = new RelayCommand(GenererRapportHTML);

            // récupération initiale de tous les enregistrements 
            RecupererTousEnregistrements(null);
        }
        #endregion
        #region Fonctions
        // Toutes les fonctions doivent prendre en arguement un object obj, au cas où l'on veut passer un argument à la Command
        private void RecupererTousEnregistrements(object obj)
        {
            // Recupère tous les enregistrements sous forme d'une liste, une fois récupérée la list est transformée en observable collection (Nécessaire pour le binding)
            Personnes = new ObservableCollection<CPersonne>(GererPersonne.Lire(""));
        }

        private void ValiderEnregistrement(object obj)
        {
            // on utilise des booléens pour savoir si on est en train de modifier ou ajouter un nouvel élément

            switch (ActionEnCours)
            {
                case ActionsPossibles.VISUALISER:
                    break;
                case ActionsPossibles.AJOUTER:
                    // pour ajouter un enregistrement, vu que notre objet CurrentPersonne est associée à la personne sélectionnée dans l'UI et donc celle étant d'être en train d'être ajoutée
                    // On peut passer les propriétés de cet objet direct à notre fonction ajouter
                    GererPersonne.Ajouter(CurrentPersonne.Nom, CurrentPersonne.Pre, CurrentPersonne.Nai);
                    break;
                case ActionsPossibles.MODIFIER:
                    GererPersonne.Modifier(CurrentPersonne.ID, CurrentPersonne.Nom, CurrentPersonne.Pre, CurrentPersonne.Nai);
                    break;
                case ActionsPossibles.SUPPRIMER:
                    
                    break;
                default:
                    break;
            }
            StackPanelAccessible = false;
            ActionEnCours = ActionsPossibles.VISUALISER;
            // Une fois l'action effectuée, il faut rafraichir 
            RecupererTousEnregistrements(null);
        }

        private void AjouterPersonne(object obj)
        {
            // Rend le panel sur le côté accessible pour pouvoir modifier les champs
            StackPanelAccessible = true;
            ActionEnCours = ActionsPossibles.AJOUTER;
            // Créé un nouvel objet personne qui sera à ajouter (sert à avoir les champs des tb vides)
            CurrentPersonne = new CPersonne();
        }
        private void ModifierPersonne(object obj)
        {
            StackPanelAccessible = true;
            ActionEnCours = ActionsPossibles.MODIFIER;
        }

        private void SupprimerPersonne(object obj)
        {
            // On gère la suppression avec un pop up qui demande si l'on est sûr de vouloir supprimer
            ActionEnCours = ActionsPossibles.SUPPRIMER;
            if (MessageBox.Show("Voulez Vous vraiment supprimer cet enregistrement ? ", "Attention", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                GererPersonne.Supprimer(CurrentPersonne.ID);
            }
            ActionEnCours = ActionsPossibles.VISUALISER;

            // On actualise la liste des enregistrements, (ici on ne passe pas par validerEnregistrement, donc on est obligé de le mettre une seconde fois)
            RecupererTousEnregistrements(null);
        }

        // Idéalement il faudrait que cette fonction, soit indépendante du modèle (ici CPersonne), que celle-ci s'adapte, à voir si cela est possible
        private void GenererRapportHTML(object obj)
        {
            StringWriter stringWriter = new StringWriter();

            HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);

            // <html>
            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Html);

            // <head> Si nécessaire d'ajouter du CSS
            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Head);

            // Bootstrap pour que ce soit plus joli
            htmlWriter.Write("<meta http-equiv=\"x-ua-compatible\" content=\"IE=edge\" >");
            htmlWriter.Write("<link rel=\"stylesheet\" href=\"bootstrap/bootstrap.min.css\">");
            htmlWriter.Write("<script src=\"bootstrap/jquery.min.js\"></script>");
            htmlWriter.Write("<script src=\"bootstrap/bootstrap.min.js\"></script>");


            // Met les caractères en UTF-8
            htmlWriter.Write("<meta charset=\"utf-8\">");
             // </head>
             htmlWriter.RenderEndTag();

            // <body>
            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Body);

            // Mets l'attribut classe de la prochaine balise égal à "container"
            htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, "container");

            // <div class="container">
            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Div);

            // class="table-bordered"
            htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, "table table-bordered");

            // <table class="table table-bordered">
            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Table);

            // HEADER TABLEAU

            // <thead> commence une ligne du tableau
            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Thead);

            // Ligne header du tableau
            // <tr> commence une ligne du tableau
            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr);

            // PropertyInfo permet de récupérer le nom des propriétés d'une classe, on s'en sert pour fabriquer le header du tableau
            foreach (PropertyInfo prop in typeof(CPersonne).GetProperties())
            {
                // <th> cellule header
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Th);
                // écrit à l'intérieur des tags html, le nom de la propriété
                htmlWriter.Write(prop.Name.ToString());
                // </th> fin cellule header
                htmlWriter.RenderEndTag();
            }


            // </tr> termine une ligne
            htmlWriter.RenderEndTag();
            // Fin ligne header du tableau

            // </thead> termine une ligne
            htmlWriter.RenderEndTag();

            // FIN HEADER TABLEAU

            // CORPS TABLEAU
            // <tbody>
            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tbody);

            // Pour chaque element de notre modèle dans la liste 
            foreach (var personne in Personnes)
            {
                // <tr> commence une ligne du tableau
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr);

                // Même principe qu'avant sauf qu'on va afficher la valeur de la propriété d'un élément
                foreach (PropertyInfo prop in typeof(CPersonne).GetProperties())
                {
                    // <td> cellule du tableau
                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                    try
                    {
                        // passe à GetValue l'objet dans lequel la méthode doit aller rechercher la valeur de la propriété
                        String valeurPropriete = prop.GetValue(personne).ToString();
                        // écrit à l'intérieur des tags html, la valeur de l'élément
                        htmlWriter.Write(valeurPropriete);
                    }
                    catch (NullReferenceException)
                    {
                        // Si la valeur de la propriété est nulle, il y a une exception, dans ce cas on écrit rien dans la cellule
                        htmlWriter.Write("");
                    }

                    // </td> fin cellule du tableau
                    htmlWriter.RenderEndTag();
                }

                // </tr> termine une ligne
                htmlWriter.RenderEndTag();
            }

            // </tbody>
            htmlWriter.RenderEndTag();

            // FIN CORPS TABLEAU

            // </table>
            htmlWriter.RenderEndTag();

            // </div>
            htmlWriter.RenderEndTag();

            // </body>
            htmlWriter.RenderEndTag();

            // </html>
            htmlWriter.RenderEndTag();

            File.WriteAllText("report.html", stringWriter.ToString());


            // Fait partie de la vue, ne devrait pas se trouver là, si des éléments de la vue se retrouve dans le VM -> violation du modèle MVVM
            HTML_Viewer viewer = new HTML_Viewer();
            viewer.AfficherRapport();
            viewer.Show();

        }

        #endregion
        // IMPORTANT : Le Vue modèle doit implémenter l'interface INotifyPropertyChanged si des variables  sont utilisées pour le binding, ici nos variables modèles et nos variable qui décident 
        // de l'activation de certains éléments
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
