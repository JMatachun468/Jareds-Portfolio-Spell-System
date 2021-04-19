using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject hint;
    public virtual void Interact()
    {

    }
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerPawn>())
        {
            hint.SetActive(true);
            other.GetComponent<PlayerPawn>().targetInteractable = this;
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerPawn>())
        {
            hint.SetActive(false);
            other.GetComponent<PlayerPawn>().targetInteractable = null;
        }
    }
}
