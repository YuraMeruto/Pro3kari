using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterMoveData : MonoBehaviour {

    [SerializeField]
    private string[] MoveList = new string[0];
    private int MaxDataSize;
	// Use this for initialization
	void Start () {
      MaxDataSize =  MoveList.Length;
	}
	
    public void GetReadMoveData(string filename)
    {
        for(int count =0;count<MaxDataSize;count++)
        {
            if(MoveList[count] == filename)
            {

            }
                
        }
    }
}
