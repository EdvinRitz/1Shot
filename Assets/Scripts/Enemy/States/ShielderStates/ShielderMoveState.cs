using UnityEngine;

public class ShielderMoveState : BaseState
{
    ShielderEnemy shielderEnemy;

    public ShielderMoveState(ShielderEnemy shielderEnemy)
    {
        this.shielderEnemy = shielderEnemy;
    }

    public override void Enter()
    {
        
    }

    // Update is called once per frame
    public override void Perform()
    {
        WalkTowardsPlayer();
    }

    public void WalkTowardsPlayer()
    {
        shielderEnemy.Agent.SetDestination(shielderEnemy.Player.transform.position);
        
        if((Vector3.Distance(shielderEnemy.transform.position, shielderEnemy.Player.transform.position) < shielderEnemy.attackDistance) && shielderEnemy.CanSeePlayer()) 
        {
            //stateMachine.ChangeState(new GruntAttackState(gruntEnemy));
        }
    }

    public override void Exit()
    {
        
    }
}
