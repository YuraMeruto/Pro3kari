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
    [SerializeField]
    private enum PlayerStatus { None, Choose,ShowCard,ChoosingCard,SummonCard};
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
    // Use this for initialization
    void Start()
    {
        MasterObject = GameObject.Find("Master");
    
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.A))
        {

            var clones = GameObject.FindGameObjectsWithTag("Kari");
            foreach (var clone in clones)
            {
                Destroy(clone);
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            MasterObject.GetComponent<BoardMaster>().AllInstance(kari);
        }
        */
        if (Input.GetKeyDown(KeyCode.S))
        {
            //            MasterObject.GetComponent<BoardMaster>().DebugInst();
            // MasterObject.GetComponent<BoardMaster>().InstanceSP();
            //    MasterObject.GetComponent<BoardMaster>().DebugMassArea();
            //Debug.Log(status);
            MasterObject.GetComponent<BoardMaster>().SetTurnPlayer();
        }

        MauseMove();
    }

    void MauseRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, MassLayer))
        {
            MassObject = hit.collider.gameObject;
            if (CopyMassObject == null)
            {
                CopyMassObject = MassObject;
            }
            else
            {
                CopyMassObject = MassObject;
            }
            hit.collider.gameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0);
                  

        }
    }
    void MauseMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, MassLayer))
            {
                Debug.Log("オブジェクトです");
                switch (status)
                {
                    case PlayerStatus.SummonCard:
                    case PlayerStatus.None:
                        AtachMassObject = hit.collider.gameObject;
                        SwitchPlayerNone();

                        break;

                    case PlayerStatus.Choose:
                        CopyAtachMassObject = hit.collider.gameObject;
                        SwitchPlayerChoose();
                        break;

                    case PlayerStatus.ChoosingCard:
                        AtachMassObject = hit.collider.gameObject;
                        AtachMassNumber = AtachMassObject.GetComponent<NumberMass>().GetNumber();
                        SummonCard();
                        break;
                }
            }

            RaycastHit hit2;
            if (Physics.Raycast(ray, out hit2, Mathf.Infinity, DeckLayer))
            {
                Debug.Log("これはデッキです");
                switch(status)
                {
                    case PlayerStatus.ChoosingCard:
                    case PlayerStatus.ShowCard:
                    case PlayerStatus.None:
                        AtachDeckObj = hit2.collider.gameObject;
                        SwitchDeck();

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
                        AtachDeckCardObj = hit2d.collider.gameObject;
                        ChoosingCard();
                        status = PlayerStatus.ChoosingCard;
                        break;
                }          
            }
        }
    }

    void SwitchPlayerNone()//何も選択していない状態でキャラクターを選択した時
    {
        CopyAtachMassObject = AtachMassObject;
        AtachMassNumber = AtachMassObject.GetComponent<NumberMass>().GetNumber();
        AtachCharObject = MasterObject.GetComponent<BoardMaster>().GetCharObject(AtachMassNumber);
        bool IsEnemy = false;
        if (AtachCharObject != null)
        {
            AtachCharNumber = AtachCharObject.GetComponent<CharacterStatus>().GetPlayerNumber();
        }
        int MasterTurnNumber = MasterObject.GetComponent<BoardMaster>().GetTurnPlayer();
        if (AtachCharNumber != MasterTurnNumber)//もし選択したマスが自分のキャラクターでなければスルーさせる
        {
            Debug.Log("キャラクターではないのでスルーします");
            IsEnemy = true;
        }
        if (!IsEnemy)
        {
            if (AtachCharObject != null)
            {
                Debug.Log(AtachCharObject.name);
                int retnum = AtachCharObject.GetComponent<CharacterStatus>().GetSummoningSickness();
                if (retnum == 0)
                {
                    AtachCharObject.GetComponent<MoveData>().IsPossibleMove(AtachMassNumber);
                    status = PlayerStatus.Choose;
                }
            }
        }
    }

    void SwitchPlayerChoose()//キャラクターを選択していたら
    {
        bool ret;
        CopyAttachMassNumber = CopyAtachMassObject.GetComponent<NumberMass>().GetNumber();
        GameObject CopyAtachCharObject = MasterObject.GetComponent<BoardMaster>().GetCharObject(CopyAttachMassNumber);
        bool IsSamePlayerCharObj = true;
        if (CopyAtachCharObject != null)
        {
            int CopyAtachCharPlayerNumber = CopyAtachCharObject.GetComponent<CharacterStatus>().GetPlayerNumber();
            int AtachcharCHarPlayerNumber = AtachCharObject.GetComponent<CharacterStatus>().GetPlayerNumber();
            if (AtachcharCHarPlayerNumber == CopyAtachCharPlayerNumber)//ほかの自分のキャラクターだったらそのキャラクターにアタッチさせる
            {
                IsSamePlayerCharObj = false;
                status = PlayerStatus.None;
                AllIsMoveAreaDestroy();
                AllSummonsCardDestroy();
                if (AtachDeckObj != null)
                {
                    AtachDeckObj.GetComponent<SummonsDeck>().SetIsCardShow();
                }
            }
        }
        if (IsSamePlayerCharObj)
        {
            IsEnemyObj = null;
            ret = MasterObject.GetComponent<BoardMaster>().GetIsMove(CopyAttachMassNumber);

            IsEnemyObj = MasterObject.GetComponent<BoardMaster>().GetCharObject(CopyAttachMassNumber);//移動先が敵の駒があるのか確認
            if (IsEnemyObj != null)
            {
                int EnemyPlayerNumber = IsEnemyObj.GetComponent<CharacterStatus>().GetPlayerNumber();
                int MyPlayerNumber = AtachCharObject.GetComponent<CharacterStatus>().GetPlayerNumber();
                if (MyPlayerNumber == EnemyPlayerNumber)//同じプレイヤーのキャラクターなら消さない
                {
                    IsEnemyObj = null;
                }
                var BattleRet = ResultBattleScene();
                switch (BattleRet)
                {

                    case BattleResult.Win:
                        Destroy(IsEnemyObj);
                        AllIsMoveAreaDestroy();
                        AllSummonsCardDestroy();
                        if (AtachDeckObj != null)
                        {
                            AtachDeckObj.GetComponent<SummonsDeck>().SetIsCardShow();
                        }
                        MovePos.z = 1.0f;
                        AtachCharObject.transform.position = MovePos;
                        MasterObject.GetComponent<BoardMaster>().SetIsCharObj(CopyAttachMassNumber, AtachMassNumber, AtachCharObject);
                        MasterObject.GetComponent<BoardMaster>().SetAllFalseIsMove();
                        MasterObject.GetComponent<BoardMaster>().SetTurnPlayer();
                        AtachCharObject = null;
                        ret = false;
                        status = PlayerStatus.None;
                        break;

                    case BattleResult.Lose:
                        AllIsMoveAreaDestroy();
                        AllSummonsCardDestroy();
                        if (AtachDeckObj != null)
                        {
                            AtachDeckObj.GetComponent<SummonsDeck>().SetIsCardShow();
                        }
                        MasterObject.GetComponent<BoardMaster>().SetLoseObj(AtachCharObject, AtachMassNumber);
                        MasterObject.GetComponent<BoardMaster>().SetAllFalseIsMove();
                        MasterObject.GetComponent<BoardMaster>().SetTurnPlayer();
                        AtachCharObject = null;
                        ret = false;
                        status = PlayerStatus.None;
                        break;

                    case BattleResult.Draw:
                        MovePos = MasterObject.GetComponent<BoardMaster>().EnemySurroundings(CopyAttachMassNumber, AtachMassNumber, AtachCharObject);
                        AllIsMoveAreaDestroy();
                        AllSummonsCardDestroy();
                        if (AtachDeckObj != null)
                        {
                            AtachDeckObj.GetComponent<SummonsDeck>().SetIsCardShow();
                        }
                        MovePos.z = 1.0f;
                        AtachCharObject.transform.position = MovePos;
                        //MasterObject.GetComponent<BoardMaster>().SetIsCharObj(CopyAttachMassNumber, AtachMassNumber, AtachCharObject);
                        MasterObject.GetComponent<BoardMaster>().SetAllFalseIsMove();
                        MasterObject.GetComponent<BoardMaster>().SetTurnPlayer();
                        AtachCharObject = null;
                        status = PlayerStatus.None;
                        ret = false;
                        break;

                }

            }
            if (ret)//通常の移動の時
            {
                Debug.Log("通常移動");
                AllIsMoveAreaDestroy();
                AllSummonsCardDestroy();
                if (AtachDeckObj != null)
                {
                    AtachDeckObj.GetComponent<SummonsDeck>().SetIsCardShow();
                }
                MovePos = MasterObject.GetComponent<BoardMaster>().GetPos(CopyAttachMassNumber);
                MovePos.z = 1.0f;
                AtachCharObject.transform.position = MovePos;
                MasterObject.GetComponent<BoardMaster>().SetIsCharObj(CopyAttachMassNumber, AtachMassNumber, AtachCharObject);
                MasterObject.GetComponent<BoardMaster>().SetAllFalseIsMove();
                MasterObject.GetComponent<BoardMaster>().SetTurnPlayer();
                //                AtachCharObject.GetComponent<CharacterStatus>().SkillStart();
                AllAtachNull();
                status = PlayerStatus.None;
            }
        }
        AllAtachNull();
    }
    void AllIsMoveAreaDestroy()//移動範囲の表示のオブジェクトをすべて消す
    {
        var clones = GameObject.FindGameObjectsWithTag("IsMovetag");
        foreach (var clone in clones)
        {
            Destroy(clone);
        }
    }
    void AllSummonsCardDestroy()//移動範囲の表示のオブジェクトをすべて消す
    {
        var clones = GameObject.FindGameObjectsWithTag("SummonsCard");
        foreach (var clone in clones)
        {
            Destroy(clone);
        }
    }

    void AllAtachNull()
    {
        //AtachCharObject = null;
        //CopyAtachMassObject = null;
        //AtachDeckObj = null;
    }
    /*
    void BattleScene()
    {
        int MyCharAttack = AtachCharObject.GetComponent<CharacterStatus>().GetAttack();
        int EnemyHp = IsEnemyObj.GetComponent<CharacterStatus>().GetHp();
        int ResultHp = EnemyHp - MyCharAttack;
        IsEnemyObj.GetComponent<CharacterStatus>().SetHp(ResultHp);
        if (ResultHp <= 0)
        {
            Destroy(IsEnemyObj);
        }
        else if(ResultHp>0)
        {

        }
        
    }
    */
    BattleResult ResultBattleScene()
    {
        int MyCharHp = AtachCharObject.GetComponent<CharacterStatus>().GetHp();
        int MyCharAttack = AtachCharObject.GetComponent<CharacterStatus>().GetAttack();
        int EnemyHp = IsEnemyObj.GetComponent<CharacterStatus>().GetHp();
        int EnemyAttack = IsEnemyObj.GetComponent<CharacterStatus>().GetAttack();

        int ResultHp = EnemyHp - MyCharAttack;
        IsEnemyObj.GetComponent<CharacterStatus>().SetHp(ResultHp);
        if (ResultHp <= 0)//プレイヤーからの攻撃
        {

            //Destroy(IsEnemyObj);
            Debug.Log("勝利しました");
            return BattleResult.Win;
        }

        ResultHp = MyCharHp - EnemyAttack;
        if (ResultHp <= 0)//プレイヤーの体力がゼロになったら
        {
            AtachCharObject.GetComponent<CharacterStatus>().SetHp(ResultHp);
            Destroy(AtachCharObject);
            Debug.Log("勝利しました");
            Debug.Log(AtachCharObject);
            return BattleResult.Lose;
        }

        AtachCharObject.GetComponent<CharacterStatus>().SetHp(ResultHp);
        Debug.Log("引き分け");
        //MovePos = MasterObject.GetComponent<BoardMaster>().EnemySurroundings(CopyAttachMassNumber);
        return BattleResult.Draw;
    }


    void SwitchDeck()
    {
        bool IsOpen = AtachDeckObj.GetComponent<SummonsDeck>().GetISCardShow();
        if (IsOpen)
        {
            int MasterTurnNumber = MasterObject.GetComponent<BoardMaster>().GetTurnPlayer();
            int DeckNumber = AtachDeckObj.GetComponent<SummonsDeck>().GetPlayerNumber();
            if (MasterTurnNumber == DeckNumber)
            {
                AtachDeckObj.GetComponent<SummonsDeck>().ShowCard();
                status = PlayerStatus.ShowCard;
            }
        }
        else 
        {
            status = PlayerStatus.None;
            AtachDeckObj.GetComponent<SummonsDeck>().SetIsCardShow();
            AllIsMoveAreaDestroy();
            AllSummonsCardDestroy();
            AllKariDestroy();
        }
    }//デッキを選択したら

    void ChoosingCard()//カードを選択したら
    {

        int DictionaryNum;
        Debug.Log("これはカードです");
        int MasterTurnNumber = MasterObject.GetComponent<BoardMaster>().GetTurnPlayer();
        RaceNum = AtachDeckCardObj.GetComponent<IllustrationCard>().GetRaceNumber();
        SumonsCost = AtachDeckCardObj.GetComponent<IllustrationCard>().GetSumonCos();
        DictionaryNum = AtachDeckCardObj.GetComponent<IllustrationCard>().GetDictionaryNumber();
        MasterObject.GetComponent<BoardMaster>().SummonsFiledPos(RaceNum);
        ChoosingCardSummonObj = MasterObject.GetComponent<CharacterMaster>().GetSummonsCharacter(DictionaryNum);
    }

    void SummonCard()
    {
        //召喚できるマスならば
        bool retIsmove = MasterObject.GetComponent<BoardMaster>().GetIsMove(AtachMassNumber);
        bool retIsSPCost = MasterObject.GetComponent<BoardMaster>().UseSP(SumonsCost);

        if (retIsmove && retIsSPCost)
        {
            Debug.Log("召喚!");
            Vector3 SumonsPos = AtachMassObject.transform.position;
            SumonsPos.z += 1;
            InstanceSumonObj = Instantiate(ChoosingCardSummonObj, SumonsPos, ChoosingCardSummonObj.transform.rotation);
            int MassNum = AtachMassObject.GetComponent<NumberMass>().GetNumber();
            MasterObject.GetComponent<BoardMaster>().SetIsCharObj(MassNum, InstanceSumonObj);
            MasterObject.GetComponent<BoardMaster>().SetStatusIsMoveArea(MassNum);
            AtachDeckObj.GetComponent<SummonsDeck>().SetIsCardShow();
            if(RaceNum !=1)//ポーン以外の召喚酔い
            {
                InstanceSumonObj.GetComponent<CharacterStatus>().SetSummoningSickness(1);
                MasterObject.GetComponent<BoardMaster>().SetSummoningSicknessCharacterList(InstanceSumonObj);
            }
            SetIniSummonCard();
            AllIsMoveAreaDestroy();
            AllSummonsCardDestroy();
            AllKariDestroy();            
            status = PlayerStatus.None;
        }
    }

    void AllKariDestroy()
    {
        var clones = GameObject.FindGameObjectsWithTag("Kari");
        foreach (var clone in clones)
        {
            Destroy(clone);
        }
    }

    void SetIniSummonCard()
    {
        int pnum;
        pnum = MasterObject.GetComponent<BoardMaster>().GetTurnPlayer();
        InstanceSumonObj.GetComponent<CharacterStatus>().SetPlayerNumber(pnum);
        InstanceSumonObj.GetComponent<MoveData>().ReadSetObj(InstanceSumonObj);
        InstanceSumonObj.GetComponent<ReadCsv>().SetTargetObj(InstanceSumonObj);
        InstanceSumonObj.GetComponent<MoveData>().IniSet();
    }
}
