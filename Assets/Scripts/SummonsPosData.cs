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
        bool ini = false;
        StreamReader sr = new StreamReader(Application.dataPath + "/" + "SummonsPosData.csv", Encoding.GetEncoding("Shift_JIS"));
        while (sr.Peek() >= 0)
        {
            string[] cols = sr.ReadLine().Split(',');
            //    Debug.Log(cols.Length);
            foreach (string col in cols)
            {
                IntData[countx, countz] = int.Parse(col);
                //Debug.Log("xは;"+countx);
                //Debug.Log("zは;" + countz);
                //Debug.Log("合計は;" + IntData[countx,countz]);
                countx++;
            }
            copyz = countz;
            copyx = countx;
            countx = 0;
            countz++;
            ini = true;
        }

        MaxSideSize = copyx - 1;
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
