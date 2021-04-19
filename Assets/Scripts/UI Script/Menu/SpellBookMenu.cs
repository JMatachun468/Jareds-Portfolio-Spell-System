using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBookMenu : Menu
{
    public SpellTypeEnum selectedSpellType;
    public GameObject lastSelectedButton;
    private PlayerPawn player;
    private int currentSpellBookPage = 0;
    [SerializeField]
    private int maxSpellBookPages = 0;
    public List<GameObject> spellSlots;


    private void Start()
    {
        player = GetComponentInParent<PlayerPawn>();
        UpdateSpellBook();
        Debug.Log(currentSpellBookPage);
    }

    private void Update()
    {
        
    }
    
    public void PreviousPage()
    {
        if (currentSpellBookPage == 0) return;
            currentSpellBookPage--;
        Debug.Log(currentSpellBookPage);
        UpdateSpellBook();
    }

    public void NextPage()
    {
        if (currentSpellBookPage == maxSpellBookPages) return;
        currentSpellBookPage++;
        Debug.Log(currentSpellBookPage);
        UpdateSpellBook();
    }
    
    public void ChangeSpellClass(SpellTypeEnumReference spellType)
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

        Debug.Log(currentSpellBookPage);
        UpdateSpellBook();
    }

    public void UpdateSpellBook()
    {
        foreach (GameObject go in spellSlots)
        {
            go.GetComponent<SpellBookSlot>().spellInSlot = null;
            go.GetComponent<SpellBookSlot>().spellName.text = "";
            go.GetComponent<SpellBookSlot>().spellRank.text = "";
        }

        List<SpellScriptableObject> spellsToDisplay = player.learnedSpells.FindAll(s => s.elementalClass == selectedSpellType);

        maxSpellBookPages = (int)Mathf.Ceil(spellsToDisplay.Count / 8f) - 1;

        int slotIterator = 0;
        int size = (currentSpellBookPage * 8) + spellsToDisplay.Count;
        int startingPoint = (currentSpellBookPage * 8);
        for (int index = 0 + startingPoint;index < size; index++)
        {
            spellSlots[slotIterator].GetComponent<SpellBookSlot>().spellInSlot = spellsToDisplay[index];
            spellSlots[slotIterator].GetComponent<SpellBookSlot>().spellName.text = spellsToDisplay[index].spellName;
            spellSlots[slotIterator].GetComponent<SpellBookSlot>().spellRank.text = "Rank " + spellsToDisplay[index].rank;
            spellSlots[slotIterator].GetComponent<SpellBookSlot>().updateIcon();
            slotIterator++;
        }
    }
}
