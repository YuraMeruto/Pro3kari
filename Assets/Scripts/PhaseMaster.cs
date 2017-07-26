using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseMaster : MonoBehaviour {


    public enum Phase { None, Main1, Move, Main2, TurnEnd };
    [SerializeField]
    private Phase phase = Phase.Main1;

    [SerializeField]
    private GameObject PlayerFaseUI;

    [SerializeField]
    private GameObject PlayerObj;

    public Phase GetNowFase()
    {
        return phase;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            bool ret = PlayerFaseUI.GetComponent<PlayerTurn>().GetAnimation();
            Debug.Log(ret);
        }

        if (phase == Phase.TurnEnd) {
            bool ret = PlayerFaseUI.GetComponent<PlayerTurn>().GetAnimation();
            if(!ret)
            {

                GetComponent<BoardMaster>().SetTurnPlayer();
                phase = Phase.Main1;
                PlayerObj.GetComponent<Player>().SetNowPhase(phase);
            }
        }
        
    }

    public void NextFase()
    {
        phase++;
        switch (phase)
        {
            case Phase.Main1:
            PlayerFaseUI.GetComponent<PlayerTurn>().SetColorBlue();
            PlayerFaseUI.GetComponent<PlayerTurn>().IsSetDownTrue();
            PlayerFaseUI.GetComponent<PlayerTurn>().SetText("召喚フェイズ1");
                break;
            case Phase.Main2:
                PlayerFaseUI.GetComponent<PlayerTurn>().SetColorBlue();
                PlayerFaseUI.GetComponent<PlayerTurn>().IsSetDownTrue();
                PlayerFaseUI.GetComponent<PlayerTurn>().SetText("召喚フェイズ2");
                break;
            case Phase.Move:
                PlayerFaseUI.GetComponent<PlayerTurn>().SetColorBlue();
                PlayerFaseUI.GetComponent<PlayerTurn>().IsSetDownTrue();
                PlayerFaseUI.GetComponent<PlayerTurn>().SetText("移動フェイズ");
                break;
            case Phase.TurnEnd:
                PlayerFaseUI.GetComponent<PlayerTurn>().SetColorBlue();
                PlayerFaseUI.GetComponent<PlayerTurn>().IsSetDownTrue();
                PlayerFaseUI.GetComponent<PlayerTurn>().SetText("ターンエンド");
                break;
        }

        // PlayerObj.GetComponent<Player>().SetNowPhase(phase);
        SetNowPhase();
    }

    public void SetFase(Phase setphase)
    {
        phase = setphase;
        SetNowPhase();
    }

    public void SetNowPhase()
    {
        PlayerObj.GetComponent<Player>().SetNowPhase(phase);
    }
}
