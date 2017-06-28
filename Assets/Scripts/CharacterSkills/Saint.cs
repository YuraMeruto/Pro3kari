using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saint : SkillBase {
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
    [SerializeField]
    private int SkillDamage;
    void Start()
    {
        Parent = gameObject.transform.parent.gameObject;
        Master = GameObject.Find("Master");
        PlayerObj = GameObject.Find("Main Camera");
        Master.GetComponent<BoardSkillList>().SetEnemyMoveEndSkillList(Parent);
    }
    private int NowMyPosLength;
    private int NowMyPosSide;
    private int MaxLength;
    private int MaxSide;


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
    }

    public override void EnemyMoveEnd()
    {
        AreaDamageSkill();
    }
    public override void DestroyChar()
    {

    }
    public override void SkillTargetActive()
    {

    }
    public void AreaDamageSkill()
    {
        Debug.Log("聖女のスキル発動");
        int EnemyNowPosX = Master.GetComponent<BoardSkillList>().GetPosLength();
        int EnemyNowPosY = Master.GetComponent<BoardSkillList>().GetPosSide();
        int result = Master.GetComponent<BoardMaster>().GetPlayerArea(EnemyNowPosX, EnemyNowPosY);
        int GetPlayerNumber = Parent.GetComponent<CharacterStatus>().GetPlayerNumber();
        if (GetPlayerNumber == result)
        {
            Debug.Log(Parent);
            Master.GetComponent<BoardMaster>().SaintSkill(EnemyNowPosX, EnemyNowPosY, SkillDamage);
        }
    }
}
