using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AP : MonoBehaviour
{

    [SerializeField]
    private int APCount;
    private int PlayerNumber;
    [SerializeField]
    private GameObject APObj;
    [SerializeField]
    private GameObject APPos;

    [SerializeField]
    private int MaxAP;
    [SerializeField]
    private List<GameObject> APList = new List<GameObject>();

    private GameObject InstanceObj;
    private int CopyCount;

    public void SetAP()
    {

    }
    public void AddAP()
    {
        APCount++;
    }

    public void InstanceAP()
    {
        Vector3 pos = APPos.transform.position;
        for (int count = 0;count < APCount;count++)
        {
            GameObject ObjName = Instantiate(APObj, pos, APObj.transform.rotation);
            pos.x--;
            APList.Add(ObjName);
        }
    }

    public void AllDestory()
    {
        var clones = GameObject.FindGameObjectsWithTag("SPTag");
        foreach (var clone in clones)
        {
            Destroy(clone);
        }
    }

    public void ClearList()
    {
        APList.Clear();
    }
}
