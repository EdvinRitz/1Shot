using System.Collections;
using UnityEngine;

public class GruntAttackState : BaseState
{
    GruntEnemy gruntEnemy;
    float windupTimer;
    float winddownTimer;
    Vector3 dashDirection;
    Vector3 playerInitialPosition;
    Vector3 enemyInitialPostion;
    float dashDistanceTarget;

    float dashSpeed = 10f;

    public GruntAttackState(GruntEnemy gruntEnemy)
    {
        this.gruntEnemy = gruntEnemy;
    }
    public override void Enter()
    {
        dashDirection = (gruntEnemy.Player.transform.position - gruntEnemy.transform.position).normalized;
        playerInitialPosition = gruntEnemy.Player.transform.position;
        enemyInitialPostion = gruntEnemy.transform.position;
        dashDistanceTarget = Vector3.Distance(gruntEnemy.transform.position, playerInitialPosition);
        windupTimer = 0.5f;
        winddownTimer = 0.5f;
        gruntEnemy.Agent.isStopped = true;

    }

    public override void Perform()
    {
        windupTimer -= Time.deltaTime;
        if (windupTimer <= 0)
        {
            if(Vector3.Distance(gruntEnemy.transform.position, enemyInitialPostion) < dashDistanceTarget)
            {
                gruntEnemy.transform.position += dashSpeed * Time.deltaTime * dashDirection;
            }

            if(Vector3.Distance(gruntEnemy.transform.position, enemyInitialPostion) >= dashDistanceTarget)
            {
                winddownTimer -= Time.deltaTime;
                if(winddownTimer <= 0)
                {
                    stateMachine.ChangeState(new GruntMoveState(gruntEnemy));
                }
                
            }
        }
        
    }
    public override void Exit()
    {
        gruntEnemy.Agent.isStopped = false;
    }
}
