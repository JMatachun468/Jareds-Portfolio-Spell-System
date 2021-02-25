using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpell : MonoBehaviour
{
    [SerializeField]
    private SpellScriptableObject spell;
    [SerializeField]
    private MeshFilter projectileMeshFilter;
    [SerializeField]
    private MeshRenderer projectileMeshRenderer;
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private float projectileSpeed;

    private void Awake()
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        gameObject.AddComponent<SphereCollider>().isTrigger = true;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null) //if target somehow goes missing projectile will destroy itself
        {
            Destroy(gameObject);
        }
        trackTarget();
    }

    /// <summary>
    /// Pass the SpellScriptableObject into s, and the intended target to t
    /// </summary>
    /// <param name="s"></param>
    public void PopulateVariables(SpellScriptableObject s, GameObject t)
    {
        projectileMeshFilter = gameObject.GetComponent<MeshFilter>();
        projectileMeshRenderer = gameObject.GetComponent<MeshRenderer>();
        spell = s;
        target = t;
        PopulateVariables(spell.projectileSpeed, spell.spell3DModel.sharedMesh, spell.testMat);
    }
    /// <summary>
    /// Populates private variables on BaseSpell
    /// <para> speed is the projectiles speed, model is a mesh to be set on the projectile, spellMat is the material on the mesh, spell</para>
    /// </summary>
    /// <param name="t"></param>
    /// <param name="speed"></param>

    private void PopulateVariables(float speed, Mesh model, Material spellMat)
    {
        projectileSpeed = speed;
        projectileMeshFilter.mesh = model;
        projectileMeshRenderer.material = spellMat;
    }
    public virtual void trackTarget() //will follow target until it collides with them
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.transform.position, projectileSpeed);
    }

    public void OnTriggerEnter(Collider other) //checks to make sure collider is owned by the target and then executes code
    {
        Debug.Log("in trigger enter");
        if(other.gameObject == target)
        {
            Debug.Log(target.name + " has been hit by " + spell.spellName);
            Destroy(gameObject);
        }
    }

}
