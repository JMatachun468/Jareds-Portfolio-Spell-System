using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTrainer : Interactable
{
    public override void Interact()
    {
        UIManager.Instance.openSpellTrainer();
    }

    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        SpellTrainerMenu existCheck = (SpellTrainerMenu)UIManager.Instance.openMenus.Find(x => x.GetComponent<SpellTrainerMenu>());

        if(existCheck)
        {
            UIManager.Instance.removeMenu(existCheck);
        }
    }
}
