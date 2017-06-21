using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSwitch : MonoBehaviour {

	public void IsEnabledTrue()
    {
        gameObject.SetActive(true);
    }

    public void IsEnabledFlase()
    {
        gameObject.SetActive(false);
    }
}
