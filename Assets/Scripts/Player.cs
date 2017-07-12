using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private LayerMask MassLayer;
    [SerializeField]
    private LayerMask DeckLayer;
    [SerializeField]
    private LayerMask DeckCardLayer;
    [SerializeField]
    private GameObject AtachDeckObj;
    private GameObject AtachMassObject;
    private GameObject AtachDeckCardObj = null;
    [SerializeField]
    private GameObject CopyAtachMassObject;
    [SerializeField]
    private GameObject AtachCharObject = null;
    private int AtachMassNumber;
    private int CopyAttachMassNumber;
    private GameObject MasterObject;
    public enum PlayerStatus { None, Choose, ShowCard, ChoosingCard, SummonCard, Skillactivate, SkillChoosing, SkillTargetChoosing, SkillTargetAlready };
    [SerializeField]
    private PlayerStatus status = PlayerStatus.None;
    [SerializeField]
    private int PlayerNumber;
    private int AtachCharNumber;
    private GameObject IsEnemyObj;
    private int IsEnemyMassNumber;
    private Vector3 MovePos;
    private enum BattleResult { Win, Lose, Draw };
    public GameObject kari;
    private GameObject ChoosingCardSummonObj;
    private GameObject InstanceSumonObj;
    private int SumonsCost;
    private int RaceNum;
    private GameObject MassObject;
    private GameObject CopyMassObject;
    [SerializeField]
    private LayerMask YesLayer;
    [SerializeField]
    private LayerMask NoLayer;
    [SerializeField]
    private LayerMask TurnObjLayer;
    private PhaseMaster.Phase NowPhase;
    [SerializeField]
    private LayerMask NextPhaseLayer;
    [SerializeField]
    private GameObject YesObj;
    [SerializeField]
    private GameObject NoObj;

    private bool Is_skill = false;
    private bool IsSkillSwitch = false;
    private float SkillSwitchTime = 0;
    // Use this for initialization
    void Start()
    {
        MasterObject = GameObject.Find("Master");
        NowPhase = MasterObject.GetComponent<PhaseMaster>().GetNowFase();
    }

    // Update is called once per frame
    void Update()
    {
        MauseMove();
        IsSkillSwitchAdd();
    }

    void MauseMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, NextPhaseLayer))
            {
                Debug.Log("次のフェイズへ移行します");
                MasterObject.GetComponent<PhaseMaster>().NextFase();
                NowPhase = MasterObject.GetComponent<PhaseMaster>().GetNowFase();
            }

            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, YesLayer))
            {
                Debug.Log("これはYesのオブジェクトです");
                GetComponent<MouseState>().SetSkillActive(MouseState.SkillActivate.Yes);
                SetActiveYesNoObjFalse();
                GetComponent<SkillState>().SkillActive();
            }

            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, NoLayer))
            {
                Debug.Log("これはNoのオブジェクトです");
                GetComponent<MouseState>().SetSkillActive(MouseState.SkillActivate.No);
                SetActiveYesNoObjFalse();
            }

            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, TurnObjLayer))
            {
                Debug.Log("これはターン修了のオブジェクトです。");
                MasterObject.GetComponent<PhaseMaster>().SetFase(PhaseMaster.Phase.TurnEnd);
            }

            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, MassLayer))
            {
                Debug.Log("オブジェクトです");
                switch (status)
                {
                    case PlayerStatus.SummonCard:
                        GetComponent<MouseState>().SwitchPlayerNone();
                        break;
                    case PlayerStatus.None:
                        //                        AtachMassObject = hit.collider.gameObject;
                        GetComponent<AtachMaster>().SetAttachMassObject(hit.collider.gameObject);
                        if (NowPhase != PhaseMaster.Phase.Main1 || NowPhase != PhaseMaster.Phase.Main2)
                            GetComponent<MouseState>().SwitchPlayerNone();
                        break;



                    case PlayerStatus.Choose:
                        //                        CopyAtachMassObject = hit.collider.gameObject;
                        GetComponent<AtachMaster>().SetCopyAttachMassObj(hit.collider.gameObject);
                        GetComponent<MouseState>().SwitchPlayerChoose();
                        break;

                    case PlayerStatus.ChoosingCard:
                        //  AtachMassObject = hit.collider.gameObject;
                        GetComponent<AtachMaster>().SetAttachMassObject(hit.collider.gameObject);
                        if (PhaseMaster.Phase.Move == NowPhase)
                        {
                            break;
                        }
                        //AtachMassNumber = AtachMassObject.GetComponent<NumberMass>().GetNumber();
                        GetComponent<MouseState>().SummonCard();
                        break;

                    case PlayerStatus.SkillTargetChoosing:
                        GetComponent<AtachMaster>().SetAttachMassObject(hit.collider.gameObject);
                        GetComponent<AtachMaster>().SetCopyAttachMassObj(hit.collider.gameObject);
                        GetComponent<MouseState>().SkillTarget();
                        status = PlayerStatus.SkillTargetAlready;
                        break;
                }
            }

            RaycastHit hit2;
            if (Physics.Raycast(ray, out hit2, Mathf.Infinity, DeckLayer))
            {
                Debug.Log("これはデッキです");
                switch (status)
                {
                    case PlayerStatus.ChoosingCard:
                    case PlayerStatus.ShowCard:
                    case PlayerStatus.None:
                        GetComponent<AtachMaster>().SetAttachDeckObj(hit2.collider.gameObject);
                        GetComponent<MouseState>().SwitchDeck();

                        break;
                }
            }

            //カードをアタッチしたら
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, Mathf.Infinity, DeckCardLayer);
            if (hit2d.collider != null)
            {

                switch (status)
                {
                    case PlayerStatus.ShowCard:
                        //  AtachDeckCardObj = hit2d.collider.gameObject;
                        GetComponent<AtachMaster>().SetAttachDeckCardObj(hit2d.collider.gameObject);
                        GetComponent<MouseState>().ChoosingCard();
                        status = PlayerStatus.ChoosingCard;
                        break;
                }
            }
            IsSkillSwitch = true;
        }
        else if (Input.GetMouseButtonUp(0))//マウスのクリックが離されたとき
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, MassLayer))
            {
                switch (status)
                {
                    case PlayerStatus.Choose:
                        //                        CopyAtachMassObject = hit.collider.gameObject;
                        GetComponent<AtachMaster>().SetCopyAttachMassObj(hit.collider.gameObject);
                        if (NowPhase != PhaseMaster.Phase.Move)
                        {
                            AllIsMoveAreaDestroy();
                            status = PlayerStatus.None;
                            return;
                        }
                        GetComponent<MouseState>().SwitchPlayerChoose();
                        break;

                    case PlayerStatus.SkillTargetAlready:
                        GameObject skillinvorker = GetComponent<AtachMaster>().GetSkillInvoker();
                        skillinvorker.GetComponent<CharacterStatus>().skill.SkillTargetActive();
                        break;

                }
            }

        }
    }

    public GameObject GetAtachMassNum()
    {
        return AtachMassObject;
    }

    public void SetNowPhase(PhaseMaster.Phase phase)
    {
        NowPhase = phase;
    }

    public void SetState(PlayerStatus set)
    {
        status = set;
    }


    public void SetActiveYesNoObjTrue()
    {
        YesObj.SetActive(true);
        NoObj.SetActive(true);
        status = PlayerStatus.None;
    }

    public void SetActiveYesNoObjFalse()
    {
        YesObj.SetActive(false);
        NoObj.SetActive(false);
    }

    void IsSkillSwitchAdd()
    {
        if (IsSkillSwitch)
        {
            SkillSwitchTime += Time.deltaTime;
        }
    }

    /// <summary>
    /// スキルを使うかの選択
    /// </summary>
    public void IsSkillZyazi()
    {
        GameObject atachobj = GetComponent<AtachMaster>().GetAttachCharObj();
        //       GameObject particleobj = atachobj.transform.FindChild("SkillParticl").gameObject;
        if (SkillSwitchTime < 1)
        {
            atachobj.GetComponent<CharacterStatus>().SetIsSkill(true);
            Debug.Log("スキルオン!!!");
            //particleobj.SetActive(true);
            atachobj.GetComponent<CharacterStatus>().SetIsActiveSkillParticle(true);
            atachobj.GetComponent<CharacterStatus>().skill.SkillIsActive();
            SkillSwitchTime = 0;
            IsSkillSwitch = true;
            return;
        }

        else
        {
            Debug.Log("スキルオフ!!!");
            atachobj.GetComponent<CharacterStatus>().SetIsSkill(false);
            atachobj.GetComponent<CharacterStatus>().SetIsActiveSkillParticle(false);
            //            particleobj.SetActive(false);
            IsSkillSwitch = false;
            SkillSwitchTime = 0;
            return;
        }
    }

    void AllIsMoveAreaDestroy()//移動範囲の表示のオブジェクトをすべて消す
    {
        var clones = GameObject.FindGameObjectsWithTag("IsMovetag");
        foreach (var clone in clones)
        {
            Destroy(clone);
        }
    }

}
