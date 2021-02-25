using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerPawn : Pawn
{
    Rigidbody rb;

 
    //movement related variables
    public float moveRate = 5;

    //camera related variables
    public float rotationRate = 90;
    public float mouseSensitivity = 15;
    public float pitchRate = 90;
    public Vector2 pitchRange = new Vector2(-89, 89);
    public bool InvertCamVerticle = true;


    //Jump related variables
    public bool IsGrounded = true;
    public float JumpSpeed;

    //Spell related Variables
    public SpellScriptableObject spellToCast;
    public GameObject target;
    public bool casting;
    public Coroutine spellBeingCasted;

    //bool for interact
    public bool InteractE = false;





    public void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }


    public void SetCamPitch(float value)
    {
        if (value == 0)
        {
            return;
        }

        if (InvertCamVerticle)
        {
            value *= -1;
        }

        float nextPitch = CameraControl.transform.rotation.eulerAngles.x;
        if (nextPitch > 180)
        {
            nextPitch -= 360;
        }

        float delta = (value * mouseSensitivity * pitchRate * Time.deltaTime);
        nextPitch = nextPitch + delta;

        // Restrain with in Riange
        if (nextPitch < pitchRange.x)
        {
            nextPitch = pitchRange.x;
        }

        if (nextPitch > pitchRange.y)
        {
            nextPitch = pitchRange.y;
        }

        Quaternion r = Quaternion.Euler(nextPitch, 0, 0);
        CameraControl.transform.localRotation = r;
    }

    public override void RotatePlayer(float value)
    {
        gameObject.transform.Rotate(Vector3.up * value * mouseSensitivity * rotationRate * Time.deltaTime);
    }

    public override void Move(float horizontal, float vertical)
    {
        
        if(horizontal != 0 || vertical != 0)
        {
            if(casting)
            {
                casting = false;
                StopCoroutine(spellBeingCasted);
                Debug.Log("coroutine should have stopped");
            }
        }
        if (!rb)
        {
            Debug.Log("waiting");
            return;
        }
        Vector3 direction = (gameObject.transform.forward * vertical) + (gameObject.transform.right * horizontal);
        direction = direction.normalized;

        rb.velocity = new Vector3(0,rb.velocity.y,0) + (direction * moveRate);
    }

    public override void Jump(bool s)
    {
        if (s)
        {
            Debug.Log("Jump in Player Pawn");
            if (IsGrounded)
            {
                Debug.Log("In Ground");
                rb.velocity = new Vector3(rb.velocity.x,JumpSpeed, rb.velocity.z);
                //rb.AddForce(Vector3.up * JumpSpeed);
                IsGrounded = false;
            }
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            IsGrounded = true;
        }
    }

    public override void Fire1(bool clicked)
    {
        if(clicked && !casting)
        {
            spellBeingCasted = StartCoroutine(CastSpell());
        }

    }

    public IEnumerator CastSpell()
    { 
        if(spellToCast.castTime > 0)
        {
            casting = true;
        }
        yield return new WaitForSeconds(spellToCast.castTime); //Waits for cast time, animation should play, then casts the spell

        GameObject castedSpell = new GameObject(); //empty gameobject
        castedSpell.transform.position = gameObject.transform.position; //set it to our position, In future will be a empty gameObject on the playerPawn for transforn.position purposes
        castedSpell.name = spellToCast.name;
        castedSpell.AddComponent<BaseSpell>(); //give it our spell script that handles everything in regards to the spell projectile
        castedSpell.GetComponent<BaseSpell>().PopulateVariables(spellToCast, target); //hands it the information required, the spell being cast and our target. All else will be handled on the BaseSpell script
        casting = false;
    }

}
