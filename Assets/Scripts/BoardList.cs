using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardList : MonoBehaviour {

    public List<GameObject> SkillList = new List<GameObject>();
	
	// Update is called once per frame
    public void SetSkillList(GameObject setobj)
    {
        SkillList.Add(setobj);
    }

    public bool GetList(GameObject getobj)
    {
        for(int count = SkillList.Count-1;count>0;count--)
        {
            if(getobj == SkillList[count])
            {
                return true;
            }
        }
        return false;
    }
}
