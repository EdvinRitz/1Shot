using UnityEngine;

public class ShotgunnerMoveState : BaseState
{
    readonly ShotgunnerEnemy shotgunnerEnemy;
    public ShotgunnerMoveState(ShotgunnerEnemy shotgunnerEnemy)
    {
        this.shotgunnerEnemy = shotgunnerEnemy;
    }
    
    public override void Enter()
    {
        shotgunnerEnemy.Agent.isStopped = false;
    }

    public override void Perform()
    {
        if (Vector3.Distance(shotgunnerEnemy.transform.position, shotgunnerEnemy.Player.transform.position) > shotgunnerEnemy.moveCloserDistance)
        {
            shotgunnerEnemy.Agent.SetDestination(shotgunnerEnemy.Player.transform.position);
        }
        else
        {
            shotgunnerEnemy.Agent.isStopped = true;
            stateMachine.ChangeState(new ShotgunnerAttackState(shotgunnerEnemy));
        }
    }

    public override void Exit()
    {

    }
}
