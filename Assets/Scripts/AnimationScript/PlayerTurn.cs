using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerTurn : MonoBehaviour {


    public void IsSetDownTrue()
    {
        //        GetComponent<Animator>().SetBool("IsDown",true);
        GetComponent<Animator>().SetTrigger("Setturn");
    }

    public void IsSetDownFalse()
    {
        GetComponent<Animator>().SetTrigger("Setturn");
    }

    public void IsSetUpTrue()
    {
        GetComponent<Animator>().SetBool("IsUp", true);
    }

    public void IsSetUpFalse()
    {
        GetComponent<Animator>().SetBool("IsUp", false);
    }
    public void SetText(string str)
    {
        GetComponent<Text>().text = str;
    }
    public void SetColorRed()
    {
        GetComponent<Text>().color = Color.red;
    }
    public void SetColorBlue()
    {
        GetComponent<Text>().color = Color.blue;
    }
}
