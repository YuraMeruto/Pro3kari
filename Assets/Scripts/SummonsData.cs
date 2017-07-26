using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
public class SummonsData : MonoBehaviour {

    [SerializeField]
    private string FileName;
    private int[,] IntData = new int[16,2];
    private int MaxSideSize;
    private int MaxLengthSize;

    // Use this for initialization
    void Start()
    {
        int countx = 0;
        int countz = 0;
        TextAsset data = Resources.Load("PlayerDeck1") as TextAsset;
        StringReader sr = new StringReader(data.text);
        while (sr.Peek() >= 0)
        {
            countx = 0;
            string[] cols = sr.ReadLine().Split(',');
            foreach (string col in cols)
            {
                IntData[countx,countz] = int.Parse(col);
                countx++;
            }
            countz++;

        }
        GetComponent<SummonsDeck>().enabled = true;
        MaxSideSize = countx - 1;
        MaxLengthSize = countz - 1;
    }

    public int InputMoveData(int x,int y)
    {
        return IntData[x,y];
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
