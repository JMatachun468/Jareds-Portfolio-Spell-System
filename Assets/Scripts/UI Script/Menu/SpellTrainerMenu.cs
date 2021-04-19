using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellTrainerMenu : Menu
{
    public GameObject spellGrid;

    [SerializeField]
    private PlayerPawn player;
    public GameObject spellSlotPrefab;
    [SerializeField]
    private SpellScriptableObject selectedSpell;
    public TextMeshProUGUI selectedSpellLabel;
    public Image selectedSpellImage;
    public List<SpellScriptableObject> learnableSpells;
    public List<SpellScriptableObject> unknownSpells;
    public List<GameObject> listedSpells;
    public Sprite emptySlotSprite;

    private void Start()
    {
        selectedSpellLabel.text = "";
        if(!selectedSpell)
        {
            selectedSpellImage.color = new Color32(123, 123, 123, 255);
            selectedSpellImage.sprite = emptySlotSprite;
        }
        player = GameObject.FindObjectOfType<PlayerPawn>();
        PopulateSpellList();
    }

    private void PopulateSpellList()
    {
        
        if(listedSpells.Count > 0)
        {
            foreach(GameObject go in listedSpells)
            {
                Destroy(go);
            }
            listedSpells.Clear();
        }

        selectedSpellLabel.text = "";
        selectedSpellImage.sprite = null;

        unknownSpells = new List<SpellScriptableObject>(learnableSpells);

        foreach (SpellScriptableObject spell in learnableSpells)
        {
            foreach (SpellScriptableObject knownSpell in player.learnedSpells)
            {
                if(spell == knownSpell)
                {
                    unknownSpells.Remove(spell);
                }
            }
        }

        foreach(SpellScriptableObject spell in unknownSpells)
        {
            GameObject spellSlot = Instantiate(spellSlotPrefab, spellGrid.transform);
            spellSlot.GetComponent<SpellTrainerSlot>().spellName.text = spell.spellName;
            spellSlot.GetComponent<SpellTrainerSlot>().spellInSlot = spell;
            listedSpells.Add(spellSlot);
            spellSlot.GetComponent<Button>().onClick.AddListener(() => SelectSpell(spellSlot.GetComponent<SpellTrainerSlot>()));
        }

        if (unknownSpells.Count > 0) SelectSpell(unknownSpells[0]);
    }

    public void LearnSpell()
    {
        if (selectedSpell == null) return;

        player.learnedSpells.Add(selectedSpell);
        selectedSpell = null;
        PopulateSpellList();
    }

    public void SelectSpell(SpellTrainerSlot selectedSlot)
    {
        selectedSpell = selectedSlot.spellInSlot;
        selectedSpellImage.sprite = selectedSpell.abilitySprite;
        selectedSpellLabel.text = selectedSpell.spellName;
        selectedSpellImage.color = selectedSpell.spriteColor;
    }
    public void SelectSpell(SpellScriptableObject spell)
    {
        selectedSpell = spell;
        selectedSpellImage.sprite = selectedSpell.abilitySprite;
        selectedSpellLabel.text = selectedSpell.spellName;
        selectedSpellImage.color = selectedSpell.spriteColor;
    }
}
