using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP : MonoBehaviour
{

    [SerializeField]
    private int SPCount;
    private int PlayerNumber;
    private int CopySP;


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
    }
}
