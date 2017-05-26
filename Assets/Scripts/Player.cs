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
    private enum PlayerStatus { None, Choose, OpenCard, ChoosingCard, SummonCard };
    private PlayerStatus status = PlayerStatus.None;
    [SerializeField]
    private int PlayerNumber;
    private int AtachCharNumber;
    private GameObject IsEnemyObj;
    private int IsEnemyMassNumber;
    private Vector3 MovePos;
    private enum BattleResult { Win, Lose, Draw };
    public GameObject kari;
    private GameObject SummonObj;
    // Use this for initialization
    void Start()
    {
        MasterObject = GameObject.Find("Master");
        for (int length = 0; length < 10; length++)
        {
            for (int side = 0; side < 10; side++)
            {
            }
        }
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

            MasterObject.GetComponent<BoardMaster>().DebugInst();
            Debug.Log("aa");
        }

        MauseMove();
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
                    case PlayerStatus.None:
                        AtachMassObject = hit.collider.gameObject;

                        SwitchPlayerNone();
                        break;

                    case PlayerStatus.Choose:
                        CopyAtachMassObject = hit.collider.gameObject;
                        SwitchPlayerChoose();
                        break;

                    case PlayerStatus.SummonCard:
                        Debug.Log("aaaa");
                        AtachMassObject = hit.collider.gameObject;
                        SummonCardOnField();
                        status = PlayerStatus.None;
                        break;
                }
            }

            RaycastHit hit2;
            if (Physics.Raycast(ray, out hit2, Mathf.Infinity, DeckLayer))
            {
                Debug.Log("これはデッキです");
                switch (status)
                {

                    case PlayerStatus.None:
                        AtachDeckObj = hit2.collider.gameObject;
                        int MasterTurnNumber = MasterObject.GetComponent<BoardMaster>().GetTurnPlayer();
                        int DeckNumber = AtachDeckObj.GetComponent<SummonsDeck>().GetPlayerNumber();
                        if (MasterTurnNumber == DeckNumber)
                        {
                            AtachDeckObj.GetComponent<SummonsDeck>().ShowCard();
                        }
                        status = PlayerStatus.OpenCard;
                        break;
                }
            }

            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, Mathf.Infinity, DeckCardLayer);
            if (hit2d.collider != null)
            {
                switch (status)
                {
                    case PlayerStatus.OpenCard:
                        int RaceNum;
                        Debug.Log("これはカードです");
                        AtachDeckCardObj = hit2d.collider.gameObject;
                        RaceNum = AtachDeckCardObj.GetComponent<IllustrationCard>().GetRaceNumber();
                        MasterObject.GetComponent<BoardMaster>().SummonsFiledPos(RaceNum);
                        //           SummonCardOnField();
                        status = PlayerStatus.SummonCard;
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

                AtachCharObject.GetComponent<MoveData>().IsPossibleMove(AtachMassNumber);
                status = PlayerStatus.Choose;
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
        AtachCharObject = null;
        CopyAtachMassObject = null;
        AtachDeckObj = null;
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
    void SummonCardOnField()
    {
        int GetSumonNumber;
        GetSumonNumber = AtachDeckCardObj.GetComponent<IllustrationCard>().GetSumonNumber();
        Debug.Log(GetSumonNumber);
        SummonObj = MasterObject.GetComponent<CharacterMaster>().GetSumonnsCharacter(GetSumonNumber);
        Debug.Log(SummonObj);
        Vector3 pos = AtachMassObject.transform.position;
        pos.z += 1;
        Instantiate(SummonObj, pos, SummonObj.transform.rotation);
        AllIsMoveAreaDestroy();
     //   AllSummonsCardDestroy();
    }
    void AllKariDestroy()
    {
        var clones = GameObject.FindGameObjectsWithTag("Kari");
        foreach (var clone in clones)
        {
            Destroy(clone);
        }
    }
}
