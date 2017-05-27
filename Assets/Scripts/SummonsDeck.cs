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
    private GameObject[] ReadDeckDataObj = new GameObject[16];//読み込まれた数字からのオブジェクトの読み込み
    private int CSVMyPositionX;
    private int MaxLength;
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
            }
        }

        for (int xx = 0;xx <= MaxSide;xx++)
        {
            ReadDeckDataObj[xx] = Master.GetComponent<CharacterMaster>().GetIllustCharacter(ReadDeckData[xx,1]);
            Debug.Log(ReadDeckDataObj[xx]);
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
                int y = 1;
                InstancePos.z = 1;
                InstancePos.y -= 5;
                InstancePos.x -= 4;
            }
            if (Master.GetComponent<BoardMaster>().GetTurnPlayer() == 2)
            {
                Player = false;
                int y = 1;
                InstancePos.z = 1;
                InstancePos.y += 5;
                InstancePos.x += 4;
            }
            //        Instantiate(ReadDeckDataObj[0], InstancePos, ReadDeckDataObj[0].transform.rotation);
            for (int x = 0; x <= MaxSide; x++)
            {
                Instantiate(ReadDeckDataObj[x], InstancePos, ReadDeckDataObj[0].transform.rotation);
                if(Player)
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
}
