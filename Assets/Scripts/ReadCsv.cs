using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
public class ReadCsv : MonoBehaviour
{
    [SerializeField]
    private string FileName;
    private int[,] IntData = new int[10, 10];
    private int MaxSideSize;
    private int MaxLengthSize;

    // Use this for initialization
    void Start()
    {
        int countx = 0;
        int countz = 0;
        int copyx = 0;
        int copyz = 0;
        StreamReader sr = new StreamReader(Application.dataPath + "/" + "PornMove.csv", Encoding.GetEncoding("Shift_JIS"));
        while (sr.Peek() >= 0)
        {
            string[] cols = sr.ReadLine().Split(',');
        //    Debug.Log(cols.Length);
            foreach (string col in cols)
            {
                IntData[countz, countx] = int.Parse(col);                
                countx++;
                /*
                if (countx > 4)
                {  
                    copyz = countz;
                    copyx = countx;
                    countx = 0;
                    countz++;
                }
                */
            }
            copyz = countz;
            copyx = countx;
            countx = 0;
            countz++;
        }
     
        MaxSideSize = copyx-1;
        MaxLengthSize = copyz;
       // Debug.Log(MaxSideSize);
       // Debug.Log(MaxLengthSize);

    }

    public int InputMoveData(int z,int x)
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
