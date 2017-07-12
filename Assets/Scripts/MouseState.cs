using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseState : MonoBehaviour
{
    private GameObject MasterObject;
    private int RaceNum;
    private int SumonsCost;
    [SerializeField]
    GameObject IsEnemyObj;
    private bool IsTargetBool = false;
    public enum SkillActivate { Choosing, Yes, No, None }
    public enum SkillActivateGo { AtTheStart, AtTheEnd, MoveStart, MoveEnd, BattleStart, BattleEnd, None, }
    public SkillActivateGo skillgo;
    [SerializeField]
    private SkillActivate skillactive = SkillActivate.None;
    [SerializeField]
    private GameObject YesObj;
    [SerializeField]
    private GameObject NoObj;
    void Start()
    {
        MasterObject = GameObject.Find("Master");
    }

    public void SwitchPlayerNone()//何も選択していない状態でキャラクターを選択した時
    {
        int AtachCharNumber = 0;
        GameObject CopyAtachMassObject = GetComponent<AtachMaster>().GetAttachMassObj();
        int AtachMassNumber = GetComponent<AtachMaster>().GetAttachMassNumber();
        GameObject AtachCharObject = MasterObject.GetComponent<BoardMaster>().GetCharObject(AtachMassNumber);
        bool Ismoveret = MasterObject.GetComponent<BoardList>().GetIsMoveList(AtachCharObject);//一度動かしたキャラクターかどうか
        if (!Ismoveret)
        {
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
                        //    status =  PlayerStatus.Choose;
                        GetComponent<Player>().SetState(Player.PlayerStatus.Choose);
                    }
                }
            }
            GetComponent<AtachMaster>().SetAttachCharObj(AtachCharObject);
        }
    }
    public void SwitchPlayerChoose()//キャラクターを選択していたら
    {

        bool ret;
        Vector3 MovePos = Vector3.zero;
        GameObject CopyAtachMassObject = GetComponent<AtachMaster>().GetCopyAttachMassObj();
        GameObject AtachCharObject = GetComponent<AtachMaster>().GetAttachCharObj();
        GameObject AtachDeckObj = GetComponent<AtachMaster>().GetAttachDeckObj();
        int CopyAttachMassNumber = CopyAtachMassObject.GetComponent<NumberMass>().GetNumber();
        GameObject CopyAtachCharObject = MasterObject.GetComponent<BoardMaster>().GetCharObject(CopyAttachMassNumber);
        bool IsSamePlayerCharObj = true;
        if (CopyAtachCharObject != null)
        {
            int CopyAtachCharPlayerNumber = CopyAtachCharObject.GetComponent<CharacterStatus>().GetPlayerNumber();
            int AtachcharCHarPlayerNumber = AtachCharObject.GetComponent<CharacterStatus>().GetPlayerNumber();
            if (AtachcharCHarPlayerNumber == CopyAtachCharPlayerNumber)//ほかの自分のキャラクターだったらそのキャラクターにアタッチさせる
            {
                IsSamePlayerCharObj = false;
                //                status = PlayerStatus.None;
                GetComponent<Player>().SetState(Player.PlayerStatus.None);
                GetComponent<Player>().IsSkillZyazi();
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

            //     GameObject IsEnemyObj = null;
            ret = MasterObject.GetComponent<BoardMaster>().GetIsMove(CopyAttachMassNumber);
            int AtachMassNumber = GetComponent<AtachMaster>().GetAttachMassNumber();
            IsEnemyObj = MasterObject.GetComponent<BoardMaster>().GetCharObject(CopyAttachMassNumber);//移動先が敵の駒があるのか確認
            if (IsEnemyObj != null)
            {
                int EnemyPlayerNumber = IsEnemyObj.GetComponent<CharacterStatus>().GetPlayerNumber();
                int MyPlayerNumber = AtachCharObject.GetComponent<CharacterStatus>().GetPlayerNumber();
                if (MyPlayerNumber == EnemyPlayerNumber)//同じプレイヤーのキャラクターなら消さない
                {
                    IsEnemyObj = null;
                }
                GetComponent<AtachMaster>().SetEnemyObj(IsEnemyObj);
              
                if (!IsTargetBool)
                {
                    var BattleRet = GetComponent<BattleScene>().Result();

                    switch (BattleRet)
                    {

                        case BattleScene.BattleResult.Win://先に攻撃したキャラクターが勝った場合
                            IsEnemyObj.GetComponent<CharacterStatus>().skill.DestroyChar();
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
                            AtachCharObject.GetComponent<CharacterStatus>().skill.BattleEnd();
                            AtachCharObject = null;
                            ret = false;

                            //                        status = PlayerStatus.None;
                            GetComponent<Player>().SetState(Player.PlayerStatus.None);
                            break;

                        case BattleScene.BattleResult.Lose://先に攻撃したキャラクターが負けた場合
                            AtachCharObject.GetComponent<CharacterStatus>().skill.DestroyChar();
                            Destroy(AtachCharObject);
                            AllIsMoveAreaDestroy();
                            AllSummonsCardDestroy();
                            if (AtachDeckObj != null)
                            {
                                AtachDeckObj.GetComponent<SummonsDeck>().SetIsCardShow();
                            }
                            MasterObject.GetComponent<BoardMaster>().SetLoseObj(AtachCharObject, AtachMassNumber);
                            MasterObject.GetComponent<BoardMaster>().SetAllFalseIsMove();
                            AtachCharObject = null;
                            ret = false;
                            //                       status = PlayerStatus.None;
                            GetComponent<Player>().SetState(Player.PlayerStatus.None);
                            break;

                        case BattleScene.BattleResult.Draw://引き分けの場合
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
                            AtachCharObject.GetComponent<CharacterStatus>().skill.BattleEnd();
                            AtachCharObject = null;
                            //                        status = PlayerStatus.None;
                            GetComponent<Player>().SetState(Player.PlayerStatus.None);
                            ret = false;
                            break;

                        case BattleScene.BattleResult.None:
                            break;
                    }
                }
                IsTargetBool = false;
                MasterObject.GetComponent<BoardMaster>().MoveCountSubtraction();
            }
            if (ret)//通常の移動の時
            {

                Debug.Log("通常移動");
                AtachCharObject.GetComponent<CharacterStatus>().skill.MoveStart();
                MasterObject.GetComponent<BoardList>().SetMoveList(AtachCharObject);
                AllIsMoveAreaDestroy();
                AllSummonsCardDestroy();
                if (AtachDeckObj != null)
                {
                    AtachDeckObj.GetComponent<SummonsDeck>().SetIsCardShow();
                }
                MovePos = MasterObject.GetComponent<BoardMaster>().GetPos(CopyAttachMassNumber);
                GameObject GetMass = MasterObject.GetComponent<BoardMaster>().GetMass(CopyAttachMassNumber);
                MovePos.z = 1.0f;
                AtachCharObject.transform.position = MovePos;
                if (RaceNum == 1)//移動するキャラクターがポーンの場合
                {
                    AtachCharObject.GetComponent<CharacterStatus>().SetIsFirst();
                }

                MasterObject.GetComponent<BoardMaster>().SetIsCharObj(CopyAttachMassNumber, AtachMassNumber, AtachCharObject);
                GetComponent<AtachMaster>().SetAttachMassObject(GetMass);
                AtachCharObject.GetComponent<CharacterStatus>().skill.MoveEnd();//移動が終わったときのキャラクターのスキル
                MasterObject.GetComponent<BoardSkillList>().ActiveEnemyMoveEndSkill();//相手の動きが終わったときに発動するスキル
                MasterObject.GetComponent<BoardMaster>().MoveCountSubtraction();
                MasterObject.GetComponent<BoardMaster>().SetAllFalseIsMove();
                AllAtachNull();
                GetComponent<Player>().SetState(Player.PlayerStatus.None);
                
            }
        }
        AllAtachNull();
        AllIsMoveAreaDestroy();
    }

    public void AllIsMoveAreaDestroy()//移動範囲の表示のオブジェクトをすべて消す
    {
        var clones = GameObject.FindGameObjectsWithTag("IsMovetag");
        foreach (var clone in clones)
        {
            Destroy(clone);
        }
    }
    public void AllSummonsCardDestroy()//移動範囲の表示のオブジェクトをすべて消す
    {
        var clones = GameObject.FindGameObjectsWithTag("SummonsCard");
        foreach (var clone in clones)
        {
            Destroy(clone);
        }
    }

    public void AllAtachNull()
    {
        //AtachCharObject = null;
        //CopyAtachMassObject = null;
        //AtachDeckObj = null;
    }

    public void SwitchDeck()//デッキのカードを開くか開かないか
    {
        GameObject AtachDeckObj = GetComponent<AtachMaster>().GetAttachDeckObj();
        bool IsOpen = AtachDeckObj.GetComponent<SummonsDeck>().GetISCardShow();
        if (IsOpen)
        {
            int MasterTurnNumber = MasterObject.GetComponent<BoardMaster>().GetTurnPlayer();
            int DeckNumber = AtachDeckObj.GetComponent<SummonsDeck>().GetPlayerNumber();
            if (MasterTurnNumber == DeckNumber)
            {
                AtachDeckObj.GetComponent<SummonsDeck>().ShowCard();
                //                status = PlayerStatus.ShowCard;
                GetComponent<Player>().SetState(Player.PlayerStatus.ShowCard);
            }
        }
        else
        {
            //            status = PlayerStatus.None;
            GetComponent<Player>().SetState(Player.PlayerStatus.None);
            AtachDeckObj.GetComponent<SummonsDeck>().SetIsCardShow();
            AllIsMoveAreaDestroy();
            AllSummonsCardDestroy();
            AllKariDestroy();
        }
    }//デッキを選択したら

    public void ChoosingCard()//カードを選択したら
    {
        GameObject AtachDeckCardObj = GetComponent<AtachMaster>().GetAttachDeckCardObj();
        PhaseMaster.Phase NowPhase = MasterObject.GetComponent<PhaseMaster>().GetNowFase();
        int DictionaryNum;
        Debug.Log("これはカードです");
        if (NowPhase == PhaseMaster.Phase.Move)
        {
            return;
        }
        int MasterTurnNumber = MasterObject.GetComponent<BoardMaster>().GetTurnPlayer();
        RaceNum = AtachDeckCardObj.GetComponent<IllustrationCard>().GetRaceNumber();
        SumonsCost = AtachDeckCardObj.GetComponent<IllustrationCard>().GetSumonCos();
        DictionaryNum = AtachDeckCardObj.GetComponent<IllustrationCard>().GetDictionaryNumber();
        MasterObject.GetComponent<BoardMaster>().SummonsFiledPos(RaceNum);
        GameObject ChoosingCardSummonObj;
        ChoosingCardSummonObj = MasterObject.GetComponent<CharacterMaster>().GetSummonsCharacter(DictionaryNum);
        GetComponent<AtachMaster>().SetChoosingCardSummonObj(ChoosingCardSummonObj);
    }

    public void SummonCard()
    {
        //召喚できるマスならば
        int AtachMassNumber = GetComponent<AtachMaster>().GetAttachMassNumber();
        GameObject AtachMassObject = GetComponent<AtachMaster>().GetAttachMassObj();
        bool retIsmove = MasterObject.GetComponent<BoardMaster>().GetIsMove(AtachMassNumber);
        bool retIsSPCost = MasterObject.GetComponent<BoardMaster>().UseSP(SumonsCost);
        GameObject ChoosingCardSummonObj = GetComponent<AtachMaster>().GetChoosingCardSummonObj();
        if (retIsmove && retIsSPCost)
        {
            MasterObject.GetComponent<BoardMaster>().SPDestroyCall();
            Debug.Log("召喚!");
            Vector3 SumonsPos = AtachMassObject.transform.position;
            SumonsPos.z += 1;
            GameObject InstanceSumonObj = Instantiate(ChoosingCardSummonObj, SumonsPos, ChoosingCardSummonObj.transform.rotation);
            int MassNum = AtachMassObject.GetComponent<NumberMass>().GetNumber();
            MasterObject.GetComponent<BoardMaster>().SetIsCharObj(MassNum, InstanceSumonObj);
            MasterObject.GetComponent<BoardMaster>().SetStatusIsMoveArea(MassNum);
            GameObject AtachDeckObj = GetComponent<AtachMaster>().GetAttachDeckObj();
            AtachDeckObj.GetComponent<SummonsDeck>().SetIsCardShow();
            if (RaceNum != 1)//ポーン以外の召喚酔い
            {
                InstanceSumonObj.GetComponent<CharacterStatus>().SetSummoningSickness(1);
                MasterObject.GetComponent<BoardMaster>().SetSummoningSicknessCharacterList(InstanceSumonObj);
            }
            GetComponent<AtachMaster>().SetInstanceSumonObj(InstanceSumonObj);
            MasterObject.GetComponent<BoardMaster>().SetAllFalseIsMove();
            SetIniSummonCard();
            AllIsMoveAreaDestroy();
            AllSummonsCardDestroy();
            AllKariDestroy();
            GetComponent<Player>().SetState(Player.PlayerStatus.None);
        }
    }

    public void AllKariDestroy()
    {
        var clones = GameObject.FindGameObjectsWithTag("Kari");
        foreach (var clone in clones)
        {
            Destroy(clone);
        }
    }

    public void SetIniSummonCard()
    {
        int pnum;
        GameObject InstanceSumonObj = GetComponent<AtachMaster>().GetInstanceSumonObj();
        pnum = MasterObject.GetComponent<BoardMaster>().GetTurnPlayer();
        InstanceSumonObj.GetComponent<CharacterStatus>().SetPlayerNumber(pnum);
        InstanceSumonObj.GetComponent<MoveData>().ReadSetObj(InstanceSumonObj);
        InstanceSumonObj.GetComponent<ReadCsv>().SetTargetObj(InstanceSumonObj);
        InstanceSumonObj.GetComponent<MoveData>().IniSet();
        MasterObject.GetComponent<BoardMaster>().SetSummonCharacters(InstanceSumonObj);
        GetComponent<AtachMaster>().SetInstanceSumonObj(InstanceSumonObj);
    }

    public void SetIsTargetBool(bool set)
    {
        IsTargetBool = set;
    }

    public void SetSkillActive(SkillActivate setacitive)
    {
        skillactive = setacitive;
    }


    public void SkillTarget()
    {
        GameObject CopyAtachMassObject = GetComponent<AtachMaster>().GetAttachMassObj();
        int AtachMassNumber = GetComponent<AtachMaster>().GetAttachMassNumber();
        GameObject AtachCharObject = MasterObject.GetComponent<BoardMaster>().GetCharObject(AtachMassNumber);
        if (AtachCharObject != null) {
            int num = AtachCharObject.GetComponent<CharacterStatus>().GetPlayerNumber();
            int turnnum = MasterObject.GetComponent<BoardMaster>().GetTurnPlayer();
            if (num != turnnum)
            {
                Debug.Log("ターゲットセット");
                GetComponent<AtachMaster>().SetSkillTarget(AtachCharObject);
            }
        }
        else if(AtachCharObject == null)
        {
            Debug.Log("nullnullnull");
        }
    }
    /// <summary>
    /// スキルを発動するかどうか
    /// </summary>
    /// <returns></returns>
    public bool IsSkill()
    {
        GameObject AtachCharObject = GetComponent<AtachMaster>().GetAttachCharObj();
        if (skillactive == SkillActivate.None)
        {
            GetComponent<Player>().SetState(Player.PlayerStatus.SkillChoosing);
            skillactive = SkillActivate.Choosing;
            return false;
        }
        switch (skillactive)
        {
            case SkillActivate.Choosing:
                return false;
            case SkillActivate.Yes:
                //AtachCharObject.GetComponent<CharacterStatus>().skill.BattleStart();//戦闘が始まったときのキャラクターのスキルの処理
                skillactive = SkillActivate.None;
                return true;
            case SkillActivate.No:
                skillactive = SkillActivate.None;
                return true;
                
            case SkillActivate.None:
                break;
        }
        return false;
    }


    public SkillActivate GetSkillActive()
    {
        return skillactive;
    }

    public SkillActivateGo GetSkillActiveGo()
    {
        return skillgo;
    }
    public void SetSkillActiveGo(SkillActivateGo set)
    {
        skillgo = set;
    }
}