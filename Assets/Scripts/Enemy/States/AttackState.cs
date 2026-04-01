using System.Collections;
using UnityEngine;

public class AttackState : BaseState
{
    float windupTimer;
    Vector3 dashDirection;

    public override void Enter()
    {
        dashDirection = (enemyMovingTowardsPlayer.Player.transform.position - enemyMovingTowardsPlayer.transform.position).normalized;
        windupTimer = 0.5f;

    }

    public override void Perform()
    {
        windupTimer -= Time.deltaTime;
        if (windupTimer <= 0)
        {
            Debug.Log("attack made");
            stateMachine.ChangeState(new MoveTowardsPlayerState());
        }
        
    }
    public override void Exit()
    {
        
    }
}
