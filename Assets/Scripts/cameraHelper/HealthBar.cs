using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private const float DAMAGED_HEALTH_FADE_TIMER_MAX = 1f;
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public Image damagedBar;
    public Color damagedColor;
    private float damagedHealthFadeTimer;

    private void Update() 
    {
        if(damagedColor.a > 0)
        {
            damagedHealthFadeTimer -= Time.deltaTime;
            if(damagedHealthFadeTimer < 0)
            {
                float fadeAmount = 5f;
                damagedColor.a -= fadeAmount * Time.deltaTime;
                damagedBar.color = damagedColor;
            }
        }
    }

    private void Awake()
    {
        damagedColor.a = 0f;
        damagedColor = damagedBar.color;
        damagedBar.color = damagedColor;
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        if(damagedColor.a <= 0)
        {
            damagedBar.fillAmount = health;
        }
        damagedColor.a = 1;
        damagedBar.color = damagedColor;
        damagedHealthFadeTimer = DAMAGED_HEALTH_FADE_TIMER_MAX;
    }
}
