using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private SO_CharInfo charInfo;

    [SerializeField] private List<Transform> possibleSpawn;
    
    private PlayerInputManager playerInputManager;

    private void Awake()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
    }

    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            Transform spawnForThis = possibleSpawn[Random.Range(0, possibleSpawn.Count)];
            playerInputManager.playerPrefab = charInfo.playerPrefabForId[i];
            PlayerInput playerInput = playerInputManager.JoinPlayer(pairWithDevice:InputSystem.GetDeviceById(charInfo.playerControllerToId[i]));
            
            playerInput.gameObject.transform.SetPositionAndRotation(spawnForThis.position, charInfo.playerPrefabForId[i].transform.rotation);
        }
    }
}
