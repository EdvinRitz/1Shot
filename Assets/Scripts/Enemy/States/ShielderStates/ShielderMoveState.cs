using UnityEngine;

public class ShielderMoveState : BaseState
{
    readonly ShielderEnemy shielderEnemy;

    public ShielderMoveState(ShielderEnemy shielderEnemy)
    {
        this.shielderEnemy = shielderEnemy;
    }

    public override void Enter()
    {
        
    }

    public override void Perform()
    {
        WalkTowardsPlayer();
    }

    public void WalkTowardsPlayer()
    {
        if (Vector3.Distance(shielderEnemy.transform.position, shielderEnemy.Player.transform.position) <= shielderEnemy.stopDistance)
        {
            shielderEnemy.Agent.isStopped = true;
            shielderEnemy.transform.rotation = Quaternion.RotateTowards(shielderEnemy.transform.rotation, Quaternion.LookRotation((shielderEnemy.Player.transform.position - shielderEnemy.transform.position).normalized), 40 * Time.deltaTime);
        }
        else
        {
            shielderEnemy.Agent.isStopped = false;
            shielderEnemy.Agent.SetDestination(shielderEnemy.Player.transform.position);
        }

        if((Vector3.Distance(shielderEnemy.transform.position, shielderEnemy.Player.transform.position) < shielderEnemy.attackDistance) && shielderEnemy.CanSeePlayer()) 
        {
            //TODO switch to attack state
        }
    }

    public override void Exit()
    {
        
    }
}
