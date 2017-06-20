using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardList : MonoBehaviour
{

    public List<GameObject> SkillList = new List<GameObject>();
    public List<GameObject> SkillTargetList = new List<GameObject>();
    [SerializeField]
    public List<GameObject> MoveList = new List<GameObject>();

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

    public void SetMoveList( GameObject setobj)
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
}
