using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsAttachObj : MonoBehaviour
{

    private bool IsAtachObj;

    public void SetIsAtachObj(bool set)

    {
        IsAtachObj = set;
    }

    public bool GetIsAtachObj()
    {
        return IsAtachObj;
    }
}
