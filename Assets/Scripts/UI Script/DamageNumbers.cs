using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using TMPro;

public class DamageNumbers : MonoBehaviour
{
    public GameObject dad;
    public Animator anim;
    public float damage;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    
    void Update()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = damage.ToString();

        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            Destroy(dad);
        }
    }
}
