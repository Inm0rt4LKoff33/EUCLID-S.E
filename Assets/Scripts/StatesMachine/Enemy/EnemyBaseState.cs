using UnityEditor;
using UnityEngine;

public abstract class EnemyBaseState
{
    public abstract void EnterState(EnemyController state);
    public abstract void UpdateState(EnemyController state);
    public abstract void OnCollisionEnter(EnemyController state);
}