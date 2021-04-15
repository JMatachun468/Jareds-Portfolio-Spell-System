using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public GameObject spellBookMenu;
    [SerializeField]
    private List<Menu> openMenus;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void openSpellBook()
    {
        if(openMenus.Count > 0)
        {
            SpellBookMenu existCheck = (SpellBookMenu)openMenus.Find(x => x.GetComponent<SpellBookMenu>());
            if (existCheck)
            {
                openMenus.Remove(existCheck);
                Destroy(existCheck.gameObject);
                return;
            }
        }

        GameObject temp = Instantiate(spellBookMenu, GetComponentInParent<Canvas>().transform);
        SpellBookMenu spellBookMenuRef = temp.GetComponent<SpellBookMenu>();
        Debug.Log(spellBookMenuRef.gameObject.name);
        openMenus.Add(spellBookMenuRef);
    }

    public void closeLastMenu()
    {
        Destroy(openMenus[openMenus.Count - 1].gameObject);
        openMenus.RemoveAt(openMenus.Count - 1);
    }

    public bool menuOpen()
    {
        if(openMenus.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
