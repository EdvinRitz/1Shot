using UnityEngine;
public class GruntMoveState : BaseState
{
    public GruntMoveState(GruntEnemy gruntEnemy)
    {
        this.gruntEnemy = gruntEnemy;
    }

    public override void Enter()
    {
        
    }
    public override void Perform()
    {
        walkTowardsPlayer();
    }
    public void walkTowardsPlayer()
    {
        gruntEnemy.Agent.SetDestination(gruntEnemy.Player.transform.position);
        
        if((Vector3.Distance(gruntEnemy.transform.position, gruntEnemy.Player.transform.position) < gruntEnemy.attackDistance) && gruntEnemy.CanSeePlayer()) 
        {
            stateMachine.ChangeState(new GruntAttackState(gruntEnemy));
        }
    }

    public override void Exit()
    {
        
    }
}
