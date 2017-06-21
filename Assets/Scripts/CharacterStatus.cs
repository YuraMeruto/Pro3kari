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
    public SkillBase skill;
    public GameObject skillobj;
    private bool IsFirstMove = true; //ポーン専用
    [SerializeField]
    private bool IsSkill = false;
    private int NowLengthMass;
    private int NowSidehMass;
    private int MaxHp;
    [SerializeField]
    private GameObject ParticleObj;
    //Use this for initialization
    void Start()
    {
        MaxHp = CharcterHp;
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

    public void SetIsSkill(bool set)
    {
        IsSkill = set;
    }

    public void SetIsSkill()
    {
        IsSkill = !IsSkill;
    }

    public bool GetIsSkill()
    {
        return IsSkill;
    }

    public void SetIsActiveSkillParticle(bool _set)
    {
        ParticleObj.SetActive(_set);
    }
    public void SetReCovery(int recoverynum)
    {
        
    }

    public void SetAttackAdd(int Addnum)
    {
        CharcterAttack += Addnum;
    }

    public void SetHpAdd(int Addnum)
    {
        CharcterHp += Addnum;
        if(MaxHp<=CharcterHp)
        {
            CharcterHp = MaxHp;
        }
    }
}
