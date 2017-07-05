using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timegazaer : SkillBase {

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

    private int NowMyPosLength;
    private int NowMyPosSide;
    private int MaxLength;
    private int MaxSide;

    [SerializeField]
    GameObject IsEnemyObj;


    [SerializeField]
    private GameObject MoveAreaObj;

    void Start()
    {
        Parent = gameObject.transform.parent.gameObject;
        Master = GameObject.Find("Master");
        PlayerObj = GameObject.Find("Main Camera");
        Debug.Log(PlayerObj);
    }


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
        Debug.Log("スキル発動タイムゲイザー");
        HpRecoverySkill();
    }
    public override void EnemyMoveEnd()
    {
    }
    public override void DestroyChar()
    {

    }
    public override void SkillTargetActive()
    {

    }
    public override void SkillIsActive()
    {
        HpRecoverySkill();
    }
    void HpRecoverySkill()
    {
       GameObject nowmass = PlayerObj.GetComponent<AtachMaster>().GetAttachMassObj();
       int massplayernum = nowmass.GetComponent<NumberMass>().GetPlayerNumber();
       int myplayernum = Parent.GetComponent<CharacterStatus>().GetPlayerNumber();
        if(myplayernum != massplayernum)//発動場所が今自分の場所なのか？
        {
            return;
        }
        bool IsGetSkill = Parent.GetComponent<CharacterStatus>().GetIsSkill();
        if (!IsGetSkill)//スキルを発動状態にしているか
        {
            return;
        }
        MouseState.SkillActivate retactive = PlayerObj.GetComponent<MouseState>().GetSkillActive();
        switch (retactive)
        {
            case MouseState.SkillActivate.Yes:
                Debug.Log("タイムゲイザーの効果発動");
                Parent.GetComponent<CharacterStatus>().SetIsActiveSkillParticle(false);
                Parent.GetComponent<CharacterStatus>().SetIsSkill(false);
                Parent.GetComponent<CharacterStatus>().SetReCovery(RecoveryNum);                
                PlayerObj.GetComponent<Player>().SetActiveYesNoObjFalse();
                PlayerObj.GetComponent<MouseState>().SetSkillActive(MouseState.SkillActivate.None);
                break;

            case MouseState.SkillActivate.None:
                PlayerObj.GetComponent<MouseState>().SetSkillActive(MouseState.SkillActivate.Choosing);
                PlayerObj.GetComponent<AtachMaster>().SetSkillInvoker(Parent);
                PlayerObj.GetComponent<Player>().SetActiveYesNoObjTrue();
                break;
        }
    }
}
