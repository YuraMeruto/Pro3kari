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
    public int MoveDataMaxLengthSize;//読み込まれたデータの縦の最大値の記録
    [SerializeField]
    public int MoveDataMaxSideSize;//読み込まれたデータの横の最大値の記録
    [SerializeField]
    private int MaxMassSize;
    [SerializeField]
    private int MaxMassLength;
    //CSVの自分のポジションとの差分を計算
    private int MassNumber;
    private int NowMyPosx = 0;
    private int NowMyPosz = 0;
    private GameObject ReadObj;//Readcsvを呼び出すため仮でやっています。
    [SerializeField]
    private string FileName;

    public void IniSet()
    {
        Master = GameObject.Find("Master");
        ReadObj.GetComponent<ReadCsv>().SetFileName(this.gameObject, FileName);
        //MoveDataMaxLengthSize = ReadObj.GetComponent<ReadCsv>().MaxLengthSize;
        //MoveDataMaxSideSize = ReadObj.GetComponent<ReadCsv>().MaxSideSize;
        MaxMassLength = Master.GetComponent<BoardMaster>().GetMaxLength();
        MaxMassSize = Master.GetComponent<BoardMaster>().GetMaxSide();
        for (int z = 0; z <= MoveDataMaxLengthSize; z++)
        {
            for (int x = 0; x <= MoveDataMaxSideSize; x++)
            {
                ReadMoveData[z, x] = ReadObj.GetComponent<ReadCsv>().InputMoveData(z, x);
                //                Debug.Log(ReadMoveData[z, x]);
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
       
        //自分がどこのますにいるかをけんさく
        for (int length = 0; length < MaxMassLength; length++)
        {
            for (int side = 0; side < MaxMassSize; side++)
            {

                if (Master.GetComponent<BoardMaster>().MassNum[length, side] == num)
                {

                    GameObject ret;
                    ret = Master.GetComponent<BoardMaster>().GetCharObject(length, side);
                    int retracenum = ret.GetComponent<CharacterStatus>().GetRace();
                    if (ret == null)
                    {
                        break;
                    }
                    else
                    {
                        int playernum = ret.GetComponent<CharacterStatus>().GetPlayerNumber();
                        NowMyPosx = side;//自分のマスのポジションの各位置を記録
                        NowMyPosz = length;//自分のマスのポジションの各位置を記録
                        //                        if (retracenum == 1)
                        //                        {
                        //                            PornIsPossible();
                        //                            break;
                        //                       }
                        if (playernum == 2)
                        {
                            InstanceIsPossibleMoveArea();
                        }

                        else if (playernum == 1)
                        {
                            InstanceIsPossibleMoveAreaInverted();

                        }
                    }
                }
            }
        }

    }
    public void InstanceIsPossibleMoveAreaInverted()//プレイヤー１の移動範囲の生成の仕方
    {
        int KariZ = -MoveDataMaxLengthSize/2;
        for (int length = MoveDataMaxLengthSize; length >= 0; length--)
        {
            int KariX = -MoveDataMaxSideSize / 2;
            for (int side = MoveDataMaxSideSize; side >= 0; side--)
            {
                int karidata = ReadObj.GetComponent<MoveData>().GetReadMoveData(length, side);
                if (karidata == 1)
                {
                    int resultz = NowMyPosz + KariZ;
                    int resultx = NowMyPosx + KariX;
                    bool IsOut = true;
                    IsOut = OutSideTheArea(resultz, resultx);
                
                    if (IsOut)
                    {
                        bool ret = true;
                        ret = Master.GetComponent<BoardMaster>().GetMassStatusNone(resultz, resultx);
                        GameObject IsCharObj = Master.GetComponent<BoardMaster>().GetCharObject(resultz, resultx);
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
                            Vector3 InstancePos = Master.GetComponent<BoardMaster>().MassObj[resultz, resultx].transform.position;
                            InstancePos.z = 1.0f;
                            GameObject IsMoveObj = Instantiate(MoveAreaObj, InstancePos, Quaternion.identity) as GameObject;
                            IsMoveObj.tag = "IsMovetag";
                            Master.GetComponent<BoardMaster>().SetIsMove(resultz, resultx, true);
                        }
                    }
                }


                else if (karidata == 3)//ポーンの時だけ
                {
                    bool IsOut = true;
                    IsOut = OutSideTheArea(NowMyPosz + KariZ, NowMyPosx + KariX);
                    Debug.Log(IsOut);
                    if (IsOut)
                    {

                        GameObject IsCharObj = Master.GetComponent<BoardMaster>().GetCharObject(NowMyPosz + KariZ, NowMyPosx + KariX);

                        if (IsCharObj != null)
                        {
                            int IsCharNumber = IsCharObj.GetComponent<CharacterStatus>().GetPlayerNumber();
                            int MyNumber = this.gameObject.GetComponent<CharacterStatus>().GetPlayerNumber();
                            if (MyNumber != IsCharNumber)
                            {
                                Vector3 InstancePos = Master.GetComponent<BoardMaster>().MassObj[NowMyPosz + KariZ, NowMyPosx + KariX].transform.position;
                                InstancePos.z = 1.0f;
                                GameObject IsMoveObj = Instantiate(MoveAreaObj, InstancePos, Quaternion.identity) as GameObject;
                                IsMoveObj.tag = "IsMovetag";
                                Master.GetComponent<BoardMaster>().SetIsMove(NowMyPosz + KariZ, NowMyPosx + KariX, true);
                            }
                        }
                    }

                }
                KariX++;
            }
            KariZ++;
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
                int karidata = ReadObj.GetComponent<MoveData>().GetReadMoveData(length, side);
                if (karidata == 1)
                {
                    bool IsOut = true;
                    IsOut = OutSideTheArea(NowMyPosz + KariZ, NowMyPosx + KariX);
                    Debug.Log(IsOut);
                    if (IsOut)
                    {
                        bool ret = true;
                        ret = Master.GetComponent<BoardMaster>().GetMassStatusNone(NowMyPosz + KariZ, NowMyPosx + KariX);
                        GameObject IsCharObj = Master.GetComponent<BoardMaster>().GetCharObject(NowMyPosz + KariZ, NowMyPosx + KariX);
                        if (IsCharObj != null)
                        {
                            int IsCharNumber = IsCharObj.GetComponent<CharacterStatus>().GetPlayerNumber();
                            int MyNumber = this.gameObject.GetComponent<CharacterStatus>().GetPlayerNumber();
                            if (MyNumber == IsCharNumber)
                            {
                                ret = false;
                            }
                            int myrace = GetComponent<CharacterStatus>().GetRace();
                            if(myrace == 1)
                                {
                                ret = false;
                            }

                        }
                        if (ret)//移動できるマスであれば
                        {
                            Vector3 InstancePos = Master.GetComponent<BoardMaster>().MassObj[NowMyPosz + KariZ, NowMyPosx + KariX].transform.position;
                            InstancePos.z = 1.0f;
                            GameObject IsMoveObj = Instantiate(MoveAreaObj, InstancePos, Quaternion.identity) as GameObject;
                            IsMoveObj.tag = "IsMovetag";
                            Master.GetComponent<BoardMaster>().SetIsMove(NowMyPosz + KariZ, NowMyPosx + KariX, true);
                        }
                    }
                }


                else if (karidata == 3)//ポーンの時だけ
                {
                    bool IsOut = true;
                    IsOut = OutSideTheArea(NowMyPosz + KariZ, NowMyPosx + KariX);
                    Debug.Log(IsOut);
                    if (IsOut)
                    {

                        GameObject IsCharObj = Master.GetComponent<BoardMaster>().GetCharObject(NowMyPosz + KariZ, NowMyPosx + KariX);

                        if (IsCharObj != null)
                        {
                            int IsCharNumber = IsCharObj.GetComponent<CharacterStatus>().GetPlayerNumber();
                            int MyNumber = this.gameObject.GetComponent<CharacterStatus>().GetPlayerNumber();
                            if (MyNumber != IsCharNumber)
                            {
                                Vector3 InstancePos = Master.GetComponent<BoardMaster>().MassObj[NowMyPosz + KariZ, NowMyPosx + KariX].transform.position;
                                InstancePos.z = 1.0f;
                                GameObject IsMoveObj = Instantiate(MoveAreaObj, InstancePos, Quaternion.identity) as GameObject;
                                IsMoveObj.tag = "IsMovetag";
                                Master.GetComponent<BoardMaster>().SetIsMove(NowMyPosz + KariZ, NowMyPosx + KariX, true);
                            }
                        }
                        //                        if (ret)//移動できるマスであれば
                        //                        {
                        // Vector3 InstancePos = Master.GetComponent<BoardMaster>().MassObj[NowMyPosz + KariZ, NowMyPosx + KariX].transform.position;
                        // InstancePos.z = 1.0f;
                        // GameObject IsMoveObj = Instantiate(MoveAreaObj, InstancePos, Quaternion.identity) as GameObject;
                        // IsMoveObj.tag = "IsMovetag";
                        // Master.GetComponent<BoardMaster>().SetIsMove(NowMyPosz + KariZ, NowMyPosx + KariX, true);
                        //                      }
                    }

                }
                KariX++;
            }
            KariZ++;
        }
    }
    public bool OutSideTheArea(int InstanceLength, int InstanceSide)//マス外であるかの判定
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

    public void ReadSetObj(GameObject obj)
    {
        ReadObj = obj;
    }

    public int GetReadMoveData(int length, int side)
    {
        return ReadMoveData[length, side];
    }


    public void SetMoveData()//ポーン専用
    {
        Debug.Log("変更");
        ReadMoveData[0, 1] = 0;
    }


}

