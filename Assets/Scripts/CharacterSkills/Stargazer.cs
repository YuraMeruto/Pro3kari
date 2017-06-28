using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stargazer : SkillBase {


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
        bool result = Parent.GetComponent<CharacterStatus>().GetIsSkill();
        if (result)
        {
            StargazerSkill();
        }

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
    }

    public override void DestroyChar()
    {

    }

    public override void SkillTargetActive()
    {
        SkillActive();
    }
    public void StargazerSkill()
    {
        Debug.Log("ターゲットを決めています");
        PlayerObj.GetComponent<Player>().SetState(Player.PlayerStatus.SkillTargetChoosing);
        PlayerObj.GetComponent<AtachMaster>().SetSkillInvoker(Parent);
    }

    public void SkillActive()
    {
        Debug.Log("フルカウンター！");
        int point = Parent.GetComponent<CharacterStatus>().GetDamagePoint();
        GameObject target = PlayerObj.GetComponent<AtachMaster>().GetSkillTarget();
        target.GetComponent<CharacterStatus>().SetDamage(point);
        PlayerObj.GetComponent<Player>().SetState(Player.PlayerStatus.None);
    }
}
