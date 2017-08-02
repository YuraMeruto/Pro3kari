using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDataver2 : MonoBehaviour
{

    [SerializeField]
    private GameObject MoveAreaObj;
    [SerializeField]
    private GameObject MoveAreaEnemyObj;

    [SerializeField]
    private GameObject MasterObj;
    [SerializeField]
    private int NowPosSide = 0;
    [SerializeField]
    private int NowPosLengith = 0;
    private GameObject MainCameraObj;
    private const int MaxSide = 6;
    private const int MaxLength = 6;
    private List<int> Massnumlist = new List<int>();
    public void IsPossibleMove(int massnum)
    {
        MasterObj.GetComponent<BoardMaster>().SetNowPos(ref NowPosLengith, ref NowPosSide, massnum);//現在の場所を索敵
        int GetRaceNumer = GetComponent<CharacterStatus>().GetRace();
        Debug.Log(GetRaceNumer+"あああ");
        int turn = MasterObj.GetComponent<BoardMaster>().GetTurnPlayer();
        switch (GetRaceNumer)
        {
            case 1://ポーンの場合
                PornMove(turn, massnum);
                break;
            case 2://ルークの場合
                LukeMove(turn, massnum);
                break;
            case 3://ナイトの場合
                KnightMove(turn, massnum);
                break;
            case 4://ビショップの場合
                BiShopMove(turn,massnum);
                break;
            case 5://クイーンの場合
                LukeMove(turn,massnum);
                BiShopMove(turn,massnum);
                break;
            case 6://キングの場合
                KingMove(turn,massnum);
                break;
        }

    }
    void Start()
    {
        MasterObj = GameObject.Find("Master");
    }

    /// <summary>
    /// ポーンの移動処理
    /// </summary>
    /// <param name="turnnum"></param>
    /// <param name="massnum"></param>
    public void PornMove(int turnnum, int massnum)
    {
        int MovePoslength = NowPosLengith;
        if (turnnum == 1)
        {
            Debug.Log(MovePoslength);
            MovePoslength++;
            Debug.Log("プラスだよ");
            Debug.Log(MovePoslength);

        }
        else if (turnnum == 2)
        {
            Debug.Log(MovePoslength);

            MovePoslength--;
            Debug.Log("マイナスだよ");
            Debug.Log(MovePoslength);

        }
        PornMoveArea(MovePoslength);
    }

    /// <summary>
    /// 今のポジションをセット
    /// </summary>
    /// <param name="side"></param>
    /// <param name="length"></param>
    /// <param name="playernum"></param>
    public void NowPosition(int side, int length)
    {
        NowPosSide = side;
        NowPosLengith = length;
    }

    /// <summary>
    /// ポーンの移動範囲を表示
    /// </summary>
    /// <param name="length"></param>
    void PornMoveArea(int length)
    {
        for (int sidecount = -1; sidecount < 2; sidecount++)
        {
            int MovePosSide = NowPosSide;
            MovePosSide += sidecount;
            bool moveresult = Is_OutSideArea(length, MovePosSide);
            if (moveresult)
            {
                GameObject CharObj = MasterObj.GetComponent<BoardMaster>().GetCharObject(length, MovePosSide);
                if (sidecount == 0)
                {
                    CharObj = MasterObj.GetComponent<BoardMaster>().GetCharObject(length, MovePosSide);
                    MasterObj.GetComponent<BoardMaster>().SetIsMove(length, MovePosSide, true);
                    bool isomovearea = MasterObj.GetComponent<BoardMaster>().GetIsMove(length, MovePosSide);
                    if (CharObj == null)
                    {
                        InstanceMoveArea(length, MovePosSide);
                    }
                }
                else
                {
                    if (CharObj != null)
                    {
                        InstanceEnemyArea(length, MovePosSide);
                    }
                }
            }
        }

    }
    /// <summary>
    /// ナイトの移動処理
    /// </summary>
    /// <param name="turnnum"></param>
    /// <param name="massnum"></param>
    void KnightMove(int turnnum, int massnum)
    {
        int MovePosLength = 0;
        int MovePosSide = 0;
        for (int count = 0; count < 8; count++)
        {
            KnightCase(count, ref MovePosLength, ref MovePosSide);
            bool moveresult = Is_OutSideArea(MovePosLength, MovePosSide);
            if (moveresult)
            {
                GameObject CharObj = MasterObj.GetComponent<BoardMaster>().GetCharObject(MovePosLength, MovePosSide);
                MasterObj.GetComponent<BoardMaster>().SetIsMove(MovePosLength, MovePosSide, true);
                if (CharObj == null)
                {
                    InstanceMoveArea(MovePosLength, MovePosSide);
                }
                else
                {
                    InstanceEnemyArea(MovePosLength, MovePosSide);
                }
            }
        }
    }

    /// <summary>
    /// キングの移動処理
    /// </summary>
    /// <param name="turn"></param>
    /// <param name="massnum"></param>
    void KingMove(int turn,int massnum)
    {
        for(int length =-1;length<=1;length++)
        {
            int MovePosLength = NowPosLengith + length;
            for (int side =-1;side <=1;side++)
            {
                int MovePosSide = NowPosSide + side;
                KingMoveArea(MovePosLength,MovePosSide);
            }
        }
    }
    void KingMoveArea(int length, int side)
    {
        bool moveresultarea = Is_OutSideArea(length, side);
        if (moveresultarea)
        {
            GameObject CharObj = MasterObj.GetComponent<BoardMaster>().GetCharObject(length, side);
            MasterObj.GetComponent<BoardMaster>().SetIsMove(length, side, true);
            if (CharObj == null)
            {
                InstanceMoveArea(length, side);
            }
            else
            {
                InstanceEnemyArea(length, side);
            }
        }
    }
    /// <summary>
    /// ルークの移動処理
    /// </summary>
    /// <param name="turnnum"></param>
    /// <param name="massnum"></param>
    void LukeMove(int turnnum, int massnum)
    {

        for (int count = 1; count < 6; count++)//右に移動索敵
        {
            bool ret = true;
            int MovePosLength = NowPosLengith;
            int MovePosSide = NowPosSide + count;
            ret = LukeBishopMoveArea(MovePosLength, MovePosSide);
            if (ret == false)
            {
                break;
            }
        }
        for (int count = 1; count < 6; count++)//左に移動索敵
        {
            bool ret = true;
            int MovePosLength = NowPosLengith;
            int MovePosSide = NowPosSide - count;
            ret = LukeBishopMoveArea(MovePosLength, MovePosSide);
            if (ret == false)
            {
                break;
            }
        }
        for (int count = 1; count < 6; count++)//上に移動索敵
        {
            bool ret = true;
            int MovePosLength = NowPosLengith + count;
            int MovePosSide = NowPosSide;
            ret = LukeBishopMoveArea(MovePosLength, MovePosSide);
            if (ret == false)
            {
                break;
            }
        }
        for (int count = 1; count < 6; count++)//下に移動索敵
        {
            bool ret = true;
            int MovePosLength = NowPosLengith - count;
            int MovePosSide = NowPosSide;
            ret = LukeBishopMoveArea(MovePosLength, MovePosSide);
            if (ret == false)
            {
                break;
            }
        }
    }

    bool LukeBishopMoveArea(int length, int side)
    {
        bool moveresultarea = Is_OutSideArea(length, side);
        if (moveresultarea)
        {
            GameObject CharObj = MasterObj.GetComponent<BoardMaster>().GetCharObject(length, side);
            GameObject MassObj = MasterObj.GetComponent<BoardMaster>().GetMass(length,side);
            int Massnum = MassObj.GetComponent<NumberMass>().GetNumber();
            MasterObj.GetComponent<BoardMaster>().SetIsMove(length, side, true);
            if (CharObj == null)
            {
                Massnumlist.Add(Massnum);
                InstanceMoveArea(length, side);
                return true;
            }
            else
            {
                InstanceEnemyArea(length, side);
                return false;
            }
        }
        return true;
    }

    void BiShopMove(int turn,int massnum)
    {
        for(int count = 1;count < 6;count++)
        {
            bool ret = true;
            int MovePosLength = NowPosLengith + count;
            int MovePosSide = NowPosSide + count;
            ret = LukeBishopMoveArea(MovePosLength,MovePosSide);
            if (ret == false)
                break;
        }

        for (int count = 1; count < 6; count++)
        {
            bool ret = true;
            int MovePosLength = NowPosLengith + count;
            int MovePosSide = NowPosSide - count;
            ret = LukeBishopMoveArea(MovePosLength, MovePosSide);
            if (ret == false)
                break;
        }

        for (int count = 1; count < 6; count++)
        {
            bool ret = true;
            int MovePosLength = NowPosLengith - count;
            int MovePosSide = NowPosSide + count;
            ret = LukeBishopMoveArea(MovePosLength, MovePosSide);
            if (ret == false)
                break;
        }

        for (int count = 1; count < 6; count++)
        {
            bool ret = true;
            int MovePosLength = NowPosLengith - count;
            int MovePosSide = NowPosSide - count;
            ret = LukeBishopMoveArea(MovePosLength, MovePosSide);
            if (ret == false)
                break;
        }

    }

    void BiShopCase()
    {

    }
    /// <summary>
    /// エリア判定
    /// </summary>
    /// <param name="length"></param>
    /// <param name="side"></param>
    /// <returns></returns>
    bool Is_OutSideArea(int length, int side)
    {
        bool ret = true;
        if (length >= 6)
            ret = false;
        else if (length < 0)
            ret = false;

        if (side >= 6)
            ret = false;
        else if (side < 0)
            ret = false;

        return ret;
    }

    /// <summary>
    /// ナイトのCASE
    /// </summary>
    /// <param name="count"></param>
    /// <param name="MovePosLength"></param>
    /// <param name="MovePosSide"></param>
    void KnightCase(int count, ref int MovePosLength, ref int MovePosSide)
    {

        switch (count)
        {
            case 0:
                MovePosLength = NowPosLengith + 1;
                MovePosSide = NowPosSide - 2;
                break;
            case 1:
                MovePosLength = NowPosLengith + 2;
                MovePosSide = NowPosSide - 1;
                break;
            case 2:
                MovePosLength = NowPosLengith + 2;
                MovePosSide = NowPosSide + 1;
                break;
            case 3:
                MovePosLength = NowPosLengith + 1;
                MovePosSide = NowPosSide + 2;
                break;

            case 4:
                MovePosLength = NowPosLengith - 1;
                MovePosSide = NowPosSide + 2;
                break;
            case 5:
                MovePosLength = NowPosLengith - 2;
                MovePosSide = NowPosSide + 1;
                break;
            case 6:
                MovePosLength = NowPosLengith - 2;
                MovePosSide = NowPosSide - 1;
                break;
            case 7:
                MovePosLength = NowPosLengith - 1;
                MovePosSide = NowPosSide - 2;
                break;
        }

    }

    public void ListClear()
    {
        Massnumlist.Clear();
    }

    /// <summary>
    /// エネミーがいるときのオブジェクト
    /// </summary>
    /// <param name="length"></param>
    /// <param name="side"></param>
    void InstanceEnemyArea(int length, int side)
    {
        Vector3 Pos = MasterObj.GetComponent<BoardMaster>().GetMassPos(length, side);
        Pos.z = 1;
        Instantiate(MoveAreaEnemyObj, Pos, Quaternion.identity);
    }

    /// <summary>
    /// 通常移動できるときのオブジェクト
    /// </summary>
    /// <param name="length"></param>
    /// <param name="side"></param>
    void InstanceMoveArea(int length, int side)
    {
        Vector3 Pos = MasterObj.GetComponent<BoardMaster>().GetMassPos(length, side);
        Pos.z = 1;
        Instantiate(MoveAreaObj, Pos, Quaternion.identity);
    }
    public int ResearchMass(int num,ref bool ret)
    {
        for (int count = 0; count <Massnumlist.Count;count++)
        {
            if(num == Massnumlist[count])
            {
                ret = true;
                return Massnumlist[count];
            }
        }
        return 0;
    }
}
