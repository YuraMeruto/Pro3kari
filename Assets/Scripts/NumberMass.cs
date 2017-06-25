using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberMass : MonoBehaviour
{

    [SerializeField]
    private int Number;
    private bool IsPossibleMoveArea = false;
    public enum Status { None, OK, NG }
    public Status status = Status.None;
    [SerializeField]
    private int PlayerNumber;
    [SerializeField]
    private GameObject Master;
    [SerializeField]
    private List<Material> MaterialList = new List<Material>();

    void Start()
    {
        Master = GameObject.Find("Master");
    }
    public void SetNumber(int num)
    {
        Number = num;
    }

    public int GetNumber()
    {
        return Number;
    }

    public void SetOKStatus()
    {
        status = Status.OK;
    }

    public void SetNGStatus()
    {
        status = Status.NG;
    }

    public void SetNoneStatus()
    {
        status = Status.None;
    }

    public bool GetIsPossibleMoveArea()
    {
        return IsPossibleMoveArea;
    }

    public void SetFalseIsPossibleMoveArea()
    {
        IsPossibleMoveArea = false;
    }

    public void SettrueIsPossibleMoveArea()
    {
        IsPossibleMoveArea = true;
    }

    public void SetPlayerNumber(int set)
    {
        PlayerNumber = set;
        SetMaterialNumber();
    }

    public void SetMaterialNumber()
    {
        //        gameObject.GetComponent<Renderer>().material. = Master.GetComponent<MaterialMaster>().GetMaterial(PlayerNumber);
        gameObject.GetComponent<Renderer>().material = MaterialList[PlayerNumber];
    }
    public int GetPlayerNumber()
    {
        return PlayerNumber;
    }
}
