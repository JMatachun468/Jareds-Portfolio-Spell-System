using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName ="ScriptableObjects/SpellScriptableObject", order = 1)]
public class SpellScriptableObject : ScriptableObject
{
    public string spellName;
    public Sprite abilitySprite;
    public Color spriteColor;
    public Material testMat;
    public MeshFilter spell3DModel;
    public AnimationClip castingAnimation;
    public SpellClassEnum characterClass;
    public SpellTypeEnum elementalClass;
    public int rank;
    public int manaCost;
    public int damage;
    public float rangeLimit;
    public float castTime;
    public float projectileSpeed;
    public bool isProjectile;
    public bool isDOT;
}
