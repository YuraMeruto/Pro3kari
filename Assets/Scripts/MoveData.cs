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
    private int PlayerNumber;//プレイヤー所属ナンバー
    [SerializeField]
    private int MoveDataMaxLengthSize;
    [SerializeField]
    private int MoveDataMaxSideSize;
    [SerializeField]
    private int MaxMassSize;
    [SerializeField]
    private int MaxMassLength;
    //CSVの自分のポジションとの差分を計算
    private int MassNumber;
    private int NowMyPosx = 0;
    private int NowMyPosz = 0;
    // Use this for initialization
    void Start()
    {

        Master = GameObject.Find("Master");
        MoveDataMaxLengthSize = GetComponent<ReadCsv>().GetMaxLength();
        MoveDataMaxSideSize = GetComponent<ReadCsv>().GetMaxSide();

        MaxMassLength = Master.GetComponent<BoardMaster>().GetMaxLength();
        MaxMassSize = Master.GetComponent<BoardMaster>().GetMaxSide();


        for (int z = 0; z <= MoveDataMaxLengthSize; z++)
        {
            for (int x = 0; x <= MoveDataMaxSideSize; x++)
            {
                ReadMoveData[z, x] = GetComponent<ReadCsv>().InputMoveData(z, x);
                if (ReadMoveData[z, x] == 2)
                {
                    CSVMyPositionX = x;//CSVのいる中心の座標X
                    CSVMyPositionZ = z;//CSVのいる中心の座標Z
                }
            }
        }
    }
    void Update()
    {
        if(Master == null)
        {
            Debug.Log("NULLになったｙ");
        }
    }
    /// <summary>
    /// 自分がどこのますいるかを検索
    /// </summary>
    /// <param name="num"></param>
    public void IsPossibleMoveMove(int num)
    {
        Master = GameObject.Find("Master");
        Debug.Log(Master);
        MassNumber = num;
          //自分がどこのますにいるかをけんさく
          for (int length = 0; length < MaxMassLength; length++)
          {
              for (int side = 0; side < MaxMassSize; side++)
              {
                  Debug.Log("aa");

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
                  }
              }
          }
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
                    Debug.Log(IsOut);
                    if (IsOut)
                    {

                        bool ret = true;
                        ret = Master.GetComponent<BoardMaster>().GetMassStatusNone(NowMyPosz + KariZ, NowMyPosx + KariX);
                        Debug.Log(ret);
                        GameObject IsCharObj = Master.GetComponent<BoardMaster>().GetCharObject(NowMyPosz + KariZ, NowMyPosx + KariX);
                        if (IsCharObj != null)
                        {
                            int IsCharNumber = IsCharObj.GetComponent<CharacterStatus>().GetPlayerNumber();
                            int MyNumber = this.gameObject.GetComponent<CharacterStatus>().GetPlayerNumber();
                            if (MyNumber == IsCharNumber)
                            {
                                ret = false;
                            }
                        }
                        if (ret)//移動できるマスであれば
                        {
                            Vector3 InstancePos = Master.GetComponent<BoardMaster>().MassObj[NowMyPosz + KariZ, NowMyPosx + KariX].transform.position;
                            InstancePos.z = 1.0f;
                            Debug.Log("生成したよ");
                            GameObject IsMoveObj = Instantiate(MoveAreaObj, InstancePos, Quaternion.identity) as GameObject;
                            IsMoveObj.tag = "IsMovetag";
                            Master.GetComponent<BoardMaster>().SetIsMove(NowMyPosz + KariZ, NowMyPosx + KariX, true);
                        }
                    }
                }
                KariX++;
            }
            KariZ++;
        }
    }
    public bool OutSideTheArea(int InstanceLength, int InstanceSide)
    {
        bool ret = true;

        if (InstanceLength >= MaxMassLength || InstanceSide >= MaxMassSize)
        {
            ret = false;
        }

        else if (InstanceLength < 0 || InstanceSide < 0)
        {
            ret = false;
        }
        return ret;
    }



}
