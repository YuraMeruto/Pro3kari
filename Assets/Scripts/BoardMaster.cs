using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMaster : MonoBehaviour
{

    public GameObject[,] MassObj = new GameObject[10, 10];
    public bool[,] IsMoveMassObj = new bool[10, 10];
    private GameObject[,] CharObj = new GameObject[10, 10];
    public int[,] MassNum = new int[10, 10];
    public enum Status { None, OK, NG,IsMoveArea };
    public Status[,] MassStatus = new Status[10, 10];
    private int PlayerNumber;
    [SerializeField]
    private int MaxLength;
    [SerializeField]
    private int MaxSide;

    private int MaxNumber;
    //マスのオブジェクト
    [SerializeField]
    private GameObject[] ObjMass = new GameObject[2];

    //キャラクターのオブジェクト
    [SerializeField]
    private GameObject[] ObjCharcter = new GameObject[2];

    private Vector3 InstancePos = Vector3.zero;

    [SerializeField]
    private LayerMask MassLayer;
    // Use this for initialization
    void Start()
    {
        int count = 0;
        int masscolor = 0;
        for (int length = 0; length < MaxLength; length++)
        {
            for (int side = 0; side < MaxSide; side++)
            {
                InstancePos.x = side;
                InstancePos.z = length;
                if (length == 0 && side == 0)
                    masscolor = 1;
                GameObject Mass = Instantiate(ObjMass[masscolor], InstancePos, Quaternion.identity) as GameObject;
                Mass.layer = 8;
                Mass.name = count.ToString();
                IsMoveMassObj[length, side] = false;
                Mass.GetComponent<NumberMass>().SetNumber(count);
                MassObj[length, side] = Mass;
                MassNum[length, side] = count;
                MassStatus[length, side] = Status.None;
              //  if (length == 2)//ポーンの初期の生成場所
              if(length == 1 && side ==1)
                {
                    PlayerNumber = 1;
                    Vector3 CharacterInstancePos = InstancePos;
                    CharacterInstancePos.y = 1;
                    GameObject Charcter = Instantiate(ObjCharcter[0], CharacterInstancePos, Quaternion.identity) as GameObject;
                    Charcter.GetComponent<PlayerNumber>().SetPlayerNumber(PlayerNumber);
                    CharObj[length, side] = Charcter;
                    MassStatus[length, side] = Status.NG;
                }
                count++;
                if (masscolor == 0)
                {
                    masscolor = 1;
                }
                else
                {
                    masscolor = 0;
                }

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
        MaxNumber = count;
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
                if(MassNum[length,side] == num)
                {
                    MassStatus[length, side] = Status.IsMoveArea;
                }
            }
        }   
    }
    public void SetStatusIsMoveArea(int length,int side)
    {
        MassStatus[length, side] = Status.IsMoveArea;
    }

    
    public bool GetIsMove(int num)
    {
        for (int length = 0; length < MaxLength; length++)
        {
            for (int side = 0; side < MaxSide; side++)
            {
                if(MassNum[length,side]==num)
                return IsMoveMassObj[length, side];
            }
        }
        return false;
    }

    public
}
