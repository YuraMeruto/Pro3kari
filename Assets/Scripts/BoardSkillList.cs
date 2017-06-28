using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSkillList : MonoBehaviour {
    
    //相手の移動が終わったときに発動するキャラクターのリスト
    [SerializeField]
    private List<GameObject> EnemyMoveEndSkillList;
    [SerializeField]
    private int EnemyPosx;
    [SerializeField]
    private int EnemyPosy;

    [SerializeField]
    private List<GameObject> MyMoveEndSkillList;
    //相手の動きが終わったときに発動スキルキャラクターのリストの追加
    public void SetEnemyMoveEndSkillList(GameObject setobj)
    {
        EnemyMoveEndSkillList.Add(setobj);
    }
    
    public void ActiveEnemyMoveEndSkill()
    {
        int numplayerturn = GetComponent<BoardMaster>().GetTurnPlayer();
        for(int count = 0;count < EnemyMoveEndSkillList.Count;count++)
        {
            int playernum  = EnemyMoveEndSkillList[count].GetComponent<CharacterStatus>().GetPlayerNumber();
            if (numplayerturn != playernum)
            {
                Debug.Log("aaa");
                EnemyMoveEndSkillList[count].GetComponent<CharacterStatus>().skill.EnemyMoveEnd();
            }
        }
    }
    
    public void DestroyList(GameObject destroyobj)
    {
        for (int count = 0; count < EnemyMoveEndSkillList.Count; count++)
        {
         if(EnemyMoveEndSkillList[count] == destroyobj)
            {
                EnemyMoveEndSkillList.RemoveAt(count);
            }
        }

    }

    public void SetPosLength (int setx) {

        EnemyPosx = setx;
    }

    public void SetPosSide(int sety)
    {

        EnemyPosy = sety;
    }
    public int GetPosLength()
    {
        return EnemyPosx;
    }

    public int GetPosSide()
    {
        return EnemyPosy;
    }

    public void SetMyMoveEndSkillAdd(GameObject setcharacter)
    {
        MyMoveEndSkillList.Add(setcharacter);
    }

    public void ActiveGetMyMoveEndSkil()
    {
        int numplayerturn = GetComponent<BoardMaster>().GetTurnPlayer();
        for (int count = 0; count < MyMoveEndSkillList.Count; count++)
        {
            int playernum = MyMoveEndSkillList[count].GetComponent<CharacterStatus>().GetPlayerNumber();
            if (numplayerturn != playernum)
            {
                Debug.Log("aaa");
                MyMoveEndSkillList[count].GetComponent<CharacterStatus>().skill.MoveEnd();
            }
        }
        
    }
}
