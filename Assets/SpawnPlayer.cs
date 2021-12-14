using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private bool _spawnPlayer;
    [SerializeField] private List<GameObject> _players;
    
    [SerializeField] private List<GameObject> playersLifeDisplay;
    void Start()
    {
        if (_spawnPlayer)
        {
            for (int i = 0; i < _players.Count; i++)
            {
                GameObject player = Instantiate(_players[i], transform.position, transform.rotation);
                
                PlayerHealthDisplay playerHealthDisplay = playersLifeDisplay[i].GetComponent<PlayerHealthDisplay>();
                playerHealthDisplay.SetPlayer(player);
            }
        }
    }

    
}
