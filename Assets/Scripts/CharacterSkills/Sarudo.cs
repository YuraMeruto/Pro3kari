using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sarudo :SkillBase {


    [SerializeField]
    private GameObject SkillInstanceObj;
    [SerializeField]
    private GameObject Master;
    private GameObject CameraObj;
    private GameObject Parent;
    [SerializeField]
    private GameObject PlayerObj;
    private enum SkillStage { None,Target,Pos};
    private SkillStage stage=SkillStage.None;
    [SerializeField]
    private int ApSkillCost = 3;
    [SerializeField]
    private GameObject TargetEnemy;
    void Start()
    {
        Parent = gameObject.transform.parent.gameObject;
        Master = GameObject.Find("Master");
        PlayerObj = GameObject.Find("Main Camera");
      
    }

    public override void SkillIsActive()
    {
        SarudoSkillActive();
    }

    public override void SkillTargetActive()
    {
        switch(stage)
        {
            case SkillStage.Target:
                SetTarget();
                SkillActive();
                break;
            case SkillStage.Pos:
                SetPos();
                break;
        }
    }

    
    public void SarudoSkillActive()
    {
        stage = SkillStage.Target;
        MouseState.SkillActivate retactive = PlayerObj.GetComponent<MouseState>().GetSkillActive();
        switch (retactive)
        {
            //スキルを発動することの再確認させる
            case MouseState.SkillActivate.Yes:
                int playernum = Parent.GetComponent<CharacterStatus>().GetPlayerNumber();
                
                //使用するAPの確認
                bool ret = Master.GetComponent<BoardMaster>().UseAp(playernum, ApSkillCost);
                if (ret) { 
                    stage = SkillStage.Target;
                    SetTarget();
                }
                else {
                    Debug.Log("APコストが足りません");
                    PlayerObj.GetComponent<MouseState>().SetSkillActive(MouseState.SkillActivate.None);
                    Parent.GetComponent<CharacterStatus>().SetIsSkill(false);
                }
                break;

            case MouseState.SkillActivate.None:
                PlayerObj.GetComponent<MouseState>().SetSkillActive(MouseState.SkillActivate.Choosing);
                PlayerObj.GetComponent<AtachMaster>().SetSkillInvoker(Parent);
                PlayerObj.GetComponent<Player>().SetActiveYesNoObjTrue();
                break;
        }

    }

    public void SkillActive()
    {
        GameObject target = PlayerObj.GetComponent<AtachMaster>().GetSkillTarget();
        int spcost = target.GetComponent<CharacterStatus>().GetSumonnCost();
        bool ret = Master.GetComponent<BoardMaster>().Is_UseSP(spcost);
        Debug.Log(ret);
        if (ret)
        {
            int enemynum = target.GetComponent<CharacterStatus>().GetPlayerNumber();
            int myplayernum = Parent.GetComponent<CharacterStatus>().GetPlayerNumber();
            if (enemynum == myplayernum)
            {
                Debug.Log("同じplayerですターゲットを変えてください");
                SetTarget();
            }
            else if (enemynum != myplayernum)
            {
                Debug.Log("次に自分の陣地を選んでください");
                stage = SkillStage.Pos;
                TargetEnemy = target;
                SetTarget();
            }
        }
        else
        {
            Debug.Log("SPCostが足りません");
        }


    }
    void SetTarget()
    {
        Debug.Log("ターゲットを決めています");
        if (stage != SkillStage.None)
        {
            PlayerObj.GetComponent<Player>().SetState(Player.PlayerStatus.SkillTargetChoosing);
            PlayerObj.GetComponent<AtachMaster>().SetSkillInvoker(Parent);
        }
    }
    void SetPos()
    {
        GameObject mass = PlayerObj.GetComponent<AtachMaster>().GetAttachMassObj();
        int playernum = mass.GetComponent<NumberMass>().GetPlayerNumber();
        int spcost = TargetEnemy.GetComponent<CharacterStatus>().GetSumonnCost();
        int myplayernum = Parent.GetComponent<CharacterStatus>().GetPlayerNumber();
        if (playernum == myplayernum)
        {
    
            Debug.Log("サルードスキル発動");
            spcost = TargetEnemy.GetComponent<CharacterStatus>().GetSumonnCost();
            Master.GetComponent<BoardMaster>().SetSp(playernum,spcost);
            Vector3 pos = mass.transform.position;
            pos.z = 1;
            TargetEnemy.transform.position = pos;
            int massnum = mass.GetComponent<NumberMass>().GetNumber();
            Vector3 posenemy = TargetEnemy.transform.position;
            Master.GetComponent<BoardMaster>().SarudoSkillPos(posenemy, TargetEnemy);
            TargetEnemy.GetComponent<CharacterStatus>().SetPlayerNumber(myplayernum);
            PlayerObj.GetComponent<Player>().SetState(Player.PlayerStatus.None);
            stage = SkillStage.None;

            Parent.GetComponent<CharacterStatus>().SetIsSkill(false);

        }
        else if(stage != SkillStage.None)
        {
            Debug.Log("自分の陣地ではありませんもういちど");
            SetTarget();

        }
    }
}
