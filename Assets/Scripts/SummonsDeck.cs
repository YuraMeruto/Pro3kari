using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonsDeck : MonoBehaviour
{


    [SerializeField]
    private GameObject MyDeckObj;
    [SerializeField]
    private GameObject Master;
    private bool Player;

    private bool IsCardShow = true;
    private int PlayerNumber;
    private int[,] ReadDeckData = new int[16, 16];//読み込まれた数字
    [SerializeField]
    private GameObject[] ReadDeckDataObj = new GameObject[16];//読み込まれた数字からのオブジェクトの読み込み
    private int CSVMyPositionX;
    [SerializeField]
    private int MaxLength;
    [SerializeField]
    private int MaxSide;
    [SerializeField]
    private List<GameObject> ReadDeckDataObjList = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        //        MyDeckObj = GameObject.Find("M");
        Master = GameObject.Find("Master");
        MaxLength = GetComponent<SummonsData>().GetMaxLength();
        MaxSide = GetComponent<SummonsData>().GetMaxSide();
        for (int x = 0; x <= MaxSide; x++)
        {
            for (int y = 0; y <= MaxLength; y++)
            {
                ReadDeckData[x, y] = GetComponent<SummonsData>().InputMoveData(x, y);//デッキからデータを読み込む
            }
        }
        //キングを検索
        for (int x = 0; x <=MaxSide ; x++)
        {
            if (ReadDeckData[x, 0] == 6)
            {
                Debug.Log("キング発見");
                Master.GetComponent<BoardMaster>().InstanceKing(PlayerNumber, ReadDeckData[x, 1]);
            }
        }
        for (int xx = 0; xx <= MaxSide; xx++)
        {
            if (ReadDeckData[xx, 0] != 6)
            {
                ReadDeckDataObj[xx] = Master.GetComponent<CharacterMaster>().GetIllastCharacter(ReadDeckData[xx, 1]);
                ReadDeckDataObjList.Add(Master.GetComponent<CharacterMaster>().GetIllastCharacter(ReadDeckData[xx, 1]));
            }
        }
        ShaffuleDeck();
        Master.GetComponent<BoardMaster>().IniDrawCard(PlayerNumber);
    }

    public void SetPlayerNumber(int num)
    {
        PlayerNumber = num;
    }

    public int GetPlayerNumber()
    {
        return PlayerNumber;
    }




    public GameObject GetReadDeckDataObjList()
    {
        GameObject ret = ReadDeckDataObjList[0];
        ReadDeckDataObjList.RemoveAt(0);
        return ret;
    }
    public void ShaffuleDeck()
    {
        for (int index =0;index<ReadDeckDataObjList.Count;index++)
        {
            GameObject CopyObj = ReadDeckDataObjList[index];
            int randomindex = Random.Range(0,ReadDeckDataObjList.Count);
            ReadDeckDataObjList[index] =ReadDeckDataObjList[randomindex];
            ReadDeckDataObjList[randomindex] = CopyObj;
        }
    }
}
