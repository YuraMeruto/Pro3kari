using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveData : MonoBehaviour
{
    [SerializeField]
    GameObject MoveAreaObj;
    [SerializeField]
    GameObject Master;
    private int[,] ReadMoveData = new int[10, 10];//読み込まれた

    private int CSVMyPositionX;    //読み込んだデータの自分のポジション
    private int CSVMyPositionZ;    //読み込んだデータの自分のポジション

    private int MoveDataMaxLengthSize;
    private int MoveDataMaxSideSize;
    private int MaxMassSize;
    private int MaxMassLength;
    private int Datalength;
    private int DataSide;
    //CSVの自分のポジションとの差分を計算
    private int DifferenceX;    //CSVにあるデータの中心のポジションとゲームのマップのポジションの差分
    private int DifferenceZ;    //CSVにあるデータの中心のポジションとゲームのマップのポジションの差分
    private int DifferenceXCopy;    //CSVにあるデータの中心のポジションとゲームのマップのポジションの差分
    private int DifferenceZCopy;    //CSVにあるデータの中心のポジションとゲームのマップのポジションの差分
    private int MassNumber;
    private int NowMyPosx = 0;
    private int NowMyPosz = 0;
    private int AbsPositionX;
    private int AbsPositionZ;
    private int ResultCalucationX;
    private int ResultCalucationZ;
    private List<int> IsPossibleAreaNumber = new List<int>();
    // Use this for initialization
    void Start()
    {
        Master = GameObject.Find("Master");
        MoveDataMaxLengthSize = Master.GetComponent<ReadCsv>().GetMaxLength();
        MoveDataMaxSideSize = Master.GetComponent<ReadCsv>().GetMaxSide();
        // MoveDataMaxLengthSize /= 2;
        //        MoveDataMaxSideSize /= 2;
        MaxMassLength = Master.GetComponent<BoardMaster>().GetMaxLength();
        MaxMassSize = Master.GetComponent<BoardMaster>().GetMaxSide();
        for (int z = 0; z <= MoveDataMaxLengthSize; z++)
        {
            for (int x = 0; x <= MoveDataMaxSideSize; x++)
            {
                ReadMoveData[z, x] = Master.GetComponent<ReadCsv>().InputMoveData(z, x);
                //  Debug.Log(ReadMoveData[z, x]);
                if (ReadMoveData[z, x] == 2)
                {
                    CSVMyPositionX = x;//CSVのいる中心の座標X
                    CSVMyPositionZ = z;//CSVのいる中心の座標Z
                }
            }
        }
    }

    /// <summary>
    /// 自分がどこのますいるかを検索
    /// </summary>
    /// <param name="num"></param>
    public void IsPossibleMove(int num)
    {
        MassNumber = num;
        int CopyMyPositionX = CSVMyPositionX;
        int CopyMyPositionZ = CSVMyPositionZ;
        //自分がどこのますにいるかをけんさく
        for (int length = 0; length < MaxMassLength; length++)
        {
            for (int side = 0; side < MaxMassSize; side++)
            {

                if (Master.GetComponent<BoardMaster>().MassNum[length, side] == num)
                {
                    GameObject ret;
                    ret = Master.GetComponent<BoardMaster>().GetCharObject(length, side);
                    if (ret == null)
                    {
                        break;
                    }
                    else
                    {
                        NowMyPosx = side;
                        NowMyPosz = length;
                        InstanceIsPossibleMoveArea();
                    }

                    //                    break;
                }
            }
        }
        //自分のいるポジションからMoveDataの自分のポジションとの差分を計算
        //        DifferencCalculation(NowMyPosx, NowMyPosz);
    }



    /// <summary>
    /// 移動範囲の表示及び生成
    /// </summary>
    public void InstanceIsPossibleMoveArea()
    {
        int KariZ = -MoveDataMaxLengthSize / 2;
        for (int length = 0; length <= MoveDataMaxLengthSize; length++)
        {
            int KariX = -MoveDataMaxSideSize / 2;
            for (int side = 0; side <= MoveDataMaxSideSize; side++)
            {
                if (ReadMoveData[length, side] == 1)
                {
                    bool IsOut = true;
                    IsOut = OutSideTheArea(NowMyPosz + KariZ, NowMyPosx + KariX);
                    if (IsOut)
                    {
                        /*
                        Debug.Log(CSVMyPositionZ);
                        Debug.Log(CSVMyPositionX);
                        Debug.Log(length);
                        Debug.Log(side);
                        */
                        bool ret = true;
                        int DifferencX = NowMyPosx - CSVMyPositionX;//自分のポジションとCSVのデータのポジションの差分
                        int DifferencZ = NowMyPosz - CSVMyPositionZ;
                        ret = Master.GetComponent<BoardMaster>().GetMassStatusNone(NowMyPosz + KariZ, NowMyPosx + KariX);
                        if (ret)//移動できるマスであれば
                        {
                            Vector3 InstancePos = Master.GetComponent<BoardMaster>().MassObj[NowMyPosz + KariZ, NowMyPosx + KariX].transform.position;
                            //Vector3 InstancePos = Master.GetComponent<BoardMaster>().MassObj[NowMyPosz, NowMyPosx].transform.position;
                            InstancePos.y = 1.0f;
                            Instantiate(MoveAreaObj, InstancePos, Quaternion.identity);
                            IsPossibleAreaNumber.Add(Master.GetComponent<BoardMaster>().MassNum[NowMyPosz + KariZ, NowMyPosx + KariX]);
                            
                        }
                    }
                }
                    KariX++;
                
            }
            KariZ++;
        }
    }
    bool OutSideTheArea(int InstanceLength,int InstanceSide)
    {
        bool ret = true;

        if(InstanceLength > MaxMassLength || InstanceSide > MaxMassSize)
        {
            ret = false;
        }

        else if(InstanceLength < 0 || InstanceSide < 0)
        {
            ret = false;
        }
        return ret;
    }
}
