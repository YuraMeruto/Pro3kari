using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mucus : SkillBase
{



    [SerializeField]
    private GameObject SkillInstanceObj;
    [SerializeField]
    private GameObject Master;
    private GameObject CameraObj;
    private GameObject Parent;
    [SerializeField]
    private GameObject PlayerObj;
    [SerializeField]
    private int RecoveryNum;
    void Start()
    {
        Parent = gameObject.transform.parent.gameObject;
        Master = GameObject.Find("Master");
        PlayerObj = GameObject.Find("Main Camera");
        Debug.Log(PlayerObj);
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
         HpRecoverySkill();
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
        Debug.Log(gameObject);
    }

    public override void MoveStart()
    {
    }
    public override void MoveEnd()
    {
    }
    public override void EnemyMoveEnd()
    {
    }
    void HpRecoverySkill()
    {
        Debug.Log("始まりの粘液のスキル発動");
        Parent.GetComponent<CharacterStatus>().SetHpAdd(RecoveryNum);
    }
}
