using System.Collections;
using UnityEngine;

public class AttackState : BaseState
{
    float windupTimer;
    float winddownTimer;
    Vector3 dashDirection;
    Vector3 playerInitialPosition;
    Vector3 enemyInitialPostion;
    float dashDistanceTarget;

    float dashSpeed = 10f;

    public override void Enter()
    {
        dashDirection = (enemyMovingTowardsPlayer.Player.transform.position - enemyMovingTowardsPlayer.transform.position).normalized;
        playerInitialPosition = enemyMovingTowardsPlayer.Player.transform.position;
        enemyInitialPostion = enemyMovingTowardsPlayer.transform.position;
        dashDistanceTarget = Vector3.Distance(enemyMovingTowardsPlayer.transform.position, playerInitialPosition);
        windupTimer = 0.5f;
        winddownTimer = 1f;
        enemyMovingTowardsPlayer.Agent.isStopped = true;

    }

    public override void Perform()
    {
        windupTimer -= Time.deltaTime;
        if (windupTimer <= 0)
        {
            Debug.Log("attack made");
            if(Vector3.Distance(enemyMovingTowardsPlayer.transform.position, enemyInitialPostion) < dashDistanceTarget)
            {
                enemyMovingTowardsPlayer.transform.position += dashSpeed * Time.deltaTime * dashDirection;
            }

            if(Vector3.Distance(enemyMovingTowardsPlayer.transform.position, enemyInitialPostion) >= dashDistanceTarget)
            {
                winddownTimer -= Time.deltaTime;
                if(winddownTimer <= 0)
                {
                    stateMachine.ChangeState(new MoveTowardsPlayerState());
                }
                
            }
            //stateMachine.ChangeState(new MoveTowardsPlayerState());
        }
        
    }
    public override void Exit()
    {
        enemyMovingTowardsPlayer.Agent.isStopped = false;
    }
}
