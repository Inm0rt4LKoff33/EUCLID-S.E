using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderingState : EnemyBaseState
{
    // Randomize numbers
    int rotationTime = Random.Range(1, 3);
    int rotationWait = Random.Range(1, 4);
    int rotationLR = Random.Range(1, 2);
    int walkWait = Random.Range(1, 5);
    int walkTime = Random.Range(1, 3);

    public float moveSpeed = 3F;
    public float rotationSpeed = 100F;

    public override void EnterState(EnemyController state)
    {
        throw new System.NotImplementedException();
    }

    public override void OnCollisionEnter(EnemyController state)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(EnemyController state)
    {
    }
}
