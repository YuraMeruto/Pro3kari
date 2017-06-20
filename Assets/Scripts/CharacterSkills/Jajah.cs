using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jajah : SkillBase
{
    [SerializeField]
    private int SkillCount = 1;
    [SerializeField]
    private GameObject SkillInstanceObj;
    [SerializeField]
    private GameObject Master;
    private GameObject CameraObj;
    private GameObject Parent;
    [SerializeField]
    private GameObject PlayerObj;
    void Start()
    {
        Parent = gameObject.transform.parent.gameObject;
        Master = GameObject.Find("Master");
        PlayerObj = GameObject.Find("Main Camera");
    }
    private int NowMyPosLength;
    private int NowMyPosSide;
    private int MaxLength;
    private int MaxSide;
    [SerializeField]
    GameObject IsEnemyObj;


    [SerializeField]
    private GameObject MoveAreaObj;

    public override bool Is_AtTheStart()
    {
        return false;
    }
    public override bool Is_AtTheEnd()
    {
        return false;
    }
    public override bool Is_BattleStart()
    {
        return true;
    }

    public override bool Is_BattleEnd()
    {
        return false;
    }
    public override bool Is_MoveStart()
    {
        return true;
    }

    public override bool Is_MoveEnd()
    {
        return false;
    }
    public override void AtTheStart()
    {
        DestroySkill();
    }
    public override void AtTheEnd()
    {
        DestroySkill();

    }
    public override void BattleStart()
    {
        DestroySkill();

    }
    public override void BattleEnd()
    {
        //      DestroySkill();

    }

    public override void MoveStart()
    {
        DestroySkill();

    }
    public override void MoveEnd()
    {
        // DestroySkill();
    }

    public void DestroySkill()
    {
        Debug.Log("ジャジャスキル発動");
        if (SkillCount <= 0)
        {
            return;
        }
      GameObject  targetobj=  PlayerObj.GetComponent<AtachMaster>().GetAttachEnemyObj();
        bool ret = Master.GetComponent<BoardList>().GetSkillTargetList(targetobj);
        if(ret)
        {
            Destroy(targetobj);
            PlayerObj.GetComponent<MouseState>().SetIsTargetBool(true);
            Master.GetComponent<BoardList>().ClearSkillTargetList();
        }
        /*
                NowMyPos();
                int MaxMoveDataLength = Parent.GetComponent<MoveData>().MoveDataMaxLengthSize;
                int MaxMoveDataSide = Parent.GetComponent<MoveData>().MoveDataMaxSideSize;
                int Kariz = -MaxMoveDataLength / 2;
                for (int length = 0; length < MaxLength; length++)
                {
                    int KariX = -MaxMoveDataSide / 2;
                    for (int side = 0; side < MaxSide; side++)
                    {
                        int num = Parent.GetComponent<MoveData>().GetReadMoveData(length, side);

                        if (num == 3)
                        {
                            int sumlength = NowMyPosLength + Kariz;
                            int sumside = NowMyPosSide + KariX;
                            bool IsOut = true;
                            IsOut = OutSideTheArea(sumlength, sumside);
                            if (IsOut)
                            {
                                 IsEnemyObj = Master.GetComponent<BoardMaster>().GetCharObject(sumlength,sumside);
                                if(IsEnemyObj != null)
                                {
                                    int IsEnemyPlayerNumber = IsEnemyObj.GetComponent<CharacterStatus>().GetPlayerNumber();
                                    int MyNumber = Parent.GetComponent<CharacterStatus>().GetPlayerNumber();
                                    if(IsEnemyPlayerNumber != MyNumber)
                                    {
                                        Master.GetComponent<BoardList>().SetSkillList(IsEnemyObj);
                                        Vector3 InstancePos = Master.GetComponent<BoardMaster>().MassObj[sumlength,sumside].transform.position;
                                        InstancePos.z = 1.0f;
                                        GameObject IsMoveObj = Instantiate(SkillInstanceObj, InstancePos, Quaternion.identity) as GameObject;
                                        IsMoveObj.tag = "IsMovetag";
                                        Master.GetComponent<BoardMaster>().SetIsMove(sumlength,sumside, true);
                                    }
                                }
                            }
                        }

                    }
                }
                SkillCount--;
            }

            public void NowMyPos()
            {
                GameObject Massobj = PlayerObj.GetComponent<AtachMaster>().GetAttachMassObj();
                int Massnum = Massobj.GetComponent<NumberMass>().GetNumber();
                MaxLength = Parent.GetComponent<MoveData>().GetMaxLengthMove();
                MaxSide = Parent.GetComponent<MoveData>().GetMaxSideMove();
                int num;
                //        int KariZ = 
                for (int length = 0; length < MaxLength; length++)
                {
                    for (int side = 0; side < MaxSide; side++)
                    {
                        if (Master.GetComponent<BoardMaster>().MassNum[length, side] == Massnum)
                        {
                            NowMyPosLength = length;
                            NowMyPosSide = side;
                            break;
                        }

                    }
                }

            }

            public bool OutSideTheArea(int InstanceLength, int InstanceSide)//マス外であるかの判定
            {
                bool ret = true;
                int MaxMassLength = Master.GetComponent<BoardMaster>().GetMaxLength();
                int MaxMassSide = Master.GetComponent<BoardMaster>().GetMaxSide();
                if (InstanceLength >= MaxMassLength || InstanceSide >= MaxMassSide)
                {
                    ret = false;
                }

                else if (InstanceLength < 0 || InstanceSide < 0)
                {
                    ret = false;
                }
                return ret;
            }
            */



    }
}
