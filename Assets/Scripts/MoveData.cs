using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveData : MonoBehaviour
{

    [SerializeField]
    GameObject Master;
    private int[,] ReadMoveData = new int[10, 10];//読み込まれた

    private int MyPositionX;    //読み込んだデータの自分のポジション
    private int MyPositionZ;    //読み込んだデータの自分のポジション
                                //CSVの自分のポジションとの差分を計算
    private int DifferenceX;    //CSVにあるデータの中心のポジションとゲームのマップのポジションの差分
    private int DifferenceZ;    //CSVにあるデータの中心のポジションとゲームのマップのポジションの差分
    private int DifferenceXCopy;    //CSVにあるデータの中心のポジションとゲームのマップのポジションの差分
    private int DifferenceZCopy;    //CSVにあるデータの中心のポジションとゲームのマップのポジションの差分
    private int MassNumber;
    // Use this for initialization
    void Start()
    {
        Master = GameObject.Find("Master");
        for (int x = 0; x < 10; x++)
        {
            for (int z = 0; z < 10; z++)
            {
                ReadMoveData[x, z] = Master.GetComponent<ReadCsv>().InputMoveData(x, z);
                if (ReadMoveData[x, z] == 2)
                {
                    MyPositionX = x;
                    MyPositionZ = z;
                }
            }
        }
    }


    public void IsPossibleMove(int num)
    {
        MassNumber = num;
        int NowMyPosx = 0;
        int NowMyPosz = 0;
        int CopyMyPositionX = MyPositionX;
        int CopyMyPositionZ = MyPositionZ;
        //自分がどこのますにいるかをけんさく
        for (int length = 0; length <= 10; length++)
        {
            for (int side = 0; side <= 10; side++)
            {
                if (GetComponent<BoardMaster>().MassNum[side, length] == num)
                {
                    NowMyPosx = side;
                    NowMyPosz = length;
                    break;
                }
            }
        }
        //自分のいるポジションからMoveDataの自分のポジションとの差分を計算
        DifferencCalculation(NowMyPosx, NowMyPosz);
    }


    public void DifferencCalculation(int nowposx, int nowposz)
    {
        if (MyPositionX >= nowposx)
        {
            DifferenceX = MyPositionX - nowposx;
        }

        else if (MyPositionX <= nowposx)
        {
            DifferenceX = nowposx - MyPositionX;
        }

        if (MyPositionZ >= nowposz)
        {
            DifferenceZ = MyPositionZ - nowposz;
        }

        else if (MyPositionZ <= nowposz)
        {
            DifferenceX = nowposz - MyPositionZ;
        }
        DifferenceXCopy = DifferenceX;
        DifferenceZCopy = DifferenceZ;
    }

    /// <summary>
    /// 移動範囲の表示及び生成
    /// </summary>
    public void InstanceIsPossibleMoveArea()
    {
        for(int side = 0;side <=10 ; side++)
        {
            for(int length = 0;length <= 10;length++)
            {
               int AbsPositionX = Mathf.Abs(MyPositionX - side);
                int AbsPositionZ = Mathf.Abs(MyPositionZ - length);
                if (ReadMoveData[AbsPositionX,AbsPositionZ] == 1 && Master.GetComponent<BoardMaster>().GetMassStatus(MassNumber))
                {

                }
            }
        }
    }
}
