using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMaster : MonoBehaviour
{
    public GameObject DeckObj;

    [SerializeField]
    private List<GameObject> CharacterList = new List<GameObject>();
    private enum Phase {None,Main1,Move,Main2,TurnEnd };
    private Phase phase = Phase.Main1;
    [SerializeField]
    private string FileName;
    private int[,] IntData = new int[10, 10];
    private int[,] FieldSummonData = new int[10, 10];
    public GameObject Kari;
    private int[,] MassArea = new int[10, 10];//自軍か敵軍の判別用
    public GameObject[,] MassObj = new GameObject[10, 10];
    public bool[,] IsMoveMassObj = new bool[10, 10];
    private GameObject[,] CharObj = new GameObject[10, 10];
    public int[,] MassNum = new int[10, 10];
    [SerializeField]
    private int TurnPlayer;//現在どのプレイヤーが操作できるか確認
    public enum Status { None, OK, NG, IsMoveArea };
    public Status[,] MassStatus = new Status[10, 10];
    private int PlayerNumber;
    [SerializeField]
    private int MaxLength;
    [SerializeField]
    private int MaxSide;

    private int CopyCost;
    private int MaxNumber;
    [SerializeField]
    private GameObject PlayerTurnUI;
    [SerializeField]
    private List<GameObject> SummoningSicknessCharacterList = new List<GameObject>();
    //マスのオブジェクト
    [SerializeField]
    private GameObject[] ObjMass = new GameObject[2];

    //キャラクターのオブジェクト
    [SerializeField]
    private GameObject[] ObjCharcter = new GameObject[2];

    private Vector3 InstancePos = Vector3.zero;

    [SerializeField]
    private LayerMask MassLayer;

    [SerializeField]
    private GameObject SPObj;

    [SerializeField]
    private GameObject[] PlayerObj = new GameObject[0];

    [SerializeField]
    private GameObject SPPos;

    [SerializeField]
    private List<GameObject> AtTheStartSkillList = new List<GameObject>();

    [SerializeField]
    private int MoveCount = 2;
    // Use this for initialization
    void Start()
    {
        Vector3 DeckPos;
        TurnPlayer = 1;
        int count = 0;
        int masscolor = 0;
        for (int length = 0; length < MaxLength; length++)
        {
            for (int side = 0; side < MaxSide; side++)
            {
                InstancePos.x = side;
                InstancePos.y = length;
                if (length == 0 && side == 0)
                    masscolor = 1;
                GameObject Mass = Instantiate(ObjMass[masscolor], InstancePos, Quaternion.identity);
                Mass.transform.SetParent(transform);
                Mass.layer = 8;
                Mass.name = count.ToString();
                IsMoveMassObj[length, side] = false;
                Mass.GetComponent<NumberMass>().SetNumber(count);
                MassObj[length, side] = Mass;
                MassNum[length, side] = count;
                MassStatus[length, side] = Status.None;
                count++;
                if (masscolor == 0)
                {
                    masscolor = 1;
                }
                else
                {
                    masscolor = 0;
                }
                MassArea[length, side] = 0;
            }

            if (masscolor % 2 == 0)
            {
                masscolor = 1;
            }
            else
            {
                masscolor = 0;
            }

        }
        for (int x = 0; x <= MaxLength; x++)
        {
            MassArea[0, x] = 1;
            MassArea[1, x] = 1;
            MassArea[7, x] = 2;
            MassArea[6, x] = 2;

        }
        MaxNumber = count;
        //デッキの生成開始   
        DeckPos = MassObj[0, 0].transform.position;
        DeckPos.x = DeckPos.x - 3;
        GameObject Deck1 = Instantiate(DeckObj, DeckPos, DeckObj.transform.rotation);
        PlayerNumber = 1;
        Deck1.GetComponent<SummonsDeck>().SetPlayerNumber(PlayerNumber);
        DeckPos = MassObj[7, 7].transform.position;
        DeckPos.x = DeckPos.x + 3;
        GameObject Deck2 = Instantiate(DeckObj, DeckPos, DeckObj.transform.rotation);
        PlayerNumber = 2;
        Deck2.GetComponent<SummonsDeck>().SetPlayerNumber(PlayerNumber);
        //デッキ生成終了

        //SP設定
        PlayerObj[TurnPlayer].GetComponent<SP>().InstanceSPObj();
        SetTurnPlayer();
    }

    public int GetMaxNumber()
    {
        return MaxNumber;
    }

    /// <summary>
    /// マスオブジェクトからナンバーからステータスを変更させる
    /// </summary>
    /// <param name="num"></param>
    /// <param name="stat"></param>
    public void MassNumber(int num, Status stat)
    {
        for (int length = 0; length <= MaxLength; length++)
        {
            for (int side = 0; side <= MaxSide; side++)
            {
                if (MassNum[length, side] == num)
                {
                    MassStatus[length, side] = stat;
                }
            }
        }
    }

    /// <summary>
    /// マスのステータスの変更
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public bool GetMassStatusNone(int num)
    {
        bool ret = false;
        for (int length = 0; length < MaxLength; length++)
        {
            for (int side = 0; side < MaxSide; side++)
            {
                if (MassNum[length, side] == num)
                {
                    if (MassStatus[length, side] == Status.None)
                    {
                        ret = true;
                    }
                }
            }
        }
        return ret;
    }


    public bool GetMassStatusNone(int MassLength, int MassSide)
    {
        bool ret = false;
        if (MassStatus[MassLength, MassSide] == Status.None)
        {
            ret = true;
        }
        return ret;
    }

    public Status GetMassStatus(int num)
    {
        for (int length = 0; length < MaxLength; length++)
        {
            for (int side = 0; side < MaxSide; side++)
            {
                if (MassNum[length, side] == num)
                {
                    return MassStatus[length, side];
                }
            }
        }
        return Status.None;
    }


    public Status GetMassStatus(int MassLength, int MassSide)
    {
        return MassStatus[MassLength, MassSide];
    }


    public GameObject GetCharObject(int charlength, int charside)
    {
        GameObject Obj = null;
        Obj = CharObj[charlength, charside];
        return Obj;
    }

    /// <summary>
    /// マスにいるキャラクターを取得
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public GameObject GetCharObject(int num)
    {
        GameObject ret = null;
        for (int length = 0; length < MaxLength; length++)
        {
            for (int side = 0; side < MaxSide; side++)
            {
                if (MassNum[length, side] == num)
                {
                    if (CharObj[length, side] != null)
                        ret = CharObj[length, side];

                }
            }
        }
        return ret;
    }

    public int GetMaxLength()
    {
        return MaxLength;
    }

    public int GetMaxSide()
    {
        return MaxSide;
    }

    public void SetStatusIsMoveArea(int num)
    {
        for (int length = 0; length < MaxLength; length++)
        {
            for (int side = 0; side < MaxSide; side++)
            {
                if (MassNum[length, side] == num)
                {
                    MassStatus[length, side] = Status.IsMoveArea;
                }
            }
        }
    }
    public void SetStatusIsMoveArea(int length, int side)
    {
        MassStatus[length, side] = Status.IsMoveArea;
    }


    public bool GetIsMove(int num)
    {
        for (int length = 0; length < MaxLength; length++)
        {
            for (int side = 0; side < MaxSide; side++)
            {
                if (MassNum[length, side] == num)
                    return IsMoveMassObj[length, side];
            }
        }
        return false;
    }

    public bool GetIsMove(int length, int side)
    {
        return IsMoveMassObj[length, side];
    }

    public void SetAllFalseIsMove()
    {
        for (int length = 0; length < MaxLength; length++)
        {
            for (int side = 0; side < MaxSide; side++)
            {
                IsMoveMassObj[length, side] = false;
            }
        }
    }

    public void SetIsCharObj(int newnum, int oldnum, GameObject charobj)
    {
        for (int length = 0; length < MaxLength; length++)
        {
            for (int side = 0; side < MaxSide; side++)
            {
                if (MassNum[length, side] == oldnum)
                {
                    CharObj[length, side] = null;
                }
                if (MassNum[length, side] == newnum)
                {
                    CharObj[length, side] = charobj;
                    Instantiate(Kari, MassObj[length, side].transform.position, Quaternion.identity);//仮でしています
                }

            }
        }

    }

    //召喚したとき用
    public void SetIsCharObj(int newnum, GameObject charobj)
    {
        for (int length = 0; length < MaxLength; length++)
        {
            for (int side = 0; side < MaxSide; side++)
            {
                if (MassNum[length, side] == newnum)
                {
                    CharObj[length, side] = charobj;
                    //Destroy(MassObj[length, side]);  

                }

            }
        }

    }

    public Vector3 GetPos(int num)
    {
        Vector3 vec = Vector3.zero;
        for (int length = 0; length < MaxLength; length++)
        {
            for (int side = 0; side < MaxSide; side++)
            {
                if (MassNum[length, side] == num)
                {
                    vec = MassObj[length, side].transform.position;
                }

            }
        }
        return vec;

    }
    public void SetIsMove(int length, int side, bool ret)
    {
        IsMoveMassObj[length, side] = ret;
    }

    public void SetTurnPlayer()
    {
        
        Debug.Log("ターンが変わったよ");
        GetComponent<SkillsMaster>().EndSkills();//登録されているキャラクターのスキル発動
        PlayerObj[TurnPlayer].GetComponent<SP>().ClearList();//ターンが変わる前にいったん全部消す
        AllSPObjDestroy();
        TurnEnd();
        switch (TurnPlayer)
        {
            case 1:
                TurnPlayer = 2;
                PlayerTurnUI.GetComponent<PlayerTurn>().SetColorBlue();
                PlayerTurnUI.GetComponent<PlayerTurn>().IsSetDownTrue();
                PlayerTurnUI.GetComponent<PlayerTurn>().SetText("プレイヤー2のターン");
               // PlayerTurnUI.GetComponent<PlayerTurn>().IsSetDownFalse();
                break;
            case 2:
                TurnPlayer = 1;
                PlayerTurnUI.GetComponent<PlayerTurn>().SetColorRed();
                PlayerTurnUI.GetComponent<PlayerTurn>().IsSetDownTrue();
                PlayerTurnUI.GetComponent<PlayerTurn>().SetText("プレイヤー1のターン");
               // PlayerTurnUI.GetComponent<PlayerTurn>().IsSetDownFalse();
                break;
        }
        TurnStart();
        GetComponent<SkillsMaster>().StartSkill();//登録されているキャラクターのスキル発動
        PlayerObj[TurnPlayer].GetComponent<SP>().ResetAddSP();
        PlayerObj[TurnPlayer].GetComponent<SP>().InstanceSPObj();
        //   PlayerObj[TurnPlayer].GetComponent<SP>().ClearList();
    }

    public int GetTurnPlayer()

    {
        return TurnPlayer;
    }

    /// <summary>
    /// 引き分けの時の処理
    /// </summary>
    /// <param name="Enemynum"></param>
    /// <param name="Playernum"></param>
    /// <param name="PlayerPos"></param>
    /// <returns></returns>
    public Vector3 EnemySurroundings(int Enemynum, int Playernum, GameObject PlayerPos)
    {
        Vector3 vec = Vector3.zero;
        int EnemyMassLength = 0;
        int EnemyMassSide = 0;
        for (int length = 0; length < MaxLength; length++)
        {
            for (int side = 0; side < MaxSide; side++)
            {
                if (MassNum[length, side] == Enemynum)//移動先のマスのナンバーを索敵(エネミーのマスの場所を索敵)
                {
                    EnemyMassLength = length;
                    EnemyMassSide = side;
                }
            }
        }

        for (int Movelength = -1; Movelength <= 1; Movelength++)//最初から隣接しているときの処理
        {
            for (int Moveside = -1; Moveside <= 1; Moveside++)
            {
                //                Vector3 pos = MassObj[EnemyMassLength + Movelength, EnemyMassSide + Moveside].transform.position;
                //                pos.y = 1.0f;
                bool IsOutArea = OutSideTheArea(EnemyMassLength + Movelength, EnemyMassSide + Moveside);//敵のマスからみて最初から隣接して要る時
                if (IsOutArea)
                {

                    if (MassNum[EnemyMassLength + Movelength, EnemyMassSide + Moveside] == Playernum)//最初から隣接していたら
                    {

                        Debug.Log("最初から隣接しています");
                        return vec = PlayerPos.transform.position;

                    }
                }
            }
        }

        for (int Movelength = -1; Movelength <= 1; Movelength++)//隣接していない時
        {
            for (int Moveside = -1; Moveside <= 1; Moveside++)
            {
                bool IsOutArea = OutSideTheArea(EnemyMassLength + Movelength, EnemyMassSide + Moveside);
                if (IsOutArea)
                {
                    if (MassNum[EnemyMassLength + Movelength, EnemyMassSide + Moveside] != Enemynum)
                    {
                        if (IsMoveMassObj[EnemyMassLength + Movelength, EnemyMassSide + Moveside])
                        {
                            Debug.Log("隣接していません");
                            vec = MassObj[EnemyMassLength + Movelength, EnemyMassSide + Moveside].transform.position;

                            int NewMassnum = GetMassNum(EnemyMassLength + Movelength, EnemyMassSide + Moveside);
                            //Vector3 pos = MassObj[EnemyMassLength + Movelength, EnemyMassSide + Moveside].transform.position;
                            //pos.y = 1.0f;

                            SetIsCharObj(NewMassnum, Playernum, PlayerPos);
                            return vec;
                        }
                    }

                }
            }
        }


        return vec;
    }

    public bool OutSideTheArea(int InstanceLength, int InstanceSide)
    {
        bool ret = true;

        if (InstanceLength >= MaxLength || InstanceSide >= MaxSide)
        {
            ret = false;
        }

        else if (InstanceLength < 0 || InstanceSide < 0)
        {
            ret = false;
        }
        return ret;
    }
    public void SetLoseObj(GameObject LoseObj, int num)
    {

    }

    public int GetMassNum(int length, int side)
    {
        int num = MassNum[length, side];
        Vector3 pos = MassObj[length, side].transform.position;
        //  Instantiate(Kari, MassObj[length, side].transform.position,Quaternion.identity);
        return num;
    }

    /// <summary>
    /// デバック用
    /// </summary>
    /// <param name="obj"></param>
    public void AllInstance(GameObject obj)
    {
        for (int length = 0; length < MaxLength - 1; length++)
        {
            for (int side = 0; side < MaxSide - 1; side++)
            {
                if (CharObj[length, side] != null)//移動先のマスのナンバーを索敵(エネミーのマスの場所を索敵)
                {
                    //          Vector3 ppp = CharObj[length, side].transform.position;
                    //            ppp.y = 2.0f;
                    //          GameObject a = Instantiate(obj,ppp,Quaternion.identity);
                    //            a.name = "test";
                }
            }
        }

    }
    /// <summary>
    /// デッキから選択されたカードをタッチして召喚できる場所を表示させる
    /// </summary>
    /// <param name="num"></param>
    public void SummonsFiledPos(int num)
    {
        int MaxLMap = GetComponent<SummonsPosData>().GetMaxLength();
        int MaxSMap = GetComponent<SummonsPosData>().GetMaxSide();
        Debug.Log(MaxLMap);
        Debug.Log(MaxSMap);
        for (int length = 0; length <= MaxLMap; length++)
        {
            for (int side = 0; side <= MaxSMap; side++)
            {
                if (FieldSummonData[length, side] == num)
                {
                    if (MassArea[length, side] == TurnPlayer)
                    {
                        if (CharObj[length, side] == null)
                        {
                            Vector3 Pos = MassObj[length, side].transform.position;
                            IsMoveMassObj[length, side] = true;
                            Pos.z += 1;
                            Instantiate(Kari, Pos, Quaternion.identity);
                        }
                    }
                }
            }
        }
        /*
        if (MassArea[1, 0] == 1)
        {
            Vector3 Pospos = MassObj[1, 0].transform.position;
            Pospos.z += 1;
            Instantiate(Kari, Pospos, Quaternion.identity);
        }
        Debug.Log(MassObj[0, 1]);
        */
    }

    /// <summary>
    /// 召喚できるポジションのデータが読み込まれた後実行される
    /// </summary>
    /// <param name="num"></param>
    /// <param name="length"></param>
    /// <param name="side"></param>
    public void SetSummonsPosData(int num, int length, int side)
    {
        FieldSummonData[side, length] = num;
        /*
        if (num == 1 && MassArea[side,length] == 1)
        {
            Vector3 Pos = MassObj[side,length].transform.position;
            Pos.z += 1;
            Instantiate(Kari, Pos, Quaternion.identity);
        }
        */
    }


    public void DebugInst()
    {
        //Vector3 Pospos = MassObj[1, 0].transform.position;
        //Pospos.z += 1;
        //Instantiate(Kari, Pospos, Quaternion.identity);

        int MaxLMap = GetComponent<SummonsPosData>().GetMaxLength();
        int MaxSMap = GetComponent<SummonsPosData>().GetMaxSide();

        for (int length = 0; length <= MaxLMap; length++)
        {
            for (int side = 0; side <= MaxSMap; side++)
            {
                if (MassArea[length, side] == 1)
                {
                    Vector3 Pos = MassObj[length, side].transform.position;
                    Pos.z += 1;
                    Instantiate(Kari, Pos, Quaternion.identity);
                }

            }
        }
    }

    public void SetMassArea(int num)
    {
        for (int length = 0; length < MaxLength; length++)
        {
            for (int side = 0; side < MaxSide; side++)
            {
                if (MassNum[length, side] == num)
                {
                    MassArea[length, side] = TurnPlayer;
                }
            }
        }
    }

    /// <summary>
    /// SPObjを生成
    /// </summary>
    public void InstanceSP()
    {
        //  int SPCount;
        // SPCount = PlayerObj[TurnPlayer].GetComponent<SP>().GetSP();
        //  Vector3 InstancePos = SPPos.transform.position;
        //  PlayerObj[TurnPlayer].GetComponent<SP>().InstanceSPObj();
    }
    public void DebugMassArea()
    {
        for (int l = 0; l < MaxLength; l++)
        {
            for (int s = 0; s < MaxSide; s++)
                if (MassArea[l, s] == 1)
                {
                    Vector3 p = MassObj[l, s].transform.position;
                    p.z += 1;
                    Instantiate(Kari, p, Quaternion.identity);
                }
        }
    }//デバック用

    public bool UseSP(int costnum)
    {
        CopyCost = costnum;
        int NowSP = PlayerObj[TurnPlayer].GetComponent<SP>().GetSP();
        NowSP -= costnum;
        if (NowSP >= 0)
        {
            return true;
        }
        return false;
    }

    public void AllSPObjDestroy()
    {
        var clones = GameObject.FindGameObjectsWithTag("SPTag");
        foreach (var clone in clones)
        {
            Destroy(clone);
        }
    }

    public void SetSummoningSicknessCharacterList(GameObject SetChar)
    {
        SummoningSicknessCharacterList.Add(SetChar);
    }

    public void SubtractionSummoningSicknessCharacterList()
    {
        for (int count = SummoningSicknessCharacterList.Count - 1; count >= 0; count--)
        {
            int pnum = SummoningSicknessCharacterList[count].GetComponent<CharacterStatus>().GetPlayerNumber();
            if (pnum == TurnPlayer)
            {
                SummoningSicknessCharacterList[count].GetComponent<CharacterStatus>().SubtractionSummoningSickness();
                int ssc = SummoningSicknessCharacterList[count].GetComponent<CharacterStatus>().GetSummoningSickness();

                if (ssc == 0)
                {
                    SummoningSicknessCharacterList.RemoveAt(count);
                }
            }
        }
    }

    public void TurnStart()

    {
        SubtractionSummoningSicknessCharacterList();
    }
  
    public void SPDestroyCall()
    {
        PlayerObj[TurnPlayer].GetComponent<SP>().DestroyList(CopyCost);
    }

   

    public void CharacterTurnStartSkill()
    {

    }

    public void MoveCountSubtraction()
    {
        MoveCount--;
        if(MoveCount<=0)
        {
            GetComponent<PhaseMaster>().NextFase();
            MoveCount = 2;
        //    SetTurnPlayer();
        }
    }

    void TurnEnd()
    {
        GetComponent<BoardList>().ClearMoveList();
    }
    public void SetStartAnimation()
    {

    }
}
