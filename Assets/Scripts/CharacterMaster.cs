using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMaster : MonoBehaviour {

    [SerializeField]
    private GameObject[] AllCharacter = new GameObject[0];
	// Use this for initialization
	void Start () {
		
	}
	
    public GameObject GetCharacter(int num)
    {
        return AllCharacter[num];
    }
}
