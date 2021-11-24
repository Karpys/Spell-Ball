using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Manager_NumbPlayers: MonoBehaviour
{
    [SerializeField] public List<GameObject> listPlayers;

    PlayerInputManager manager;
    int id = 0;
    [HideInInspector]
    public int number = 0;
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    [HideInInspector]
    public bool unlockMove;

    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<PlayerInputManager>();
        id = 0;
        manager.playerPrefab = listPlayers[id];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddPlayer()
    {
        id = 1;
        if(number<3)
            number += 1;
        manager.playerPrefab = listPlayers[number];
        //AttribuatePlayerList();

        if (number >= 2)
        {
            AttribuatePlayerList();
        }
    }

    private void AttribuatePlayerList()
    {
       /* player1 = listPlayers[0];
        player2 = listPlayers[1];*/
       
        unlockMove = true;
    }

    public PlayerInputManager GetManager_NumPlayers()
    {
        return manager;
    }
}
