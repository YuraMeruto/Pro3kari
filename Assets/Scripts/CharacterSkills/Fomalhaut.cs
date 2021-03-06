﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fomalhaut : SkillBase
{

    [SerializeField]
    private int SkillCount = 1;
    [SerializeField]
    private GameObject SkillInstanceObj;
    [SerializeField]
    private GameObject Master;
    private GameObject CameraObj;
    private GameObject Parent;
    [SerializeField]
    private GameObject PlayerObj;
    void Start()
    {
        Parent = gameObject.transform.parent.gameObject;
        Master = GameObject.Find("Master");
        PlayerObj = GameObject.Find("Main Camera");
        Master.GetComponent<BoardSkillList>().SetMyMoveEndSkillAdd(Parent);
    }
    private int NowMyPosLength;
    private int NowMyPosSide;
    private int MaxLength;
    private int MaxSide;
    [SerializeField]
    GameObject IsEnemyObj;


    [SerializeField]
    private GameObject MoveAreaObj;

    public override void MyTurnStart()
    {

    }

    public override void MyTurnEnd()
    {

    }

    public override void EnemyTurnStart()
    {
    }

    public override void EnemyTurnEnd()
    {

    }

    public override void AtTheStart()
    {
    }
    public override void AtTheEnd()
    {

    }
    public override void BattleStart()
    {

    }
    public override void BattleEnd()
    {

    }

    public override void MoveStart()
    {

    }
    public override void MoveEnd()
    {
        FomalhautSkill();
    }
    public override void EnemyMoveEnd()
    {
    }

    public override void DestroyChar()
    {
        Master.GetComponent<BoardMaster>().ControlObjRelease(Parent);
    }
    public override void SkillTargetActive()
    {

    }
    public void FomalhautSkill()
    {
        //int PlayerNumber = Parent.GetComponent<CharacterStatus>().GetPlayerNumber();
      GameObject MyMass =  PlayerObj.GetComponent<AtachMaster>().GetCopyAttachMassObj();
      GameObject DestinationMass = PlayerObj.GetComponent<AtachMaster>().GetAttachMassObj();
        Master.GetComponent<BoardList>().FomalhautskillResultMoveRoot(MyMass, DestinationMass,Parent);
    }
}
