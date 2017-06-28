using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtachMaster : MonoBehaviour
{
    [SerializeField]
    private GameObject AttachMassObject;
    [SerializeField]
    private GameObject CopyAttachMassObject;
    [SerializeField]
    private GameObject AttachCharObject;
    [SerializeField]
    private GameObject EnemyObjct;
    [SerializeField]
    private GameObject ChoosingCardObject;
    [SerializeField]
    private GameObject InstanceSumonObject;
    [SerializeField]
    private GameObject MassObject;
    [SerializeField]
    private GameObject CopyMassObject;
    [SerializeField]
    private GameObject AtachDeckObj;
    [SerializeField]
    private GameObject AtachDeckCardObj;
    [SerializeField]
    private GameObject ChoosingCardSummonObj;
    [SerializeField]
    private int Damage =100;
    private int AttachMassNumber;
    [SerializeField]
    private GameObject SkillTarget;
    [SerializeField]
    private GameObject SkillInvoker;
    public void SetAttachMassObject(GameObject setobj)
    {
        AttachMassObject = setobj;
        AttachMassNumber = AttachMassObject.GetComponent<NumberMass>().GetNumber();
    }

    public void SetCopyAttachMassObj(GameObject setobj)
    {
        CopyAttachMassObject = setobj;
    }

    public void SetEnemyObj(GameObject setobj)
    {
        EnemyObjct = setobj;
    }
    public void SetChoosingCardObj(GameObject setobj)
    {
        ChoosingCardObject = setobj;
    }
    public void SetAttachDeckCardObj(GameObject setobj)
    {
        AtachDeckCardObj = setobj;
    }

    public void SetAttachDeckObj(GameObject setobj)
    {
        AtachDeckObj = setobj;
    }

    public void SetInstanceSumonObj(GameObject setobj)
    {
        InstanceSumonObject = setobj;
    }

    public void SetAttachCharObj(GameObject setobj)
    {
        AttachCharObject = setobj;
    }

    public void SetChoosingCardSummonObj(GameObject setobj)
    {
        ChoosingCardObject = setobj;
    }

    public void SetDamage(int setdamage)
    {
        Damage = setdamage;
    }

    public void SetSkillTarget(GameObject setobj)
    {
        SkillTarget = setobj;
    }
    public void SetSkillInvoker(GameObject setobj)
    {
        SkillInvoker = setobj;
    }

    //Get関係
    public GameObject GetAttachMassObj()
    {
        return AttachMassObject;
    }

    public GameObject GetAttachEnemyObj()
    {
        return EnemyObjct;
    }

    public GameObject GetAttachChoosingCardObj()
    {
        return ChoosingCardObject;
    }

    public GameObject GetAttachDeckObj()
    {
        return AtachDeckObj;
    }

    public GameObject GetAttachDeckCardObj()
    {
        return AtachDeckCardObj;
    }

    public GameObject GetInstanceSumonObj()
    {
        return InstanceSumonObject;
    }

    public GameObject GetCopyAttachMassObj()
    {
        return CopyAttachMassObject;
    }

    public int GetAttachMassNumber()
    {
        return AttachMassNumber;
    }

    public GameObject GetAttachCharObj()
    {
        return AttachCharObject;
    }

    public GameObject GetChoosingCardSummonObj()
    {
        return ChoosingCardObject;
    }

    public int GetDamage()
    {
        return Damage;
    }

    public GameObject GetSkillTarget()
    {
        return SkillTarget;
    }

    public GameObject GetSkillInvoker()
    {
        return SkillInvoker;
    }
}
