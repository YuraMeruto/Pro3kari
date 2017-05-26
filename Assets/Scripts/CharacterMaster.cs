using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMaster : MonoBehaviour {

    [SerializeField]
    private GameObject[] AllIllustCharacter = new GameObject[0];
    [SerializeField]
    private GameObject[] AllSummonsCharacter = new GameObject[0];

    // Use this for initializationb
    void Start () {
		
	}
	
    public GameObject GetIllastCharacter(int num)
    {
        return AllIllustCharacter[num];
    }

    public GameObject GetSummonsCharacter(int num)
    {
        return AllSummonsCharacter[num];
    }
}
