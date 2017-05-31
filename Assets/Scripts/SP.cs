using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP : MonoBehaviour
{

    [SerializeField]
    private int SPCount ;
    private int PlayerNumber;
    [SerializeField]
    private int CopySP;
    [SerializeField]
    private GameObject SPObj;
    [SerializeField]
    private GameObject SPPos;

    [SerializeField]
    private List<GameObject> SPObjList = new List<GameObject>();

    private int Number;
    public void ResetSP()
    {
        SPCount = CopySP;
    }
    public void AddSP()
    {
        SPCount += 1;
    }

    /// <summary>
    /// SPを使ったら
    /// </summary>
    public void ConsumptionSP(int count)
    {
        SPCount -= count;
    }

    public int GetSP()
    {
        return SPCount;
    }

    public void ResetAddSP()
    {
        SPCount = CopySP;
        SPCount += 1;
        CopySP = SPCount;

    }

    public void SetSPArray()
    {
        for (int count = 0; count < SPCount; count++)
        {
            SPObjList.Add(SPObj);
        }
    }
        
    public void  DestroyList(int num)
    {
        int copynum = num;
        for(int count = SPObjList.Count; num > 0;count--)
        {
            Debug.Log("aaaaaaaaaa");
            Destroy(SPObjList[count-1]);
            SPObjList.RemoveAt(count-1);
            num--;
        }
        SPCount = SPObjList.Count ;
    }

    public void ClearList()
    {
        SPObjList.Clear();
    }
    /*
    public void InstanceSPObj(int num)
    {
        Vector3 InstancePos = SPPos.transform.position;
        for (;num>0;num--)
        {
         GameObject ObjName = Instantiate(SPObj,InstancePos,SPObj.transform.rotation);
            InstancePos.x--;
        }
    }
    */
    public void InstanceSPObj()
    {
        Vector3 InstancePos = SPPos.transform.position;
             
        for (int count =0;count<SPCount;count++)
        {
            GameObject ObjName = Instantiate(SPObj, InstancePos, SPObj.transform.rotation);          
            InstancePos.x--;
            SPObjList.Add(ObjName);
        }
    }

    public void AllDestroy()
    {
        var clones = GameObject.FindGameObjectsWithTag("SPTag");
        foreach (var clone in clones)
        {
            Destroy(clone);
        }
    }
}
