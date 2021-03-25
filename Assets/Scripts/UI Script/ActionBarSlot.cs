using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ActionBarSlot : MonoBehaviour, IDragHandler , IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    private PlayerPawn player;
    private GraphicRaycaster raycaster;
    public SpellScriptableObject spellInSlot;
    private bool dragging;
    void Start()
    {
        player = GetComponentInParent<PlayerPawn>();
        raycaster = GetComponentInParent<GraphicRaycaster>();
        gameObject.GetComponent<Image>().sprite = spellInSlot.abilitySprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateIcon()
    {
        if(spellInSlot == null)
        {
            gameObject.GetComponent<Image>().sprite = null;
            return;
        }
        gameObject.GetComponent<Image>().sprite = spellInSlot.abilitySprite;
    }

    public void CastSpell()
    {
        if(spellInSlot == null)
        {
            return;
        }
        player.SpellRestrictionCheck(spellInSlot, player.target);
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        player.draggingAbility = true;
        Debug.Log("start of drag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("end of drag");

        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        List<RaycastResult> results = new List<RaycastResult>();

        pointerData.position = Mouse.current.position.ReadValue();
        raycaster.Raycast(pointerData, results);

        foreach(RaycastResult result in results)
        {
            if(result.gameObject.GetComponent<ActionBarSlot>())
            {
                ActionBarSlot ABS = result.gameObject.GetComponent<ActionBarSlot>();

                SpellScriptableObject temp = spellInSlot;

                spellInSlot = ABS.spellInSlot;
                ABS.spellInSlot = temp;

                updateIcon();
                ABS.updateIcon();

                break;
            }
        }

        if(results.Count == 0)
        {
            spellInSlot = null;
            updateIcon();
        }
        player.draggingAbility = false;
    }

    public void OnDrag(PointerEventData eventData)
    {

    }
}
