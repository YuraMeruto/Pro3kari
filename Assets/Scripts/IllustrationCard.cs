using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllustrationCard : MonoBehaviour {

    [SerializeField]
    private int RaceNumber;

    [SerializeField]
    private int SumonNumber;
   public int GetRaceNumber()
    {
        return RaceNumber;
    }

    public int GetSumonNumber()
    {
        return SumonNumber;
    }

}
