using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Exemple
{
 public class GBase
 {
  #region Donnée membre
  string _ChaineConnexion;
  #endregion
  #region Constructeurs
  public GBase()
  { _ChaineConnexion = ""; }
  public GBase(string ChaineConnexion_)
  { _ChaineConnexion = ChaineConnexion_; }
  #endregion
  #region Accesseur
  public string ChaineConnexion
  {
   get { return _ChaineConnexion; }
   set { _ChaineConnexion = value; }
  }
  #endregion
 }
}
