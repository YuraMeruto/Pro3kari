using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardList : MonoBehaviour
{

    public List<GameObject> SkillList = new List<GameObject>();
    public List<GameObject> SkillTargetList = new List<GameObject>();
    [SerializeField]
    public List<GameObject> MoveList = new List<GameObject>();
    [SerializeField]
    private List<GameObject> RootMoveMass;
    // Update is called once per frame
    public void SetSkillList(GameObject setobj)
    {
        SkillList.Add(setobj);
    }

    public bool GetList(GameObject getobj)
    {
        for (int count = SkillList.Count - 1; count > 0; count--)
        {
            if (getobj == SkillList[count])
            {
                return true;
            }
        }
        return false;
    }

    public void SetSkillTargetList(GameObject setobj)
    {
        SkillTargetList.Add(setobj);
    }

    public void ClearSkillTargetList()
    {
        SkillTargetList.Clear();
    }

    public bool GetSkillTargetList(GameObject target)
    {
        for (int count = SkillTargetList.Count - 1; count >= 0; count--)
        {
            if (SkillTargetList[count] == target)
            {
                return true;
            }
        }
        return false;
    }

    public void SetMoveList(GameObject setobj)
    {
        MoveList.Add(setobj);
    }

    public bool GetIsMoveList(GameObject target)
    {
        for (int count = MoveList.Count - 1; count >= 0; count--)
        {
            if (MoveList[count] == target)
            {
                return true;
            }
        }
        return false;
    }
    public void RemoveMoveList(GameObject target)
    {
        for (int count = MoveList.Count - 1; count >= 0; count--)
        {
            if (MoveList[count] == target)
            {
                MoveList.RemoveAt(count);
            }
        }
    }
    public void ClearMoveList()
    {
        MoveList.Clear();
    }

    //キャラクターが通るマスめ
    public void SetRootMoveMass(GameObject setmass)
    {
        RootMoveMass.Add(setmass);
        
    }

    public void Clear()
    {
        RootMoveMass.Clear();
    }
    //フォールマルハウトのスキル専用
    public void FomalhautskillResultMoveRoot(GameObject MyMass, GameObject DestinationMass,GameObject ControlObj)
    {

        int MyMassX = MyMass.GetComponent<NumberMass>().GetXArrayNumber();
        int MyMassY = MyMass.GetComponent<NumberMass>().GetYArrayNumber();
        int DestinationMassX = DestinationMass.GetComponent<NumberMass>().GetXArrayNumber();//移動先のマス
        int DestinationMassY = DestinationMass.GetComponent<NumberMass>().GetYArrayNumber();//移動先のマス
        int XAdd;
        int YAdd;
        if (MyMassX > DestinationMassX)
        {
            XAdd = -1;
        }
        else
        {
            XAdd = 1;
        }

        if(MyMassY > DestinationMassY)
        {
            YAdd = -1;
        }
        else
        {
            YAdd = 1;
        }

        for(int count=0;count<100;count++)
        {
            gameObject.GetComponent<BoardMaster>().SetMassObjAreaChange(ref MyMassY, ref MyMassX, ControlObj);
            if (MyMassX != DestinationMassX)
            {
                MyMassX += XAdd;
            }
            if (MyMassY != DestinationMassY)
            {
                MyMassY += YAdd;
            }
            if (MyMassX == DestinationMassX && MyMassY == DestinationMassY)
            {
                break;
            }
            Debug.Log(MyMassY);
            Debug.Log(MyMassX);
        }
        ClearMoveList();
    }
}
