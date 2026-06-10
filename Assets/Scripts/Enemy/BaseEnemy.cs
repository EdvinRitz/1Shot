using System;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    protected UnityEngine.AI.NavMeshAgent agent;
    public UnityEngine.AI.NavMeshAgent Agent { get => agent; }
    protected GameObject player;
    public GameObject Player { get => player; }
    protected bool isDead = false;
    public bool IsDead { get => isDead; }
    protected StateMachine stateMachine;

    public abstract void Start();

    // Update is called once per frame
    public abstract void Update();

    public abstract bool CanSeePlayer();
    
    public void Die()
    {
        isDead = true;
        stateMachine.ChangeState(new DieState(this));
    }
}
