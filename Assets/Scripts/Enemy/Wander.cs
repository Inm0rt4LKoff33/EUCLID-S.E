using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : EnemyBaseState
{
    // State Variables
    float moveSpeed = 3F;
    float rotationSpeed = 100F;

    float range = 10F;

    public override void EnterState(EnemyController state)
    {
        state.moveSpeed = this.moveSpeed;
        state.rotationSpeed = this.rotationSpeed;
    }

    public override void OnCollisionEnter(EnemyController state)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(EnemyController state)
    {
        Debug.Log("Wandering");
        if (state.agent.remainingDistance <= state.agent.stoppingDistance)
        {
            Vector3 point;

            if (RandomPoint(state.transform.position, range, out point))
            {
                state.agent.SetDestination(point);
            }
        }
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0F, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
