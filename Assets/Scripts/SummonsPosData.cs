using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class SummonsPosData : MonoBehaviour {

    [SerializeField]
    private string FileName;
    private int[,] IntData = new int[10, 10];
    private int MaxSideSize;
    private int MaxLengthSize;
    // Use this for initialization
    void Start()
    {
        GameObject Master = GameObject.Find("Master");
        int countx = 0;
        int countz = 0;
        int copyx = 0;
        int copyz = 0;
       TextAsset data = Resources.Load("SummonsPosData")as TextAsset;
        Debug.Log(data);
        StringReader sr = new StringReader(data.text);
        while (sr.Peek() > -1)
        {
            string[] cols = sr.ReadLine().Split(',');
            //    Debug.Log(cols.Length);
            foreach (string col in cols)
            {
                Debug.Log("a");
                IntData[countx, countz] = int.Parse(col);
                countx++;
            }
            copyz = countz;
            copyx = countx;
            countx = 0;
            countz++;
        }

        MaxSideSize = copyx;
        MaxLengthSize = copyz;

        for (int length = 0;length <= MaxLengthSize;length++)
        {
            for (int side = 0; side<= MaxSideSize; side++)
            {
                Master.GetComponent<BoardMaster>().SetSummonsPosData(IntData[length,side],length,side);   
            }
        }

    }

    public int InputMoveData(int z, int x)
    {
        return IntData[z, x];
    }
    public int GetMaxLength()
    {
        return MaxLengthSize;
    }

    public int GetMaxSide()
    {
        return MaxSideSize;
    }
}
