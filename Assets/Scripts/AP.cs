using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AP : MonoBehaviour
{
    [SerializeField]
    private int MaxAp;
    [SerializeField]
    private int NowAp = 0;
    [SerializeField]
    private GameObject ApObj;
    [SerializeField]
    private List<GameObject> ApList = new List<GameObject>();
    [SerializeField]
    private GameObject ApPos;
    public void AddAp()
    {
            NowAp++;
        if (NowAp > MaxAp)
        {
            NowAp = MaxAp;
        }
        InstanceAp();
    }

    public bool UseAp(int num)
    {
        if (NowAp - num < 0)
        {
            return false;
        }
        UseApDestroy(num);
        NowAp -= num;
        return true;
    }

    public int GetNowAp()
    {
        return NowAp;
    }
    public void InstanceAp()
    {
        AllDestroy();
        Vector3 InstancePos = ApPos.transform.position;
        InstancePos.z = 1;
        for (int count = 0; count < NowAp; count ++)
        {
            GameObject Instance = Instantiate(ApObj,InstancePos,Quaternion.identity);
            InstancePos.x -= 1.5f;
            ApList.Add(Instance);
        }
    }

    public void AllDestroy()
    {
        var clones = GameObject.FindGameObjectsWithTag("ApTag");
        foreach (var clone in clones)
        {
            Destroy(clone);
            ApList.Clear();
        }
    }

    public void UseApDestroy(int num)
    {
        for(int count = ApList.Count-1;num != 0;count--)
        {
            Destroy(ApList[count]);
            ApList.RemoveAt(count);
            num--;
        }
    }
}
