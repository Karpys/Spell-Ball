using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerHealthDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _healthBarParent;
    
    public GameObject _player;

    private Manager_Life _playerLife; 
    
    private void Start()
    {
        InitPlayerHealth();
    }

    public void SetPlayer(GameObject player)
    {
        _player = player;
        InitPlayerHealth();
    }

    private void InitPlayerHealth()
    {
        if (_player == null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            _playerLife = _player.GetComponent<Manager_Life>();
            
            DrawHealth();
            
            _playerLife.OnDamage.AddListener(DrawHealth);
            _playerLife.OnDamage.AddListener(ShakePlayerVisual);
            _playerLife.OnHeal.AddListener(DrawHealth);

            gameObject.SetActive(true);
        }
    }

    public void DrawHealth()
    {
        for (int i = 0; i < _healthBarParent.transform.childCount; i++)
        {
            Transform bar = _healthBarParent.transform.GetChild(i);
            Image image = bar.GetComponent<Image>();
            print("Le joueur " + gameObject.name + " à " + _playerLife.GetCurentLife() + " points de vie");
            if (_playerLife.GetCurentLife() > i)
            {
                image.color = Color.red;
            }
            else
            {
                image.color = Color.gray;
            }
        }
    }

    public void ShakePlayerVisual()
    {
        StartCoroutine(ShakeVisual(.1f, 20f));
    }

    
    private IEnumerator ShakeVisual(float _shakeTime, float shakeRange)
    {
        float elapsed = 0.0f;
        Quaternion originalRotation = transform.rotation;
 
        while (elapsed < _shakeTime)
        {
 
            elapsed += Time.deltaTime;
            float z = Random.value * shakeRange - (shakeRange /2);
            transform.eulerAngles = new Vector3(originalRotation.x, originalRotation.y, originalRotation.z + z); 
            yield return null;
        }
 
        transform.rotation = originalRotation;
    }
    
}
