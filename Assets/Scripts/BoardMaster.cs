using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMaster : MonoBehaviour
{
    public GameObject DeckObj;

    [SerializeField]
    private List<GameObject> CharacterList = new List<GameObject>();
    private enum Phase { None, Main1, Move, Main2, TurnEnd };
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

    [SerializeField]
    GameObject CameraObj;
    private int CopyCost;
    private int MaxNumber;
    [SerializeField]
    private GameObject PlayerTurnUI;
    [SerializeField]
    private List<GameObject> SummoningSicknessCharacterList = new List<GameObject>();

    [SerializeField]
    private List<GameObject> SumonCharacters = new List<GameObject>();
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

    [SerializeField]
    private GameObject TimerObj;
    [SerializeField]
    private int IniDrawaCount;
    private GameObject[] DeckObjs = new GameObject[2];
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
                Mass.GetComponent<NumberMass>().SetXArryNumber(side);
                Mass.GetComponent<NumberMass>().SetYArryNumber(length);
                MassObj[length, side] = Mass;
                MassObj[length, side].GetComponent<NumberMass>().SetDefaltNumber(0);
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
        for (int x = 0; x < MaxLength; x++)
        {
            MassArea[0, x] = 1;
            MassObj[0, x].GetComponent<NumberMass>().SetPlayerNumber(1);
            MassObj[0, x].GetComponent<NumberMass>().SetDefaltNumber(1);
            MassArea[1, x] = 1;
            MassObj[1, x].GetComponent<NumberMass>().SetPlayerNumber(1);
            MassObj[1, x].GetComponent<NumberMass>().SetDefaltNumber(1);
            MassArea[4, x] = 2;
            MassObj[4, x].GetComponent<NumberMass>().SetPlayerNumber(2);
            MassObj[4, x].GetComponent<NumberMass>().SetDefaltNumber(2);
            MassArea[5, x] = 2;
            MassObj[5, x].GetComponent<NumberMass>().SetPlayerNumber(2);
            MassObj[5, x].GetComponent<NumberMass>().SetDefaltNumber(2);
        }
        MaxNumber = count;
        //デッキの生成開始   
        DeckPos = MassObj[0, 0].transform.position;
        DeckPos.x = DeckPos.x - 3;
        GameObject Deck1 = Instantiate(DeckObj, DeckPos, DeckObj.transform.rotation);
        PlayerNumber = 1;
        Deck1.GetComponent<SummonsDeck>().SetPlayerNumber(PlayerNumber);
        DeckObjs[0] = Deck1;
        DeckPos = MassObj[5, 5].transform.position;
        DeckPos.x = DeckPos.x + 3;
        GameObject Deck2 = Instantiate(DeckObj, DeckPos, DeckObj.transform.rotation);
        PlayerNumber = 2;
        Deck2.GetComponent<SummonsDeck>().SetPlayerNumber(PlayerNumber);
        DeckObjs[1] = Deck2;
        //デッキ生成終了

        //SP設定
        PlayerObj[TurnPlayer].GetComponent<SP>().InstanceSPObj();
        IniStartSetting();
    }
    void IniStartSetting()
    { 
        SetTurnPlayer();
    }
    public void IniDrawCard(int playernum)
    {

        for (int count = 0; count < IniDrawaCount; count++)
        {
            GameObject drawobj = DeckObjs[playernum - 1].GetComponent<SummonsDeck>().GetReadDeckDataObjList();
            PlayerObj[playernum].GetComponent<YourCard>().SetDrawCards(drawobj);
        }
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
                    GetComponent<BoardSkillList>().SetPosLength(length);
                    GetComponent<BoardSkillList>().SetPosSide(side);
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
    public GameObject GetMass(int num)
    {
        GameObject targetmass = null;
        for (int length = 0; length < MaxLength; length++)
        {
            for (int side = 0; side < MaxSide; side++)
            {
                if (MassNum[length, side] == num)
                {
                    targetmass = MassObj[length, side];
                }

            }
        }
        return targetmass;

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

        return num;
    }


    /// <summary>
    /// デッキから選択されたカードをタッチして召喚できる場所を表示させる
    /// </summary>
    /// <param name="num"></param>
    public void SummonsFiledPos(int num)
    {
        Debug.Log(num+"です");
        int MaxLMap = GetComponent<SummonsPosData>().GetMaxLength();
        int MaxSMap = GetComponent<SummonsPosData>().GetMaxSide();
        int instancecount = 0;
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
                            instancecount++;
                        }
                    }
                }
            }
        }
        if (instancecount == 0)
            CameraObj.GetComponent<AtachMaster>().SetChoosingCardSummonObj(null);
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
        TurnStartSkills();
        SubtractionSummoningSicknessCharacterList();
        GetComponent<IsAttachObj>().SetIsAtachObj(false);
        PlayerObj[TurnPlayer].GetComponent<YourCard>().SetIsDrawCard(true);
        TimerObj.GetComponent<PlayerTime>().SetIs_StopTimer(false);
    }

    public void SPDestroyCall()
    {
        PlayerObj[TurnPlayer].GetComponent<SP>().DestroyList(CopyCost);
    }


    public void MoveCountSubtraction()
    {
        MoveCount--;
        if (MoveCount <= 0)
        {
            GetComponent<PhaseMaster>().NextFase();
            MoveCount = 2;
        }
    }
    
    void TurnEnd()
    {
        TurnEndSkills();
        GetComponent<BoardList>().ClearMoveList();
        TimerObj.GetComponent<PlayerTime>().SetIs_StopTimer(true);
        TimerObj.GetComponent<PlayerTime>().SetTimer();
        
    }

    public void SetSummonCharacters(GameObject SetObj)
    {
        SumonCharacters.Add(SetObj);
    }
    void TurnStartSkills()
    {
        for (int count = 0; count <= SumonCharacters.Count - 1; count++)
        {
            int PlayerNumber = SumonCharacters[count].GetComponent<CharacterStatus>().GetPlayerNumber();
            if (TurnPlayer == PlayerNumber)
            {
                SumonCharacters[count].GetComponent<CharacterStatus>().skill.MyTurnStart();
            }
            else
            {
                SumonCharacters[count].GetComponent<CharacterStatus>().skill.EnemyTurnStart();
            }
        }
    }
    void TurnEndSkills()
    {
        for (int count = 0; count <= SumonCharacters.Count - 1; count++)
        {
            int PlayerNumber = SumonCharacters[count].GetComponent<CharacterStatus>().GetPlayerNumber();
            if (TurnPlayer == PlayerNumber)
            {
                SumonCharacters[count].GetComponent<CharacterStatus>().skill.MyTurnEnd();
            }
            else
            {
                SumonCharacters[count].GetComponent<CharacterStatus>().skill.EnemyTurnEnd();
            }
        }

    }
    //聖女のスキルに使用
    public void SaintSkill(int posy, int posx, int damage)
    {
        CharObj[posy, posx].GetComponent<CharacterStatus>().SetDamage(damage);
    }

    public int GetPlayerArea(int length, int side)
    {
        int number = MassObj[length, side].GetComponent<NumberMass>().GetPlayerNumber();
        return number;
    }

    //フォーマルハウト専用のスキル
    public void SetMassObjAreaChange(ref int lenght, ref int side, GameObject ControlObj)
    {
        int num = MassObj[lenght, side].GetComponent<NumberMass>().GetDefaltNumber();
        switch (TurnPlayer)
        {
            case 1:
                if (num == 2)
                {
                    break;
                }
                else
                {
                    MassObj[lenght, side].GetComponent<NumberMass>().SetMaterialNumber(TurnPlayer);
                    MassObj[lenght, side].GetComponent<NumberMass>().SetControlObj(ControlObj);
                }
                break;

            case 2:
                if (num == 1)
                {
                    break;
                }
                else
                {
                    MassObj[lenght, side].GetComponent<NumberMass>().SetMaterialNumber(TurnPlayer);
                    MassObj[lenght, side].GetComponent<NumberMass>().SetControlObj(ControlObj);
                }

                break;
        }

    }
    public void ControlObjRelease(GameObject obj)
    {
        Debug.Log(obj);
        for (int length = 0; length < MaxLength; length++)
        {
            for (int side = 0; side < MaxSide; side++)
            {
                GameObject resultobj = MassObj[length, side].GetComponent<NumberMass>().GetControlObj();
                if (resultobj == obj)
                {
                    MassObj[length, side].GetComponent<NumberMass>().SetControlObj(null);
                    MassObj[length, side].GetComponent<NumberMass>().SetMaterialDefalt();
                }
            }
        }
    }


    /// <summary>
    /// 最初にキングを召喚する
    /// </summary>
    /// <param name="playernumber"></param>
    public void InstanceKing(int playernumber, int directionarynum)
    {
        int MaxLMap = GetComponent<SummonsPosData>().GetMaxLength();
        int MaxSMap = GetComponent<SummonsPosData>().GetMaxSide();
        for (int length = 0; length <= MaxLMap; length++)
        {
            for (int side = 0; side <= MaxSMap; side++)
            {
                if (FieldSummonData[length, side] == 6)
                    {
                    if (MassArea[length, side] == playernumber)
                    {
                        if (CharObj[length, side] == null)
                        {
                            Vector3 Pos = MassObj[length, side].transform.position;
                            GameObject KingObj = GetComponent<CharacterMaster>().GetSummonsCharacter(directionarynum);
                            IsMoveMassObj[length, side] = true;
                            Pos.z += 1;
                            GameObject InstanceKing = Instantiate(KingObj, Pos, Quaternion.identity);
                            InstanceKing.GetComponent<MoveData>().ReadSetObj(InstanceKing);
                            InstanceKing.GetComponent<ReadCsv>().SetTargetObj(InstanceKing);
                            InstanceKing.GetComponent<MoveData>().IniSet();
                            CharObj[length, side] = InstanceKing;
                            MassStatus[length, side] = Status.IsMoveArea;
                            if (length == 0)
                                InstanceKing.GetComponent<CharacterStatus>().SetPlayerNumber(1);
                            else
                                InstanceKing.GetComponent<CharacterStatus>().SetPlayerNumber(2);

                        }
                    }
                }
            }
        }

    }

    /// <summary>
    /// サルードのスキルで使用相手の駒を自分の陣地に移動させる
    /// </summary>
    /// <param name="pos"></param>
    public void SarudoSkillPos(Vector3 pos, GameObject obj)
    {
        pos.z = 10;
        SetNullChar(obj);
        for (int length = 0; length < MaxLength; length++)
        {
            for (int side = 0; side < MaxSide; side++)
            {
                if (pos == MassObj[length, side].transform.position)
                {
                    obj.transform.position = MassObj[length, side].transform.position;
                    CharObj[length, side] = obj;
                    break;
                }

            }

        }
    }

    public void SetNullChar(GameObject target)
    {
        for (int length = 0; length < MaxLength; length++)
        {
            for (int side = 0; side < MaxSide; side++)
            {
                if (target == CharObj[length,side])
                {
                    CharObj[length, side] = null;
                    break;
                }
            }

        }
    }

    public bool  UseAp(int playernum,int useapnum)
    {
        bool ret;
      ret = PlayerObj[playernum].GetComponent<AP>().UseAp(useapnum);
        return ret;
    }

    public void SetSp(int playenum,int cost)
    {
        PlayerObj[playenum].GetComponent<SP>().DestroyList(cost);
    }

    public bool Is_UseSP(int costnum)
    {
        CopyCost = costnum;
        int NowSP = PlayerObj[TurnPlayer].GetComponent<SP>().GetSP();
        NowSP -= costnum;
        if (NowSP -costnum <= 0)
        {
            return false;
        }
        return true;
    }

    public bool IsDrawCard()
    {
       bool ret = PlayerObj[TurnPlayer].GetComponent<YourCard>().GetIsDrawCard();
        return ret;
    }

    public void AddDrawcard(GameObject drawobj)
    {
        PlayerObj[TurnPlayer].GetComponent<YourCard>().SetDrawCards(drawobj);
        PlayerObj[TurnPlayer].GetComponent<YourCard>().SetIsDrawCard(false);
        GetComponent<IsAttachObj>().SetIsAtachObj(true);
    }
    public void PopYourCard(GameObject pop_obj)
    {
        Debug.Log(TurnPlayer);
        PlayerObj[TurnPlayer].GetComponent<YourCard>().PopCard(pop_obj);
    }

}
