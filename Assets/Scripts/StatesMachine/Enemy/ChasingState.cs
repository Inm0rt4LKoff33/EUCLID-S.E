using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ChasingState : EnemyBaseState
{
    public override void EnterState(EnemyController state)
    {
        Debug.Log("Start Chasing");
        state.agent.destination = state.target.position;
    }

    public override void OnCollisionEnter(EnemyController state)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(EnemyController state)
    {
        throw new System.NotImplementedException();
    }
}
