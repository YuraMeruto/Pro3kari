using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;
public class PlayerTime : MonoBehaviour
{

    [SerializeField]
    private float Timer;
    [SerializeField]
    private float SpeedTime;
    private float CopyTime;
    [SerializeField]
    private bool Is_StopTimer = true;
    [SerializeField]
    private Text TimerText;
    private Thread SetTimerThread;
    private GameObject Master;
    void Start()
    {
        Master = GameObject.Find("Master");
        CopyTime = Timer;
        SetTimerThread = new Thread(SetTimer);
        SetTimerThread.Start();
    }

    public void DecreaseTime()
    {
    }

    void Update()
    {
        if (!Is_StopTimer)
            Timer -= Time.deltaTime * SpeedTime;
        TimerText.text = Timer.ToString("00");
        if (Timer <= 0)
        {
            Master.GetComponent<BoardMaster>().SetTurnPlayer();
        }

    }


    public void SetIs_StopTimer(bool set)
    {
        Is_StopTimer = set;
    }
    public void SetTimer()
    {
        Timer = CopyTime;
        SetTimerThread.Abort();
    }
}
