using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberMass : MonoBehaviour {

    [SerializeField]
    private int Number;
    public enum Status {None,OK,NG}
    public Status status = Status.None;

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
}
