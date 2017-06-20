using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillState : MonoBehaviour {

    public void SkillActive()
    {
        MouseState.SkillActivateGo retgo = GetComponent<MouseState>().GetSkillActiveGo();
        GameObject AtachOject = GetComponent<AtachMaster>().GetAttachCharObj();
        Debug.Log(retgo);
        Debug.Break();
        switch (retgo)
        {
            case MouseState.SkillActivateGo.AtTheStart:
                AtachOject.GetComponent<CharacterStatus>().skill.AtTheStart();
                break;
            case MouseState.SkillActivateGo.AtTheEnd:
                AtachOject.GetComponent<CharacterStatus>().skill.AtTheEnd();
                break;
            case MouseState.SkillActivateGo.BattleStart:
                AtachOject.GetComponent<CharacterStatus>().skill.BattleStart();
                break;
            case MouseState.SkillActivateGo.MoveStart:
                AtachOject.GetComponent<CharacterStatus>().skill.MoveStart();
                break;
            case MouseState.SkillActivateGo.MoveEnd:
                AtachOject.GetComponent<CharacterStatus>().skill.MoveEnd();
                break;

        }

    }
}
