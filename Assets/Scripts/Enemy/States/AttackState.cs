using System.Collections;
using UnityEngine;

public class AttackState : BaseState
{
    float windupTimer;
    float winddownTimer;
    Vector3 dashDirection;
    Vector3 playerInitialPosition;

    float dashSpeed = 10f;

    public override void Enter()
    {
        dashDirection = (enemyMovingTowardsPlayer.Player.transform.position - enemyMovingTowardsPlayer.transform.position).normalized;
        playerInitialPosition = enemyMovingTowardsPlayer.Player.transform.position;
        windupTimer = 0.5f;
        winddownTimer = 0.5f;
        enemyMovingTowardsPlayer.Agent.isStopped = true;

    }

    public override void Perform()
    {
        windupTimer -= Time.deltaTime;
        if (windupTimer <= 0)
        {
            Debug.Log("attack made");
            if(Vector3.Distance(enemyMovingTowardsPlayer.transform.position, playerInitialPosition) >= 0.1f)
            {
                enemyMovingTowardsPlayer.transform.position += dashSpeed * Time.deltaTime * dashDirection;
            }

            if(Vector3.Distance(enemyMovingTowardsPlayer.transform.position, playerInitialPosition) <= 0.1f)
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
