using UnityEngine;

public class ShotgunnerPanicState : BaseState
{
    readonly ShotgunnerEnemy shotgunnerEnemy;
    Vector3 panicDirection;
    float normalSpeed;
    Vector3 initialPostition;
    public ShotgunnerPanicState(ShotgunnerEnemy shotgunnerEnemy)
    {
        this.shotgunnerEnemy = shotgunnerEnemy;
    }
    public override void Enter()
    {
        normalSpeed = shotgunnerEnemy.Agent.speed;
        shotgunnerEnemy.Agent.speed = shotgunnerEnemy.Agent.speed * 2;
        panicDirection = (shotgunnerEnemy.Player.transform.position - shotgunnerEnemy.transform.position).normalized;
        initialPostition = shotgunnerEnemy.transform.position;
        //shotgunnerEnemy.Agent.SetDestination(shotgunnerEnemy.transform.position + panicDirection * 10f);
    }

    public override void Perform()
    {
        //panicDirection = -(shotgunnerEnemy.Player.transform.position - shotgunnerEnemy.transform.position).normalized;
        //shotgunnerEnemy.Agent.SetDestination(shotgunnerEnemy.transform.position + panicDirection * 3f);
        shotgunnerEnemy.Agent.SetDestination(initialPostition + panicDirection * 10f);
    }

    public override void Exit()
    {
        shotgunnerEnemy.Agent.speed = normalSpeed;
    }
}
