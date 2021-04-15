using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBookMenu : Menu
{
    public SpellTypeEnum selectedSpellType;
    public GameObject lastSelectedButton;

    public List<GameObject> spellSlots;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void changeSpellClass(SpellTypeEnumReference spellType)
    {
        if (lastSelectedButton == spellType.gameObject) return;

        if(lastSelectedButton != null)
        {
            ColorBlock revertButtonColors = lastSelectedButton.GetComponent<Button>().colors;
            revertButtonColors.normalColor = revertButtonColors.highlightedColor;
            lastSelectedButton.GetComponent<Button>().colors = revertButtonColors;
            lastSelectedButton.transform.parent.GetComponent<Image>().color = new Color32(135, 135, 135, 255);
        }
        selectedSpellType = spellType.spellType;
        lastSelectedButton = spellType.gameObject;


        ColorBlock newButtonColors = lastSelectedButton.GetComponent<Button>().colors;
        newButtonColors.normalColor = newButtonColors.selectedColor;
        lastSelectedButton.GetComponent<Button>().colors = newButtonColors;
        lastSelectedButton.transform.parent.GetComponent<Image>().color = new Color32(255, 230, 42, 255);
    }
}
