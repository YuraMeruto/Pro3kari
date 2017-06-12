﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseState : MonoBehaviour
{
    private GameObject MasterObject;
    private int RaceNum;
    private int SumonsCost;
    [SerializeField]
    GameObject IsEnemyObj;
    void Start()
    {
        MasterObject = GameObject.Find("Master");
    }

 public   void SwitchPlayerNone()//何も選択していない状態でキャラクターを選択した時
    {
        int AtachCharNumber = 0;
        //        CopyAtachMassObject = AtachMassObject;
        //       AtachMassNumber = AtachMassObject.GetComponent<NumberMass>().GetNumber();
      GameObject  CopyAtachMassObject = GetComponent<AtachMaster>().GetAttachMassObj();
      int  AtachMassNumber = GetComponent<AtachMaster>().GetAttachMassNumber();
      GameObject AtachCharObject = MasterObject.GetComponent<BoardMaster>().GetCharObject(AtachMassNumber);
        //        AtachCharObject = GetComponent<AtachMaster>().GetAttachCharObj();
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
                bool ISSkillTargetList = MasterObject.GetComponent<BoardList>().GetList(IsEnemyObj);
                if(ISSkillTargetList)
                {
                    Debug.Log("リスト内にターゲットがいたぞ");
                }
                AtachCharObject.GetComponent<CharacterStatus>().skill.BattleStart();//戦闘が始まったときのキャラクターのスキルの処理
                GetComponent<AtachMaster>().SetEnemyObj(IsEnemyObj);
                var BattleRet = GetComponent<BattleScene>().Result();
               
                switch (BattleRet)
                {

                    case BattleScene.BattleResult.Win://先に攻撃したキャラクターが勝った場合
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

                        AtachCharObject = null;
                        ret = false;

//                        status = PlayerStatus.None;
                        GetComponent<Player>().SetState(Player.PlayerStatus.None);
                        break;

                    case BattleScene.BattleResult.Lose://先に攻撃したキャラクターが負けた場合
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
                        AtachCharObject = null;
//                        status = PlayerStatus.None;
                        GetComponent<Player>().SetState(Player.PlayerStatus.None);
                        ret = false;
                        break;
                }
                MasterObject.GetComponent<BoardMaster>().MoveCountSubtraction();
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
                if (RaceNum == 1)//移動するキャラクターがポーンの場合
                {
                    AtachCharObject.GetComponent<CharacterStatus>().SetIsFirst();
                }
                MasterObject.GetComponent<BoardMaster>().SetIsCharObj(CopyAttachMassNumber, AtachMassNumber, AtachCharObject);
                MasterObject.GetComponent<BoardMaster>().SetAllFalseIsMove();
                AtachCharObject.GetComponent<CharacterStatus>().skill.MoveEnd();//移動が終わったときの処理
                MasterObject.GetComponent<BoardMaster>().MoveCountSubtraction();

                //                AtachCharObject.GetComponent<CharacterStatus>().SkillStart();
                AllAtachNull();
                GetComponent<Player>().SetState(Player.PlayerStatus.None);
            }
        }
        AllAtachNull();
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

    public void SwitchDeck()
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
            SetIniSummonCard();
            AllIsMoveAreaDestroy();
            AllSummonsCardDestroy();
            AllKariDestroy();
//            status = PlayerStatus.None;
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
        GetComponent<AtachMaster>().SetInstanceSumonObj(InstanceSumonObj);
    }
}