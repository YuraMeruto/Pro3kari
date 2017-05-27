using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllustrationCard : MonoBehaviour {

    [SerializeField]
    private int RaceNumber;
    [SerializeField]
    private int DictionaryNumber;
   public int GetRaceNumber()
    {
        return RaceNumber;
    }
    
    public int GetDictionaryNumber()
    {
        return DictionaryNumber;
    }
}
