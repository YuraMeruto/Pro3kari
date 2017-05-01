using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
public class ReadCsv : MonoBehaviour
{
    [SerializeField]
    private string FileName;
    private int[,] IntData = new int[10, 10];
    // Use this for initialization
    void Start()
    {
        int countx = 0;
        int countz = 0;
        StreamReader sr = new StreamReader(Application.dataPath + "/" + "MoveData.csv", Encoding.GetEncoding("Shift_JIS"));
        while (sr.Peek() >= 0)
        {
            string[] cols = sr.ReadLine().Split(',');
            foreach (string col in cols)
            {
                IntData[countx, countz] = int.Parse(col);
                countz++;
                if (countz >= 10)
                {
                    countz = 0;
                    countx++;
                }

            }

        }

    }

    public int InputMoveData(int x,int z)
    {
        return IntData[x, z];
    }
}
