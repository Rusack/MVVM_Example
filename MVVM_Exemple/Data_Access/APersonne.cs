using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace MVVM_Exemple
{
    // Accès
    public class APersonne : ABase
    {
        #region Constructeur
        public APersonne(string sChaineConnexion)
            : base(sChaineConnexion)
        { }
        #endregion
        #region Méthodes
        public int Ajouter(string Nom_, string Pre_, DateTime? Nai_)
        {
            CreerCommande("AjouterPersonne", true);
            int res = -1;
            Commande.Parameters.Add("@ID", SqlDbType.Int);
            Direction("@ID", ParameterDirection.Output);
            Commande.Parameters.AddWithValue("@NOM", Nom_);
            Commande.Parameters.AddWithValue("@PRE", Pre_);
            if (Nai_ == null)
                Commande.Parameters.AddWithValue("@NAI", Convert.DBNull);
            else
                Commande.Parameters.AddWithValue("@NAI", Nai_);
            Commande.Connection.Open();
            Commande.ExecuteNonQuery();
            res = int.Parse(LireParametres("@ID"));
            Commande.Connection.Close();
            return res;
        }
        public int Modifier(int ID_, string Nom_, string Pre_, DateTime? Nai_)
        {
            CreerCommande("ModifierPersonne", true);
            int res = -1;
            Commande.Parameters.AddWithValue("@ID", ID_);
            Commande.Parameters.AddWithValue("@NOM", Nom_);
            Commande.Parameters.AddWithValue("@PRE", Pre_);
            if (Nai_ == null)
                Commande.Parameters.AddWithValue("@NAI", Convert.DBNull);
            else
                Commande.Parameters.AddWithValue("@NAI", Nai_);
            Commande.Connection.Open();
            res = Commande.ExecuteNonQuery();
            Commande.Connection.Close();
            return res;
        }
        public List<CPersonne> Lire(string Index)
        {
            CreerCommande("SelectionnerPersonne", true);
            Commande.Parameters.AddWithValue("@Index", Index);
            Commande.Connection.Open();
            SqlDataReader dr = Commande.ExecuteReader();
            List<CPersonne> liste = new List<CPersonne>();
            while (dr.Read())
            {
                CPersonne tmp = new CPersonne();
                tmp.ID = int.Parse(dr["ID"].ToString());
                tmp.Nom = dr["Nom"].ToString();
                tmp.Pre = dr["Pre"].ToString();
                if (dr["Nai"] != DBNull.Value)
                    tmp.Nai = DateTime.Parse(dr["Nai"].ToString());
                liste.Add(tmp);
            }
            dr.Close();
            Commande.Connection.Close();
            return liste;
        }
        public CPersonne Lire_ID(int ID_)
        {
            CreerCommande("SelectionnerPersonne_ID", true);
            Commande.Parameters.AddWithValue("@ID", ID_);
            Commande.Connection.Open();
            SqlDataReader dr = Commande.ExecuteReader();
            CPersonne rep = new CPersonne();
            if (dr.Read())
            {
                rep.ID = int.Parse(dr["ID"].ToString());
                rep.Nom = dr["Nom"].ToString();
                rep.Pre = dr["Pre"].ToString();
                if (dr["Nai"] != DBNull.Value)
                    rep.Nai = DateTime.Parse(dr["Nai"].ToString());
            }
            dr.Close();
            Commande.Connection.Close();
            return rep;
        }
        public int Supprimer(int ID_)
        {
            CreerCommande("SupprimerPersonne", true);
            Commande.Parameters.AddWithValue("@ID", ID_);
            Commande.Connection.Open();
            int rep = Commande.ExecuteNonQuery();
            Commande.Connection.Close();
            return rep;
        }
        #endregion
    }
}
