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
    public GameObject spellTrainerMenu;
    public List<Menu> openMenus;

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
        closeLastMenu();
        openMenus.Add(spellBookMenuRef);
    }

    public void openSpellTrainer()
    {
        if (openMenus.Count > 0)
        {
            SpellTrainerMenu existCheck = (SpellTrainerMenu)openMenus.Find(x => x.GetComponent<SpellTrainerMenu>());
            if (existCheck)
            {
                openMenus.Remove(existCheck);
                Destroy(existCheck.gameObject);
                return;
            }
        }

        GameObject temp = Instantiate(spellTrainerMenu, GetComponentInParent<Canvas>().transform);
        SpellTrainerMenu spellTrainerMenuRef = temp.GetComponent<SpellTrainerMenu>();
        closeLastMenu();
        openMenus.Add(spellTrainerMenuRef);
    }

    public void removeMenu(Menu menu)
    {
        openMenus.Remove(menu);
        Destroy(menu.gameObject);
    }

    public void closeLastMenu()
    {
        if (openMenus.Count == 0) return;
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
