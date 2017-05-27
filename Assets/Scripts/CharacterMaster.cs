using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMaster : MonoBehaviour {

    [SerializeField]
    private GameObject[] AllIllustCharacter = new GameObject[0];
    [SerializeField]
    private GameObject[] AllDictionaryCharacter = new GameObject[0];

    // Use this for initialization
    void Start () {
		
	}
	
    public GameObject GetIllustCharacter(int num)
    {
        return AllIllustCharacter[num];
    }

    public GameObject GettDictionaryCharacter(int num)
    {
        return AllDictionaryCharacter[num];
    }
}
