using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    LayerMask MassLayer;
    private GameObject AtachMassObject;
    private GameObject CopyAtachMassObject;
    private GameObject AtachCharObject = null;
    private int AtachMassNumber;
    private GameObject MasterObject;
    private enum PlayerStatus { None, Choose };
    public enum Status { None, OK, NG, IsMoveArea };
    private PlayerStatus status = PlayerStatus.None;
    // Use this for initialization
    void Start()
    {
        MasterObject = GameObject.Find("Master");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, MassLayer))
            {
                switch (status)
                {
                    case PlayerStatus.None:
                        AtachMassObject = hit.collider.gameObject;
                        CopyAtachMassObject = AtachMassObject;
                        AtachMassNumber = AtachMassObject.GetComponent<NumberMass>().GetNumber();
                        AtachCharObject = MasterObject.GetComponent<BoardMaster>().GetCharObject(AtachMassNumber);                    
                        if (AtachCharObject != null)
                        {
                            AtachCharObject.GetComponent<MoveData>().IsPossibleMove(AtachMassNumber);
                            status = PlayerStatus.Choose;
                        }
                        break;

                    case PlayerStatus.Choose:
                         bool ret;
                        CopyAtachMassObject = hit.collider.gameObject;
                        int CopyAttaMassNumber = CopyAtachMassObject.GetComponent<NumberMass>().GetNumber();
                        ret = MasterObject.GetComponent<BoardMaster>().GetIsMove(CopyAttaMassNumber);
                        if (ret)
                        {
                            var clones = GameObject.FindGameObjectsWithTag("IsMovetag");
                            foreach(var clone in clones)
                            {
                                Destroy(clone);
                            }
                            Vector3 MovePos = MasterObject.GetComponent<BoardMaster>().GetPos(CopyAttaMassNumber);
                            MovePos.y = 1.0f;
                            AtachCharObject.transform.position = MovePos;
                            MasterObject.GetComponent<BoardMaster>().SetIsCharObj(CopyAttaMassNumber,AtachMassNumber,AtachCharObject);
                            MasterObject.GetComponent<BoardMaster>().SetAllFalseIsMove();
                            AtachCharObject = null;
                            status = PlayerStatus.None;
                        }
                        break;
                }
                
                //Debug.Log(AtachMassObjct);
                //Debug.Log(AtachMassNumber);
                //Debug.Log(AtachCharObjct);
                //Debug.Log(status);
            }
        }
    }
}
