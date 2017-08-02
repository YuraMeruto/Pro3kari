using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jajah : SkillBase
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
        DestroySkill();
    }
    public override void AtTheEnd()
    {
        DestroySkill();

    }
    public override void BattleStart()
    {
        DestroySkill();

    }
    public override void BattleEnd()
    {
    }

    public override void EnemyMoveEnd()
    {
    }

    public override void MoveStart()
    {
        DestroySkill();

    }
    public override void MoveEnd()
    {
    }
    public override void DestroyChar()
    {

    }
    public override void SkillTargetActive()
    {

    }
    public void DestroySkill()
    {
        bool IsGetSkill = Parent.GetComponent<CharacterStatus>().GetIsSkill();
        if (!IsGetSkill)
        {
            return;
        }
        Debug.Log("ジャジャスキル発動");
        if (SkillCount <= 0)
        {
            Debug.Log("スキルが発動できません");
            return;
        }
      GameObject  targetobj=  PlayerObj.GetComponent<AtachMaster>().GetAttachEnemyObj();
       // bool ret = Master.GetComponent<BoardList>().GetSkillTargetList(targetobj);
//        Debug.Log("retの結果は"+ret);
        if(targetobj != null)
        {
            Debug.Log("ターゲットを消滅させた");
            Destroy(targetobj);
            PlayerObj.GetComponent<MouseState>().SetIsTargetBool(true);
            Master.GetComponent<BoardList>().ClearSkillTargetList();
            PlayerObj.GetComponent<BattleScene>().SetIsBattleStart(false);
        }
 


    }
}
