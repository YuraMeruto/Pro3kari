using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
public class ReadCsv : MonoBehaviour
{
    [SerializeField]
    private string FileName;
    private int[,] IntData = new int[18, 18];
    [SerializeField]
    public int MaxSideSize;
    [SerializeField]
    public int MaxLengthSize;

    private GameObject TargetObj;
    // Use this for initialization
    void Start()
    {
        /*
        Debug.Log(FileName+".csv");
        int countx = 0;
        int countz = 0;
        int copyx = 0;
        int copyz = 0;
        StreamReader sr = new StreamReader(Application.dataPath + "/" + FileName +".csv", Encoding.GetEncoding("Shift_JIS"));
        Debug.Log(Application.dataPath);
       
        while (sr.Peek() >= 0)
        {
            string[] cols = sr.ReadLine().Split(',');
        //    Debug.Log(cols.Length);
            foreach (string col in cols)
            {
                IntData[countz, countx] = int.Parse(col);

               TargetObj.GetComponent<ReadCsv>().SetData(countz,countx,int.Parse(col));
               int a = TargetObj.GetComponent<ReadCsv>().InputMoveData(countz,countx);
                countx++;
                Debug.Log(a);
                /*
                if (countx > 4)
                {  
                    copyz = countz;
                    copyx = countx;
                    countx = 0;
                    countz++;
                }
                
            }
            copyz = countz;
            copyx = countx;
            countx = 0;
            countz++;
        }
        MaxSideSize = copyx-1;
        MaxLengthSize = copyz;
        if(MaxSideSize > 0 && MaxLengthSize >0)
       TargetObj.GetComponent<MoveData>().enabled = true;
       TargetObj.GetComponent<MoveData>().MoveDataMaxLengthSize = MaxLengthSize;
       TargetObj.GetComponent<MoveData>().MoveDataMaxSideSize = MaxSideSize;

        // Debug.Log(MaxSideSize);
        // Debug.Log(MaxLengthSize);
    */
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

    public void SetTargetObj(GameObject obj)
    {
        TargetObj = obj;
    }
    public void SetData(int length,int side,int num)
    {
        IntData[length, side] = num;
    }

    public void SetFileName(GameObject targetobj,string filename)
    {
        FileName = filename;
        TargetObj = targetobj;
        Debug.Log(FileName + ".csv");
        int countx = 0;
        int countz = 0;
        int copyx = 0;
        int copyz = 0;
        StreamReader sr = new StreamReader(Application.dataPath + "/" + FileName + ".csv", Encoding.GetEncoding("Shift_JIS"));

        while (sr.Peek() >= 0)
        {
            string[] cols = sr.ReadLine().Split(',');
            //    Debug.Log(cols.Length);
            foreach (string col in cols)
            {
                IntData[countz, countx] = int.Parse(col);
                TargetObj.GetComponent<ReadCsv>().SetData(countz, countx, int.Parse(col));
                int a = TargetObj.GetComponent<ReadCsv>().InputMoveData(countz, countx);
                countx++;
     //           Debug.Log(a);
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
        MaxSideSize = copyx - 1;
        MaxLengthSize = copyz;
        if (MaxSideSize > 0 && MaxLengthSize > 0)
            TargetObj.GetComponent<MoveData>().enabled = true;
        TargetObj.GetComponent<MoveData>().MoveDataMaxLengthSize = MaxLengthSize;
        TargetObj.GetComponent<MoveData>().MoveDataMaxSideSize = MaxSideSize;

        // Debug.Log(MaxSideSize);
        // Debug.Log(MaxLengthSize);

    }
}
