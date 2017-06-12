using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScene : MonoBehaviour {

    public enum BattleResult { Win,Lose,Draw};
    private GameObject Master;
    private GameObject AttachCharcterObject;
    private GameObject EnemyObject;


public  BattleResult Result()
    {
         GameObject AttachCharcterObject = GetComponent<AtachMaster>().GetAttachCharObj();
        GameObject EnemyObject = GetComponent<AtachMaster>().GetAttachEnemyObj();
        int MyCharHp = AttachCharcterObject.GetComponent<CharacterStatus>().GetHp();
        int MyCharAttack = AttachCharcterObject.GetComponent<CharacterStatus>().GetAttack();
        int EnemyHp = EnemyObject.GetComponent<CharacterStatus>().GetHp();
        int EnemyAttack = EnemyObject.GetComponent<CharacterStatus>().GetAttack();

        int ResultHp = EnemyHp - MyCharAttack;
        EnemyObject.GetComponent<CharacterStatus>().SetHp(ResultHp);
        if (ResultHp <= 0)//プレイヤーからの攻撃
        {

            //Destroy(IsEnemyObj);
            Debug.Log("勝利しました");
            return BattleResult.Win;
        }

        ResultHp = MyCharHp - EnemyAttack;
        if (ResultHp <= 0)//プレイヤーの体力がゼロになったら
        {
            AttachCharcterObject.GetComponent<CharacterStatus>().SetHp(ResultHp);
            Destroy(AttachCharcterObject);
            Debug.Log("勝利しました");
            Debug.Log(AttachCharcterObject);
            return BattleResult.Lose;
        }

        AttachCharcterObject.GetComponent<CharacterStatus>().SetHp(ResultHp);
        Debug.Log("引き分け");
        //MovePos = MasterObject.GetComponent<BoardMaster>().EnemySurroundings(CopyAttachMassNumber);
        return BattleResult.Draw;
    }

}
