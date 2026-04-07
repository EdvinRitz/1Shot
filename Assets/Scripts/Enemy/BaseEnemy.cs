using System;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public UnityEngine.AI.NavMeshAgent Agent { get => agent; }
    public GameObject player;
    public GameObject Player { get => player; }
    public StateMachine stateMachine;

    public abstract void Start();

    // Update is called once per frame
    public abstract void Update();

    public abstract bool CanSeePlayer();
    
    public void Die()
    {
        stateMachine.ChangeState(new DieState(this));
    }
}
