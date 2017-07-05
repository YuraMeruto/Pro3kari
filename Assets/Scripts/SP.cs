using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP : MonoBehaviour
{

    [SerializeField]
    private int SPCount;
    private int PlayerNumber;
    [SerializeField]
    private int CopySP;
    [SerializeField]
    private GameObject SPObj;
    [SerializeField]
    private GameObject SPPos;

    [SerializeField]
    private int SPMax;    
    [SerializeField]
    private List<GameObject> SPObjList = new List<GameObject>();
    [SerializeField]
    private int ApStartNum;
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
        if (CopySP <= SPMax)
        {
            SPCount = CopySP;
            SPCount += 1;
            CopySP = SPCount;
            if (ApStartNum <= SPCount)
                GetComponent<AP>().AddAp();
        }
    }

    public void SetSPArray()
    {
        for (int count = 0; count < SPCount; count++)
        {
            SPObjList.Add(SPObj);
        }
    }

    public void DestroyList(int num)
    {
        int copynum = num;
        for (int count = SPObjList.Count; num > 0; count--)
        {
            Destroy(SPObjList[count - 1]);
            SPObjList.RemoveAt(count - 1);
            num--;
        }
        SPCount = SPObjList.Count;
    }

    public void ClearList()
    {
        SPObjList.Clear();
    }
    
    public void InstanceSPObj()
    {
        Vector3 InstancePos = SPPos.transform.position;

        for (int count = 0; count < SPCount; count++)
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
