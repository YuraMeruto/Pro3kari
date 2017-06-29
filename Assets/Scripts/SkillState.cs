using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillState : MonoBehaviour {

    public void SkillActive()
    {
        // MouseState.SkillActivateGo retgo = GetComponent<MouseState>().GetSkillActiveGo();
        MouseState.SkillActivate ret = GetComponent<MouseState>().GetSkillActive();
        GameObject Invorker = GetComponent<AtachMaster>().GetSkillInvoker();
        switch(ret)
        {
            case MouseState.SkillActivate.Yes:
                Invorker.GetComponent<CharacterStatus>().skill.SkillIsActive();
                break;
            case MouseState.SkillActivate.No:
                GetComponent<Player>().SetState(Player.PlayerStatus.None);
                GetComponent<MouseState>().SetSkillActive(MouseState.SkillActivate.None); ;
                break;
                
        }

    }
}
