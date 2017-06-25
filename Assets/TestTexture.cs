using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTexture : MonoBehaviour {

    public Material m1;
    public Material m2;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.W))
        {
            GetComponent<Renderer>().material = m1;
        }
	}
}
