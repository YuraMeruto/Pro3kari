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

    public override bool Is_AtTheStart()
    {
        return false;
    }
    public override bool Is_AtTheEnd()
    {
        return true;
    }
    public override bool Is_BattleStart()
    {
        return false;
    }

    public override bool Is_BattleEnd()
    {
        return true;
    }
    public override bool Is_MoveStart()
    {
        return false;
    }

    public override bool Is_MoveEnd()
    {
        return true;
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
        MouseState.SkillActivate retactive = PlayerObj.GetComponent<MouseState>().GetSkillActive();
        switch(retactive)
        {
            case MouseState.SkillActivate.Yes:
                Debug.Log("ソラリオスキル発動");
                Debug.Log(Parent);
                if (SkillCount <= 0)
                {
                    return;
                }

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
