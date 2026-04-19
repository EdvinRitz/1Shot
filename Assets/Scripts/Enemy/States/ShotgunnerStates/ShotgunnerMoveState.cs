using UnityEngine;

public class ShotgunnerMoveState : BaseState
{
    readonly ShotgunnerEnemy shotgunnerEnemy;
    Vector3 playerDirection;
    public ShotgunnerMoveState(ShotgunnerEnemy shotgunnerEnemy)
    {
        this.shotgunnerEnemy = shotgunnerEnemy;
    }
    
    public override void Enter()
    {
        shotgunnerEnemy.Agent.SetDestination(shotgunnerEnemy.Player.transform.position);
    }

    public override void Perform()
    {
        //shotgunnerEnemy.transform.LookAt(shotgunnerEnemy.Player.transform);
        Vector3 directionToPlayer = (shotgunnerEnemy.Player.transform.position - shotgunnerEnemy.transform.position).normalized;
        shotgunnerEnemy.transform.rotation = Quaternion.RotateTowards(shotgunnerEnemy.transform.rotation, Quaternion.LookRotation(directionToPlayer), 180 * Time.deltaTime);
        if (Vector3.Distance(shotgunnerEnemy.transform.position, shotgunnerEnemy.Player.transform.position) > shotgunnerEnemy.moveCloserDistance || !shotgunnerEnemy.CanSeePlayer())
        {
            shotgunnerEnemy.Agent.SetDestination(shotgunnerEnemy.Player.transform.position);
        }
        else
        {
            playerDirection = (shotgunnerEnemy.Player.transform.position - shotgunnerEnemy.transform.position).normalized;
            shotgunnerEnemy.Agent.SetDestination(shotgunnerEnemy.transform.position + playerDirection * 1f);
            stateMachine.ChangeState(new ShotgunnerAttackState(shotgunnerEnemy));
        }
    }

    public override void Exit()
    {

    }
}
