using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyPortrait : MonoBehaviour
{
    [Header("GameObject References")]
    public Enemy enemy;
    public GameObject manaBar;
    public GameObject healthBar;

    [Header("Sliders and Texts")]
    [SerializeField]
    private Slider mana;
    [SerializeField]
    private Slider health;
    [SerializeField]
    private TextMeshProUGUI manaText;
    [SerializeField]
    private TextMeshProUGUI healthText;

    void Start()
    {
        mana = manaBar.GetComponentInChildren<Slider>();
        health = healthBar.GetComponentInChildren<Slider>();
        manaText = manaBar.GetComponentInChildren<TextMeshProUGUI>();
        healthText = healthBar.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemy) return;
        health.maxValue = enemy.GetMaxHealth();
        mana.maxValue = enemy.GetMaxMana();

        mana.value = enemy.GetCurrentMana();
        health.value = enemy.GetCurrentHealth();

        healthText.text = health.value.ToString() + " / " + health.maxValue.ToString();
        manaText.text = mana.value.ToString() + " / " + mana.maxValue.ToString();
    }
}
