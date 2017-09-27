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
 public class ABase
 {
  #region Données membres
  private SqlCommand _Commande;
  #endregion
  #region Constructeurs
  public ABase(string sChaineConnexion)
  {
   _Commande = new SqlCommand();
   _Commande.Connection = new SqlConnection(sChaineConnexion);
  }
  public ABase(string sChaineConnexion,
   string sCommande, bool lProcedureStockee)
   : this(sChaineConnexion)
  {
   if (lProcedureStockee)
    _Commande.CommandType = CommandType.StoredProcedure;
   else
    _Commande.CommandType = CommandType.Text;
   _Commande.CommandText = sCommande;
  }
  #endregion
  #region Accesseurs
  public SqlCommand Commande
  {
   get { return _Commande; }
   set { _Commande = value; }
  }
  #endregion
  #region Utilitaires
  public void Direction(string sParam, ParameterDirection dParam)
  { Commande.Parameters[sParam].Direction = dParam; }
  public string LireParametres(string sParam)
  { return Commande.Parameters[sParam].Value.ToString(); }
  public void CreerCommande(string sCommande, bool lProcedureStockee)
  {
   if (lProcedureStockee)
    _Commande.CommandType = CommandType.StoredProcedure;
   else
    _Commande.CommandType = CommandType.Text;
   _Commande.CommandText = sCommande;
  }
  #endregion
 }
}
