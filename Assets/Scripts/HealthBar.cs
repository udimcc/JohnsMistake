using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider slider;


    private void Awake()
    {
        this.slider = this.GetComponent<Slider>();
        
        if (this.slider == null)
        {
            Debug.LogError("No slider found");
        }
    }

    public void SetMaxHealth(float maxHealth)
    {
        this.slider.maxValue = maxHealth;
    }

    public void SetHealth(float health)
    {
        this.slider.value = health;
    }
}
