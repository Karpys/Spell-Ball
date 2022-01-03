using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonBallAbility : AbilityBase
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform spawnPoint;
    
    public override void Ability()
    {
        GameObject ball = Instantiate(ballPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}