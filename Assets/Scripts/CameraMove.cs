using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 2 * Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, -2 * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(2 * Time.deltaTime,0, 0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate( -2 * Time.deltaTime,0,0);
        }

        else if(Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, 0 , 2 * Time.deltaTime);
        }

        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, 0, -2 * Time.deltaTime);
        }
    }
}
