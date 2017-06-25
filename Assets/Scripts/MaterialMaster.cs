using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialMaster : MonoBehaviour {

    [SerializeField]
    private List<Material> MaterialList = new List<Material>();

  public Material GetMaterial(int num)
    {

        return MaterialList[num];
    }
}
