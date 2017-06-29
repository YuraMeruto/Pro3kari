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
                Debug.Log(ReadDeckData[x, y]);
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
            ReadDeckDataObj[xx] = Master.GetComponent<CharacterMaster>().GetIllastCharacter(ReadDeckData[xx, 1]);
            //        Debug.Log(ReadDeckDataObj[xx]);
        }
    }

    //読み込んだカードを生成
    public void ShowCard()
    {
        if (IsCardShow)
        {
            Vector3 InstancePos = Camera.main.transform.position;
            //カードを表示させる場所を指定（いまだけ）
            if (Master.GetComponent<BoardMaster>().GetTurnPlayer() == 1)
            {
                Player = true;

                InstancePos.z = 1;
                InstancePos.y -= 5;
                InstancePos.x -= 4;
            }
            if (Master.GetComponent<BoardMaster>().GetTurnPlayer() == 2)
            {
                Player = false;

                InstancePos.z = 1;
                InstancePos.y += 5;
                InstancePos.x += 4;
            }
            for (int x = 0; x <= MaxSide; x++)
            {
                Instantiate(ReadDeckDataObj[x], InstancePos, ReadDeckDataObj[0].transform.rotation);
                if (Player)
                    InstancePos.x++;
                if (!Player)
                    InstancePos.x--;
            }
            IsCardShow = false;
        }
    }

    public void SetPlayerNumber(int num)
    {
        PlayerNumber = num;
    }

    public int GetPlayerNumber()
    {
        return PlayerNumber;
    }
    public void SetIsCardShow()
    {
        IsCardShow = true;
    }

    public bool GetISCardShow()
    {
        return IsCardShow;
    }
}
