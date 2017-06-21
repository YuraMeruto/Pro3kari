using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : MonoBehaviour {

    public virtual void MyTurnStart() { }
    public virtual void MyTurnEnd() { }
    public virtual void EnemyTurnStart() { }
    public virtual void EnemyTurnEnd() { }
    public virtual void AtTheStart() { }
//    public virtual bool Is_AtTheStart() { return true; }
    public virtual void AtTheEnd() { }
 //   public virtual bool Is_AtTheEnd() { return true; }
    public virtual void MoveStart() { }
 //   public virtual bool Is_MoveStart() { return true; }
    public virtual void MoveEnd() { }
  //  public virtual bool Is_MoveEnd() { return true; }
    public virtual void BattleStart() { }
  //  public virtual bool Is_BattleStart() { return true; }
    public virtual void BattleEnd() { }
  //  public virtual bool Is_BattleEnd() { return true; }
}
