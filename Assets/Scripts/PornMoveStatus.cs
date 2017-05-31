using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PornMoveStatus : MonoBehaviour {

    private int[,] MoveStatus = new int[3, 3];
    private bool IsFirstMove = true;
	// Use this for initialization
	void Start () {
        MoveStatus[0, 1] = 1;
        MoveStatus[1, 1] = 1;
        MoveStatus[2, 1] = 2;
    }


}
    