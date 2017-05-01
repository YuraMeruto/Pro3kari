using UnityEngine;
using System.Collections;

public class Porn : MonoBehaviour {

    private int[,] MoveData = new int[10,10];
	// Use this for initialization
	void Start () {
        for (int x = 0; x < 10; x++)
        {
            for (int z =0; z < 10; z++) {
                MoveData[x,z] = GetComponent<ReadCsv>().InputMoveData(x,z);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
