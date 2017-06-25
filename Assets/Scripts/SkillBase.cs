using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : MonoBehaviour {

    public virtual void MyTurnStart() { }
    public virtual void MyTurnEnd() { }
    public virtual void EnemyTurnStart() { }
    public virtual void EnemyTurnEnd() { }
    public virtual void AtTheStart() { }
    public virtual void AtTheEnd() { }
    public virtual void MoveStart() { }
    public virtual void MoveEnd() { }
    public virtual void EnemyMoveEnd() { }
    public virtual void BattleStart() { }
    public virtual void BattleEnd() { }
}
