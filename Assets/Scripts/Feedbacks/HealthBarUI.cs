using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private EntityHealth entityHealth;
    [SerializeField] private Image healthBarImage;

    private CanvasGroup canvasGroup;

    private void OnEnable()
    {
        entityHealth.OnHealhIncreased += EntityHealth_OnHealhIncreased;
        entityHealth.OnHealhDecreased += EntityHealth_OnHealhDecreased;
    }

    private void OnDisable()
    {
        entityHealth.OnHealhIncreased -= EntityHealth_OnHealhIncreased;
        entityHealth.OnHealhDecreased -= EntityHealth_OnHealhDecreased;
    }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        canvasGroup.alpha = 0f;
    }

    private void UpdateHealthBar(int maxHealth, int health)
    {
        canvasGroup.alpha = 1f;

        float ratio = (float)health / maxHealth;
        SetHealthBarFillAmount(ratio);
    }

    private void SetHealthBarFillAmount(float fillAmount) => healthBarImage.fillAmount = fillAmount;

    private void EntityHealth_OnHealhIncreased(object sender, EntityHealth.OnHealthEventArgs e)
    {
        UpdateHealthBar(e.maxHealth, e.health);
    }

    private void EntityHealth_OnHealhDecreased(object sender, EntityHealth.OnHealthEventArgs e)
    {
        UpdateHealthBar(e.maxHealth, e.health);
    }
}
