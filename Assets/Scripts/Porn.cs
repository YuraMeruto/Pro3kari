using UnityEngine;
using System.Collections;

public class Porn : MonoBehaviour {

    [SerializeField]
    GameObject Master;
    private int[,] MoveData = new int[10,10];
    private int PlayerNumber;
    //Use this for initialization
	void Start () {
        Master = GameObject.Find("Master");
        for (int x = 0; x < 10; x++)
        {
            for (int z =0; z < 10; z++) {
                MoveData[x,z] = Master.GetComponent<ReadCsv>().InputMoveData(x,z);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  public void SetPlayerNumber(int SetNumber)
    {
        PlayerNumber = SetNumber;
    }

}
