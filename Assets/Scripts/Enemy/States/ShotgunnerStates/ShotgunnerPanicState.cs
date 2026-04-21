using UnityEngine;
using UnityEngine.AI;

public class ShotgunnerPanicState : BaseState
{
    readonly ShotgunnerEnemy shotgunnerEnemy;
    float normalSpeed;
    float panicTimer;
    Vector3 panicEndPostition;
    ShielderEnemy shielderEnemyClosest;
    public ShotgunnerPanicState(ShotgunnerEnemy shotgunnerEnemy)
    {
        this.shotgunnerEnemy = shotgunnerEnemy;
    }
    public override void Enter()
    {
        shotgunnerEnemy.PanicState = true;
        panicTimer = 3f;
        normalSpeed = shotgunnerEnemy.Agent.speed;
        shotgunnerEnemy.Agent.speed = shotgunnerEnemy.Agent.speed * 2;

        if(ShielderEnemy.activeShielderEnemies.Count == 0)
        {
            shotgunnerEnemy.Agent.updateRotation = false; 
            panicEndPostition = shotgunnerEnemy.transform.position - (shotgunnerEnemy.Player.transform.position - shotgunnerEnemy.transform.position).normalized * shotgunnerEnemy.panicMoveDistance;
            shotgunnerEnemy.transform.rotation = Quaternion.LookRotation((shotgunnerEnemy.transform.position - shotgunnerEnemy.Player.transform.position ).normalized);
        }
        else if(ShielderEnemy.activeShielderEnemies.Count == 1)
        {
            shielderEnemyClosest = ShielderEnemy.activeShielderEnemies[0];
        }
        else if(ShielderEnemy.activeShielderEnemies.Count > 1)
        {
            shielderEnemyClosest = ShielderEnemy.activeShielderEnemies[0];

            for(int i = 1; i < ShielderEnemy.activeShielderEnemies.Count; i++)
            {
                if (Vector3.Distance(shotgunnerEnemy.transform.position, ShielderEnemy.activeShielderEnemies[i].transform.position) < Vector3.Distance(shotgunnerEnemy.transform.position, shielderEnemyClosest.transform.position))
                {
                    shielderEnemyClosest = ShielderEnemy.activeShielderEnemies[i];
                }
            }
        }
    }

    public override void Perform()
    {
        if(ShielderEnemy.activeShielderEnemies.Count > 0)
        {
            panicEndPostition = shielderEnemyClosest.transform.position - shielderEnemyClosest.transform.forward * 2f;
        }
        else
        {
            panicTimer -= Time.deltaTime;
        }

        shotgunnerEnemy.Agent.SetDestination(panicEndPostition);

        if(ShielderEnemy.activeShielderEnemies.Count > 0 && Vector3.Distance(shotgunnerEnemy.transform.position, panicEndPostition) <= 0.3f)
        {
            stateMachine.ChangeState(new ShotgunnerMoveState(shotgunnerEnemy));
        }
        else
        {
            if(panicTimer <= 0)
            {
                stateMachine.ChangeState(new ShotgunnerMoveState(shotgunnerEnemy));
            }
        }
    }

    public override void Exit()
    {
        shotgunnerEnemy.Agent.speed = normalSpeed;
        shotgunnerEnemy.PanicState = false;
        if(ShielderEnemy.activeShielderEnemies.Count == 0)
        {
            shotgunnerEnemy.Agent.updateRotation = true; 
        }
    }
}
