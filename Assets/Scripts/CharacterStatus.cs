using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    [SerializeField]
    private int Race;
    [SerializeField]
    GameObject Master;
    private int[,] MoveData = new int[10, 10];
    [SerializeField]
    private int PlayerNumber;//自分がしょぞくしているナンバー
    [SerializeField]
    private int CharcterHp;
    [SerializeField]
    private int CharcterAttack;

    [SerializeField]
    private int SummoningSickness =0;
    [SerializeField]
    private int SummonsCost;
    public List<SkillMaster> skills = new List<SkillMaster>();
    public SkillBase skill;
    private bool IsFirstMove = true; //ポーン専用
    //Use this for initialization
    void Start()
    {

        Master = GameObject.Find("Master");
        for (int x = 0; x < 10; x++)
        {
            for (int z = 0; z < 10; z++)
            {
                MoveData[x, z] = GetComponent<ReadCsv>().InputMoveData(x, z);
            }
        }
    }

    // Update is called once per frame


    public void SetPlayerNumber(int SetNumber)
    {
        PlayerNumber = SetNumber;
    }

    public int GetPlayerNumber()
    {
        return PlayerNumber;
    }

    public void SetHp(int hp)
    {
        CharcterHp = hp;
        Debug.Log(CharcterHp);
    }

    public int GetHp()
    {
        return CharcterHp;
    }

    public int GetAttack()
    {
        return CharcterAttack;
    }

    public void SetAttack(int attack)
    {
        CharcterAttack = attack;
    }

    public void SkillStart()
    {
        skills[0].Init();
    }

    public void SetRole()
    {

    }

    public int GetRace()
    {
        return Race;
    }

    public int GetSummoningSickness()
    {
        return SummoningSickness;
    }

    public void SetSummoningSickness(int num)
    {
        SummoningSickness = num;
    }

    public void SubtractionSummoningSickness()
    {
        SummoningSickness--;
    }

    public void SetIsFirst()
    {
        IsFirstMove = false;
        GetComponent<MoveData>().SetMoveData();
    }
}
