using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillState : MonoBehaviour {

    public void SkillActive()
    {
        // MouseState.SkillActivateGo retgo = GetComponent<MouseState>().GetSkillActiveGo();
        MouseState.SkillActivate ret = GetComponent<MouseState>().GetSkillActive();
        GameObject Invorker = GetComponent<AtachMaster>().GetSkillInvoker();
        if (Invorker == null)
        {
            Debug.Log("nullです");
            return;
        }
        switch(ret)
        {
            case MouseState.SkillActivate.Yes:
                Debug.Log("発動者は"+Invorker+"です。");
                Invorker.GetComponent<CharacterStatus>().skill.SkillIsActive();
                break;
            case MouseState.SkillActivate.No:
                GetComponent<AtachMaster>().SetSkillInvoker(null);
                GetComponent<Player>().SetState(Player.PlayerStatus.None);
                GetComponent<MouseState>().SetSkillActive(MouseState.SkillActivate.None);
                break;
                
        }

    }
}
