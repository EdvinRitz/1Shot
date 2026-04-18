using UnityEngine;

public class ShotgunnerPanicState : BaseState
{
    readonly ShotgunnerEnemy shotgunnerEnemy;
    Vector3 panicDirection;
    float normalSpeed;
    public ShotgunnerPanicState(ShotgunnerEnemy shotgunnerEnemy)
    {
        this.shotgunnerEnemy = shotgunnerEnemy;
    }
    public override void Enter()
    {
        normalSpeed = shotgunnerEnemy.Agent.speed;
        shotgunnerEnemy.Agent.speed = shotgunnerEnemy.Agent.speed * 2;
        panicDirection = (shotgunnerEnemy.Player.transform.position - shotgunnerEnemy.transform.position).normalized;
        shotgunnerEnemy.Agent.SetDestination(shotgunnerEnemy.transform.position + panicDirection * 10f);
    }

    public override void Perform()
    {
        //panicDirection = -(shotgunnerEnemy.Player.transform.position - shotgunnerEnemy.transform.position).normalized;
        //shotgunnerEnemy.Agent.SetDestination(shotgunnerEnemy.transform.position + panicDirection * 3f);
    }

    public override void Exit()
    {
        shotgunnerEnemy.Agent.speed = normalSpeed;
    }
}
