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
        Debug.Log("ポーンだよ");
    }

    public override void AtTheEnd()
    {
        Debug.Log("ポーンだよ終わり");
    }


    public override void MoveStart()
    {
        Debug.Log("ポーンが動き始めるよ");
    }

}
