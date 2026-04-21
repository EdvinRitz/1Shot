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
        if(Vector3.Distance(gruntEnemy.transform.position, gruntEnemy.Player.transform.position) > gruntEnemy.playerCloseDistance)
        {
            gruntEnemy.Agent.isStopped = false;
            gruntEnemy.Agent.SetDestination(gruntEnemy.Player.transform.position);
        }
        else
        {
            gruntEnemy.Agent.isStopped = true;
            gruntEnemy.transform.rotation = Quaternion.RotateTowards(gruntEnemy.transform.rotation, Quaternion.LookRotation((gruntEnemy.Player.transform.position - gruntEnemy.transform.position).normalized), 180 * Time.deltaTime);
        }

        
        if((Vector3.Distance(gruntEnemy.transform.position, gruntEnemy.Player.transform.position) < gruntEnemy.attackDistance) && gruntEnemy.CanSeePlayer()) 
        {
            stateMachine.ChangeState(new GruntAttackState(gruntEnemy));
        }
    }

    public override void Exit()
    {
        
    }
}
