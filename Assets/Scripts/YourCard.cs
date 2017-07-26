using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YourCard : MonoBehaviour
{

    [SerializeField]
    private bool IsDrawCard = false;
    [SerializeField]
    private int IniDrawCount;
    [SerializeField]
    private List<GameObject> DrawCards = new List<GameObject>();
    [SerializeField]
    private GameObject InstancePosObj;
    private Vector3 CopyPos;
    void Start()
    {
        CopyPos = InstancePosObj.transform.position;
    }
    public void SetIsDrawCard(bool set)
    {
        IsDrawCard = set;
    }

    public bool GetIsDrawCard()
    {
        return IsDrawCard;
    }

    public void SetDrawCards(GameObject set)
    {
        InstanceCards(set);

    }

    public void InstanceCards(GameObject set)
    {
        Vector3 Pos = InstancePosObj.transform.position;
        GameObject InstanceCard = Instantiate(set, Pos, set.transform.rotation);
        Pos.x++;
        InstancePosObj.transform.position = Pos;
        DrawCards.Add(InstanceCard);
    }

    public void PopCard(GameObject target)
    {
        if (target == null)
            return;
        for (int count = 0; count < DrawCards.Count - 1; count++)
        {
            if (DrawCards[count].name == target.name)
            {
                DrawCards.RemoveAt(count);
                Destroy(target);
                Vector3 vec = InstancePosObj.transform.position;
                vec.x--;
                InstancePosObj.transform.position = vec;
            }
        }
        ResetCards();

    }
    public void ResetCards()
    {
        Vector3 pos = CopyPos;
        for (int count = 0; count <= DrawCards.Count - 1; count++)
        {
            DrawCards[count].transform.position = pos;
            pos.x++;
        }
        InstancePosObj.transform.position = pos;
    }
}
