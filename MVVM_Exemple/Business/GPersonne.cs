using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Exemple
{
 public class GPersonne : GBase
 {
  #region Constructeurs
  public GPersonne()
   : base()
  { }
  public GPersonne(string sChaineConnexion)
   : base(sChaineConnexion)
  { }
  #endregion
  #region Accesseur
  public int Ajouter(string Nom_, string Pre_, DateTime? Nai_)
  { return new APersonne(ChaineConnexion).Ajouter(Nom_, Pre_, Nai_); }
  public int Modifier(int ID_, string Nom_, string Pre_, DateTime? Nai_)
  { return new APersonne(ChaineConnexion).Modifier(ID_, Nom_, Pre_, Nai_); }
  public List<CPersonne> Lire(string Index)
  { return new APersonne(ChaineConnexion).Lire(Index); }
  public CPersonne Lire_ID(int ID_)
  { return new APersonne(ChaineConnexion).Lire_ID(ID_); }
  public int Supprimer(int ID_)
  { return new APersonne(ChaineConnexion).Supprimer(ID_); }
  #endregion
 }
}
