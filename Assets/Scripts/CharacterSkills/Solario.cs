using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solario : SkillBase
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

    }
    public override void AtTheEnd()
    {
    }
    public override void BattleStart()
    {

    }
    public override void BattleEnd()
    {
        RecoveryMoveSkill();
    }

    public override void MoveStart()
    {
       // RecoveryMoveSkill();

    }
    public override void MoveEnd()
    {
        MouseState.SkillActivateGo a = PlayerObj.GetComponent<MouseState>().GetSkillActiveGo();
        if (a != MouseState.SkillActivateGo.MoveEnd)
        {
            PlayerObj.GetComponent<MouseState>().SetSkillActiveGo(MouseState.SkillActivateGo.MoveEnd);
            PlayerObj.GetComponent<Player>().SetActiveYesNoObjTrue();
            return;
        }
       RecoveryMoveSkill();
    }

    public void RecoveryMoveSkill()
    {
        bool IsGetSkill = Parent.GetComponent<CharacterStatus>().GetIsSkill();
        if (!IsGetSkill)
        {
            return;
        }
        MouseState.SkillActivate retactive = PlayerObj.GetComponent<MouseState>().GetSkillActive();
        if (SkillCount <= 0)
            return;
        
        switch (retactive)
        {
            case MouseState.SkillActivate.Yes:
                Debug.Log("ソラリオスキル発動");
                Parent.GetComponent<CharacterStatus>().SetIsActiveSkillParticle(false);
                Master.GetComponent<BoardList>().RemoveMoveList(Parent);
                SkillCount--;
                Parent.GetComponent<CharacterStatus>().SetIsSkill(false);
                PlayerObj.GetComponent<Player>().SetActiveYesNoObjFalse();
                PlayerObj.GetComponent<MouseState>().SetSkillActive(MouseState.SkillActivate.None);
                PlayerObj.GetComponent<MouseState>().SetSkillActiveGo(MouseState.SkillActivateGo.None);
                break;

            case MouseState.SkillActivate.None:
                PlayerObj.GetComponent<MouseState>().SetSkillActive(MouseState.SkillActivate.Choosing);
                PlayerObj.GetComponent<Player>().SetActiveYesNoObjTrue();
                break;
        }
    }
}
