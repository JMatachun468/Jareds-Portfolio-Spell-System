using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class PlayerPawn : Pawn
{
    Rigidbody rb;

    [Header("Player Stats")]
    public int strength;
    public int stamina;
    public int intellect;
    public int spirit;
    public int agility;
    
    [Header("UI Variables")]
    public GameObject PlayerUICanvas; //reference to canvas for UI
    public GameObject SpellTimerUIPrefab; //prefab for cast timer bar
    private GameObject currentUITimer; //reference to current cast timer prefab in scene
    public GameObject damageNumberPrefab; //reference to prefab for our damage numbers
    public bool draggingAbility = false;

    [Header("ActionBarSlots")]
    public ActionBarSlot slot1;
    public ActionBarSlot slot2;
    public ActionBarSlot slot3;
    public ActionBarSlot slot4;
    public ActionBarSlot slot5;
    public ActionBarSlot slot6;
    public ActionBarSlot slot7;
    public ActionBarSlot slot8;
    public ActionBarSlot slot9;
    public ActionBarSlot slot0;

    //movement related variables
    [Header("Movement Variables")]
    public float moveRate = 5;

    [Header("Camera Variables")]
    public float rotationRate = 90;
    public float mouseSensitivity = 15;
    public float pitchRate = 90;
    public Vector2 pitchRange = new Vector2(-89, 89); //clamp for camera controls
    public bool InvertCamVerticle = true;

    [Header("Jump Variables")]
    public bool IsGrounded = true; //ground check
    public float JumpSpeed;

    [Header("Spell Variables")]
    public SpellScriptableObject spell; //current selected spell, will have changed funtionality in future
    public bool casting; //are we casting
    public Coroutine spellBeingCasted; //stored coroutine for spell cast
    public Coroutine spellUITimer; //stored coroutine for UI timer
    public float GlobalCoolDown; //GCD is a buffer between ability casts that begins countdown immediately after casting a spell, mostly effects instant cast or low cast time abilities
    public float GCDTimer; //timer for the GlobalCoolDown

    [Header("Targeting Variables")]
    public GameObject target; //our currently selected target
    public GameObject currentEnemyPortrait; //UI portrait of our currently targeted enemy
    public GameObject currentTargetIndicator; //reference to current target indicator prefab in scene above target
    public GameObject enemyPortraitPrefab; //prefab for enemy portraits
    public GameObject targetIndicatorPrefab; //prefab above target

    [Header("Animation Variables")]
    public int rotationSlerpSpeed;
    public Animator animator;
    public GameObject playerModel;

    //bool for interact
    public bool InteractE = false;

    public void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();

        maxHealth = stamina * 10;
        currentHealth = maxHealth;
        maxMana = intellect * 10;
        currentMana = maxMana;
    }

    public void Update()
    {
        if(GCDTimer > 0)
        {
            GCDTimer -= Time.deltaTime;
        }
        if(target == null)
        {
            Destroy(currentEnemyPortrait);
            Destroy(currentTargetIndicator);
        }

        Vector3 localVel = rb.transform.InverseTransformDirection(rb.velocity);
        animator.SetFloat("playerVelocityZ", localVel.z);
        animator.SetFloat("playerVelocityX", localVel.x);

    }

    public void SetCamPitch(float value) //pitches the camera up and down seperate from the player model
    {
        if (value == 0)
        {
            return;
        }

        if (InvertCamVerticle)
        {
            value *= -1;
        }

        if(!Mouse.current.leftButton.isPressed)//checks that we are clicking down to pitch camera
        {
            return;
        }

        if(EventSystem.current.IsPointerOverGameObject())//Checks if we are over a UI element when we click
        {
            return;
        }

        if (draggingAbility) //bool for whether or not we are moving abilities on the bar
        {
            return;
        }

        float nextPitch = CameraControl.transform.rotation.eulerAngles.x;
        if (nextPitch > 180)
        {
            nextPitch -= 360;
        }

        float delta = (value * mouseSensitivity * pitchRate * Time.deltaTime);
        nextPitch = nextPitch + delta;

        // Restrain with in Range
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

    public override void RotatePlayer(float value) //rotates the whole player model
    {
        if (!Mouse.current.leftButton.isPressed) //checks to see if we are clicking down to rotate the camera
        {
            return;
        }
        if (EventSystem.current.IsPointerOverGameObject())//Checks if we are over a UI element when we click
        {
            return;
        }
        if (draggingAbility) //bool for whether or not we are moving abilities on the bar
        {
            return;
        }

        gameObject.transform.Rotate(Vector3.up * value * mouseSensitivity * rotationRate * Time.deltaTime);
    }

    public override void Move(float horizontal, float vertical) //okay movement code, could be overhauled
    {
        if(horizontal != 0 || vertical != 0)
        {
            if(casting) //if we are casting something and we move, cancel the casted spell
            {
                animator.SetBool("castingSpell", false);
                casting = false;
                StopCoroutine(spellBeingCasted); //stops coroutines for our spell cast and the associated UI prefab
                StopCoroutine(spellUITimer);
                Destroy(currentUITimer); //destroys our UI prefab
            }
        }
        if (!rb)
        {
            return;
        }
        Vector3 direction = (gameObject.transform.forward * vertical) + (gameObject.transform.right * horizontal);
        direction = direction.normalized;

        rb.velocity = new Vector3(0,rb.velocity.y,0) + (direction * moveRate);
    }

    public override void Jump(bool s) //bad jump code
    {
        if (s)
        {
            if (IsGrounded)
            {
                rb.velocity = new Vector3(rb.velocity.x,JumpSpeed, rb.velocity.z);
                //rb.AddForce(Vector3.up * JumpSpeed);
                IsGrounded = false;
            }
        }
    }

    public override void Close(bool escape) //You can hit escape to untarget an enemy
    {
        if(escape)
        {
            if(target)
            {
                target = null;
                Destroy(currentTargetIndicator);
                Destroy(currentEnemyPortrait);
            }
        }
    }

    public override void Fire1(bool clicked, Vector2 mousePos) //Checks mouse clicks for targets
    {
        if(clicked)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(mousePos); //convert screen point to ray into world

            if (Physics.Raycast(ray, out hit)) //checks for hit
            {
                Transform objectHit = hit.transform;

                if (objectHit.gameObject.GetComponent<Enemy>()) //if it has an enemy component, which is the parent of all enemies
                {
                    target = objectHit.gameObject; //stores our target
                    if(currentTargetIndicator) //checks if we already had a targetIndicatorPrefab and destroys it if we do
                    {
                        Destroy(currentTargetIndicator);
                        Destroy(currentEnemyPortrait);
                    }
                    currentTargetIndicator = Instantiate(targetIndicatorPrefab, target.transform); //instantiates a new targetIndicatorPrefab parented to our target
                    currentEnemyPortrait = Instantiate(enemyPortraitPrefab, PlayerUICanvas.transform);
                    currentEnemyPortrait.GetComponent<EnemyPortrait>().enemy = target.GetComponent<Enemy>();
                }
            }
        }
    }

    public override void ActionBar1(bool input)
    {
        if (input) //checks to make sure we aren't already casting something
        {
            SpellRestrictionCheck(slot1.spellInSlot, target);
        }
    }

    public override void ActionBar2(bool input)
    {
        if(input)
        {
            SpellRestrictionCheck(slot2.spellInSlot, target);
        }
    }

    public override void ActionBar3(bool input)
    {
        if(input)
        {
            SpellRestrictionCheck(slot3.spellInSlot, target);
        }
    }

    public override void ActionBar4(bool input)
    {
        if (input)
        {
            SpellRestrictionCheck(slot4.spellInSlot, target);
        }
    }

    public override void ActionBar5(bool input)
    {
        if (input)
        {
            SpellRestrictionCheck(slot5.spellInSlot, target);
        }
    }

    public override void ActionBar6(bool input)
    {
        if (input)
        {
            SpellRestrictionCheck(slot6.spellInSlot, target);
        }
    }

    public override void ActionBar7(bool input)
    {
        if (input)
        {
            SpellRestrictionCheck(slot7.spellInSlot, target);
        }
    }

    public override void ActionBar8(bool input)
    {
        if (input)
        {
            SpellRestrictionCheck(slot8.spellInSlot, target);
        }
    }

    public override void ActionBar9(bool input)
    {
        if (input)
        {
            SpellRestrictionCheck(slot9.spellInSlot, target);
        }
    }

    public override void ActionBar0(bool input)
    {
        if (input)
        {
            SpellRestrictionCheck(slot0.spellInSlot, target);
        }
    }


    public void SpellRestrictionCheck(SpellScriptableObject spellToCast, GameObject startingTarget)
    {
        if(spellToCast == null)
        {
            return;
        }

        if (GCDTimer > 0)
        {
            return;
        }

        if (!target) //checks for target, if none exits method
        {
            return;
        }

        if (!casting) //checks to make sure we aren't already casting something
        {
            if(currentMana < spellToCast.manaCost)
            {
                return;
            }
            spellBeingCasted = StartCoroutine(CastSpell(spellToCast, startingTarget)); //stores our currently casted spell coroutine for later reference
            Debug.Log(spellBeingCasted);
        }

    }

    public IEnumerator CastSpell(SpellScriptableObject spellToCast, GameObject startingTarget)
    {
        SpellScriptableObject originalSpell = spellToCast;
        GameObject originalTarget = startingTarget.gameObject;
        GCDTimer = GlobalCoolDown;
        if(spellToCast.castTime > 0)
        {
            casting = true;
            animator.SetBool("castingSpell", true);
            animator.Play("Base Layer.castingSpell");
            spellUITimer = StartCoroutine(CastSpellTimer(spellToCast.castTime));
        }

        yield return new WaitForSeconds(spellToCast.castTime); //Waits for cast time, animation should play, then casts the spell

        currentMana -= spellToCast.manaCost;
        GameObject castedSpell = new GameObject(); //empty gameobject
        castedSpell.transform.position = gameObject.transform.position; //set it to our position, In future will be a empty gameObject on the playerPawn for transforn.position purposes
        castedSpell.name = originalSpell.name;
        castedSpell.AddComponent<BaseSpell>(); //give it our spell script that handles everything in regards to the spell projectile
        castedSpell.GetComponent<BaseSpell>().PopulateVariables(originalSpell, originalTarget, damageNumberPrefab); //hands it the information required, the spell being cast and our target. All else will be handled on the BaseSpell script
        casting = false;
        animator.SetBool("castingSpell", false);
    }

    public IEnumerator CastSpellTimer(float CastTimer)
    {
        currentUITimer = Instantiate(SpellTimerUIPrefab, PlayerUICanvas.transform); //Instantiate our SpellCastTimerPrefab, and parent it to our canvas for player UI
        currentUITimer.gameObject.GetComponent<Slider>().maxValue = CastTimer; //Sets our sliders max value to our cast time
        for (float x = 0; x < CastTimer;) //For loop for the length of our cast time
        {
            currentUITimer.gameObject.GetComponent<Slider>().value = x; //setting our slider value equal to x
            x += Time.deltaTime; //x increases in relation to time.deltaTime
            yield return null; //yields the coroutine til next frame to acquire an updated time.deltaTime
        }
        Destroy(currentUITimer); //destroy our prefab after we are done with it
        yield return true; //exit the coroutine
    }

    private void OnCollisionStay(Collision other) //bad floor detection
    {
        if (other.gameObject.tag == "Ground")
        {
            IsGrounded = true;
        }
    }

}
