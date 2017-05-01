using UnityEngine;
using System.Collections;

public class NumberCharacter : MonoBehaviour {

    private int MassNumber;

    public void SetMassNumber(int x)
    {
        MassNumber = x;
    }

    public int GetNumber(int x)
    {
        return MassNumber;
    }
}
