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

    }

    public override void Perform()
    {
        if (Vector3.Distance(shotgunnerEnemy.transform.position, shotgunnerEnemy.Player.transform.position) > shotgunnerEnemy.moveCloserDistance - 1.5f)
        {
            shotgunnerEnemy.Agent.SetDestination(shotgunnerEnemy.Player.transform.position - shotgunnerEnemy.transform.forward * (shotgunnerEnemy.moveAwayDistance + 1.5f));
        }
        else if (!shotgunnerEnemy.CanSeePlayer())
        {
            shotgunnerEnemy.Agent.SetDestination(shotgunnerEnemy.Player.transform.position);
        }
        else
        {
            stateMachine.ChangeState(new ShotgunnerAttackState(shotgunnerEnemy));
        }
    }

    public override void Exit()
    {

    }
}
