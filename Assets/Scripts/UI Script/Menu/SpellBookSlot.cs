using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class SpellBookSlot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    private PlayerPawn player;
    private GraphicRaycaster raycaster;
    public SpellScriptableObject spellInSlot;
    private RectTransform rt;
    [SerializeField]
    private GameObject draggingIcon;
    public Vector3 startingPos;
    public Vector2 mousePos;
    private bool dragging;
    public Image panel;
    private ActionBarSlot lastDraggedOverSlot;
    public Sprite emptySlotIcon;
    void Start()
    {
        player = GetComponentInParent<PlayerPawn>();
        raycaster = GetComponentInParent<GraphicRaycaster>();
        rt = GetComponent<RectTransform>();
        if(spellInSlot) gameObject.GetComponent<Image>().sprite = spellInSlot.abilitySprite;
        startingPos = rt.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            Dragging();
        }
        if (spellInSlot == null)
        {
            ColorBlock temp = ColorBlock.defaultColorBlock;
            temp.pressedColor = Color.white;
            gameObject.GetComponent<Button>().colors = temp;
            gameObject.GetComponent<Image>().color = new Color32(214, 158, 118, 255);
            gameObject.GetComponent<Image>().sprite = emptySlotIcon;
        }
        else
        {
            ColorBlock temp = ColorBlock.defaultColorBlock;
            temp.pressedColor = new Color32(85, 85, 85, 255);
            temp.normalColor = spellInSlot.spriteColor;
            temp.highlightedColor = spellInSlot.spriteColor;
            gameObject.GetComponent<Button>().colors = temp;
            gameObject.GetComponent<Image>().color = spellInSlot.spriteColor;
        }
    }

    public void updateIcon() //used for updating icon after moving an ability around
    {
        if (spellInSlot == null)
        {
            gameObject.GetComponent<Image>().sprite = null;
            return;
        }
        gameObject.GetComponent<Image>().sprite = spellInSlot.abilitySprite;
    }

    public void Dragging()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        List<RaycastResult> results = new List<RaycastResult>();

        pointerData.position = Mouse.current.position.ReadValue();
        raycaster.Raycast(pointerData, results);

        bool foundSlot = false;
        foreach (RaycastResult result in results)
        {
            if (result.gameObject.GetComponent<ActionBarSlot>())
            {
                foundSlot = true;
                if (lastDraggedOverSlot != null)
                {
                    lastDraggedOverSlot.panel.color = Color.black;
                }

                lastDraggedOverSlot = result.gameObject.GetComponent<ActionBarSlot>();
                lastDraggedOverSlot.panel.color = new Color32(255, 254, 183, 255);
            }
        }

        Debug.Log(foundSlot);
        if (!foundSlot)
        {
            if (lastDraggedOverSlot != null)
            {
                lastDraggedOverSlot.panel.color = Color.black;
            }
        }

        draggingIcon.transform.position = Mouse.current.position.ReadValue();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (spellInSlot == null) return;
        dragging = true;
        player.draggingAbility = true;

        draggingIcon = Instantiate(new GameObject("draggingIcon"), GetComponentInParent<Canvas>().transform);
        draggingIcon.AddComponent<Image>();
        draggingIcon.GetComponent<Image>().color = spellInSlot.spriteColor;
        draggingIcon.GetComponent<Image>().sprite = spellInSlot.abilitySprite;
        draggingIcon.GetComponent<Image>().raycastTarget = false;
        draggingIcon.GetComponent<RectTransform>().sizeDelta = new Vector2(75, 75);

    }



    public void OnEndDrag(PointerEventData eventData)
    {
        if (spellInSlot == null) return;
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        List<RaycastResult> results = new List<RaycastResult>();

        pointerData.position = Mouse.current.position.ReadValue();
        raycaster.Raycast(pointerData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.GetComponent<ActionBarSlot>())
            {
                ActionBarSlot ABS = result.gameObject.GetComponent<ActionBarSlot>();

                ABS.spellInSlot = spellInSlot;

                updateIcon();
                ABS.updateIcon();

                break;
            }
        }

        if(lastDraggedOverSlot) lastDraggedOverSlot.panel.color = Color.black;
        rt.transform.localPosition = startingPos;
        Destroy(draggingIcon);
        player.draggingAbility = false;
        dragging = false;
        updateIcon();
    }

    public void OnDrag(PointerEventData eventData)
    {

    }
}
