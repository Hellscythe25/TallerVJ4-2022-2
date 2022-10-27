using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] Slider hpSlider;
    [SerializeField] Slider shieldSlider;
    Health health;

    private void Start()
    {
        hpSlider.interactable = false;
        shieldSlider.interactable = false;
    }

    public void SetHealth(Health hp) 
    {
        health = hp;
        hpSlider.maxValue = health.GetMaxHealth();
        shieldSlider.maxValue = health.GetMaxShields();

        hpSlider.value = hp.CurrentHP();
        shieldSlider.value = hp.CurrentShields();
    }
    
}
