using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : MonoBehaviour {

    public virtual void AtTheStart() { }
    public virtual void AtTheEnd() { }
    public virtual void MoveStart() { }
    public virtual void MoveEnd() { }
    public virtual void BattleStart() { }
    public virtual void BattleEnd() { }
}
