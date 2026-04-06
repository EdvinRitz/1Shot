using UnityEngine;
public class GruntMoveState : BaseState
{

    public override void Enter()
    {
        
    }
    public override void Perform()
    {
        walkTowardsPlayer();
    }
    public void walkTowardsPlayer()
    {
        enemyMovingTowardsPlayer.Agent.SetDestination(enemyMovingTowardsPlayer.Player.transform.position);
        
        if((Vector3.Distance(enemyMovingTowardsPlayer.transform.position, enemyMovingTowardsPlayer.Player.transform.position) < enemyMovingTowardsPlayer.attackDistance) && enemyMovingTowardsPlayer.CanSeePlayer()) 
        {
            Debug.Log("attack State activated");
            stateMachine.ChangeState(new GruntAttackState());
        }
        //Vector3.Distance(enemyMovingTowardsPlayer.transform.position, enemyMovingTowardsPlayer.Player.transform.position)
        
    }

    public override void Exit()
    {
        
    }
}
