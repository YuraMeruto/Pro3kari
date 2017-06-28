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

    [SerializeField]
    private int XArrayNum;
    [SerializeField]
    private int YArrayNum;
    [SerializeField]
    private int DefaltMaterialNumber;
    [SerializeField]
    private GameObject ControllingCharacter;
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
    public void SetMaterialDefalt()
    {
        gameObject.GetComponent<Renderer>().material = MaterialList[DefaltMaterialNumber];
    }
    //スキルによる効果
    public void SetMaterialNumber(int num)
    {
        //        gameObject.GetComponent<Renderer>().material. = Master.GetComponent<MaterialMaster>().GetMaterial(PlayerNumber);
        gameObject.GetComponent<Renderer>().material = MaterialList[num];
    }
    public int GetPlayerNumber()
    {
        return PlayerNumber;
    }

    public void SetXArryNumber(int setnum)
    {
        XArrayNum = setnum;
    }

    public void SetYArryNumber(int setnum)
    {
        YArrayNum = setnum;
    }

    public int GetXArrayNumber()
    {
        return XArrayNum;
    }

    public int GetYArrayNumber()
    {
        return YArrayNum;
    }

    public void SetDefaltNumber(int setnum)
    {
        DefaltMaterialNumber = setnum;
    }

    public int GetDefaltNumber()
    {
        return DefaltMaterialNumber;
    }

    //フォーマルハウトのスキルで使用
    public void SetControlObj(GameObject setobj)
    {
        ControllingCharacter = setobj;
    }

    public GameObject GetControlObj()
    {
        return ControllingCharacter;
    }
}
