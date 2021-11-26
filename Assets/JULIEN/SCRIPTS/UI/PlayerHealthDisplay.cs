using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _healthBarParent;
    
    public GameObject _player;

    private void Start()
    {
        InitPlayerHealth();
    }

    public void InitPlayerHealth()
    {
        for (int i = 0; i < _healthBarParent.transform.childCount; i++)
        {
            Transform bar = _healthBarParent.transform.GetChild(i);
            Image image = bar.GetComponent<Image>();
            image.color = Color.red;
        }
        
        if (_player == null)
        {
            gameObject.SetActive(false);
        }
    }

    
}
