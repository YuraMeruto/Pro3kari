using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNumber : MonoBehaviour {

    private int PlayerNum;

    public void SetPlayerNumber(int Number)
    {
        PlayerNum = Number;
    }

    public int GetNumber()
    {
        return PlayerNum;
    }
}
