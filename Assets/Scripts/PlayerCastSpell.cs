using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCastSpell : MonoBehaviour
{
    public SpellScriptableObject spellToCast;
    public GameObject target;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("hit space");
            StartCoroutine(CastSpell());
        }
    }
    private void FixedUpdate()
    {

    }

    public IEnumerator CastSpell()
    {
        yield return new WaitForSeconds(spellToCast.castTime); //Waits for cast time, animation should play, then casts the spell

        GameObject castedSpell = new GameObject(); //empty gameobject
        castedSpell.transform.position = gameObject.transform.position; //set it to our position, In future will be a empty gameObject on the playerPawn for transforn.position purposes
        castedSpell.name = spellToCast.name;
        castedSpell.AddComponent<BaseSpell>(); //give it our spell script that handles everything in regards to the spell projectile
        castedSpell.GetComponent<BaseSpell>().PopulateVariables(spellToCast, target); //hands it the information required, the spell being cast and our target. All else will be handled on the BaseSpell script
    }




}
