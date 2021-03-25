﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName ="ScriptableObjects/SpellScriptableObject", order = 1)]
public class SpellScriptableObject : ScriptableObject
{
    public string spellName;
    public Sprite abilitySprite;
    public Material testMat;
    public MeshFilter spell3DModel;
    public float damage;
    public float castTime;
    public float projectileSpeed;
    public bool isProjectile;
    public bool isDOT;
}
