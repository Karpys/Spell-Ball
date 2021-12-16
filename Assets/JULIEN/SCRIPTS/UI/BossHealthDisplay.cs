using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthDisplay : MonoBehaviour
{
    [SerializeField] private float maxHP;
    [SerializeField] private float currentHP;

    [SerializeField] private GameObject healthBar;

    private RectTransform _rectTransformBar;
    private float previousHP = float.MinValue;
    private void Start()
    {
        _rectTransformBar = (RectTransform) healthBar.transform;
    }

    private void Update()
    {
        if (previousHP != currentHP)
        {
            _rectTransformBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, ((400 * currentHP) / maxHP));
            previousHP = currentHP;
        }
    }

    public void SetMaxHealth(float _maxHealth)
    {
        maxHP = _maxHealth;
    }

    public void SetCurrentHealth(float _currentHealth)
    {
        currentHP = _currentHealth;
    }
}
