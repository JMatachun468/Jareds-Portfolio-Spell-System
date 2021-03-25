using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerPortrait : MonoBehaviour
{
    [Header("GameObject References")]
    [SerializeField]
    private PlayerPawn player;
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
        player = GetComponentInParent<PlayerPawn>();
        mana = manaBar.GetComponentInChildren<Slider>();
        health = healthBar.GetComponentInChildren<Slider>();
        manaText = manaBar.GetComponentInChildren<TextMeshProUGUI>();
        healthText = healthBar.GetComponentInChildren<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        health.maxValue = player.getMaxHealth();
        mana.maxValue = player.getMaxMana();

        mana.value = player.getCurrentMana();
        health.value = player.getCurrentHealth();

        healthText.text = health.value.ToString() + " / " + health.maxValue.ToString();
        manaText.text = mana.value.ToString() + " / " + mana.maxValue.ToString();
    }
}
