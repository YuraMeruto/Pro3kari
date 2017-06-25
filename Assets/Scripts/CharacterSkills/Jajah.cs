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

    public override void MyTurnStart()
    {

    }

    public override void MyTurnEnd()
    {

    }

    public override void EnemyTurnStart()
    {
    }

    public override void EnemyTurnEnd()
    {

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
    }

    public override void EnemyMoveEnd()
    {
    }

    public override void MoveStart()
    {
        DestroySkill();

    }
    public override void MoveEnd()
    {
    }

    public void DestroySkill()
    {
        bool IsGetSkill = Parent.GetComponent<CharacterStatus>().GetIsSkill();
        if (!IsGetSkill)
        {
            return;
        }
        Debug.Log("ジャジャスキル発動");
        if (SkillCount <= 0)
        {
            Debug.Log("スキルが発動できません");
            return;
        }
      GameObject  targetobj=  PlayerObj.GetComponent<AtachMaster>().GetAttachEnemyObj();
       // bool ret = Master.GetComponent<BoardList>().GetSkillTargetList(targetobj);
//        Debug.Log("retの結果は"+ret);
        if(targetobj != null)
        {
            Debug.Log("ターゲットを消滅させた");
            Destroy(targetobj);
            PlayerObj.GetComponent<MouseState>().SetIsTargetBool(true);
            Master.GetComponent<BoardList>().ClearSkillTargetList();
            PlayerObj.GetComponent<BattleScene>().SetIsBattleStart(false);
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
