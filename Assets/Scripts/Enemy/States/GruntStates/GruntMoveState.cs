using UnityEngine;
public class GruntMoveState : BaseState
{
    readonly GruntEnemy gruntEnemy;
    public GruntMoveState(GruntEnemy gruntEnemy)
    {
        this.gruntEnemy = gruntEnemy;
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
