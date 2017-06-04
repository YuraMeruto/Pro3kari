using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PornS : SkillBase {

    public GameObject master;
    void Start()
    {

    }
    public override void AtTheStart() {
        Debug.Log("ポーンのスタート");
    }

    public override void AtTheEnd()
    {
        Debug.Log("ポーンだよ終わり");
    }


    public override void MoveStart()
    {
        Debug.Log("ポーンが動き始めるよ");
    }

    public override void MoveEnd()
    {
        Debug.Log("ポーンの動き終わるよ");
    }

    public override void BattleStart()
    {
        Debug.Log("ポーンのバトルスタート");
    }

    public override void BattleEnd()
    {
        Debug.Log("ポーンのバトル修了");
    }
}
