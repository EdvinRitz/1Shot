using UnityEngine;

public class ShotgunnerPanicState : BaseState
{
    readonly ShotgunnerEnemy shotgunnerEnemy;
    float normalSpeed;
    Vector3 panicEndPostition;
    public float panicMoveDistance = 10f;
    ShielderEnemy shielderEnemyClosest;
    public ShotgunnerPanicState(ShotgunnerEnemy shotgunnerEnemy)
    {
        this.shotgunnerEnemy = shotgunnerEnemy;
    }
    public override void Enter()
    {
        normalSpeed = shotgunnerEnemy.Agent.speed;
        shotgunnerEnemy.Agent.speed = shotgunnerEnemy.Agent.speed * 2;

        if(ShielderEnemy.activeShielderEnemies.Count == 0)
        {
            panicEndPostition = shotgunnerEnemy.transform.position + (shotgunnerEnemy.transform.position - shotgunnerEnemy.Player.transform.position).normalized * panicMoveDistance;
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
                if (Vector3.Distance(shotgunnerEnemy.transform.position, ShielderEnemy.activeShielderEnemies[i].transform.position) < Vector3.Distance(shotgunnerEnemy.transform.position, ShielderEnemy.activeShielderEnemies[i-1].transform.position))
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
            panicEndPostition = shielderEnemyClosest.transform.position - shielderEnemyClosest.transform.forward * 1.5f;
        }

        shotgunnerEnemy.Agent.SetDestination(panicEndPostition);

        if(Vector3.Distance(shotgunnerEnemy.transform.position, panicEndPostition) <= 0.2f)
        {
            stateMachine.ChangeState(new ShotgunnerMoveState(shotgunnerEnemy));
        }
    }

    public override void Exit()
    {
        shotgunnerEnemy.Agent.speed = normalSpeed;
    }
}
