using UnityEngine;
using UnityEngine.AI;

public class ShotgunnerAttackState : BaseState
{
    private float shotTimer;
    readonly ShotgunnerEnemy shotgunnerEnemy;
    Vector3 retreatDirection;

    public ShotgunnerAttackState(ShotgunnerEnemy shotgunnerEnemy)
    {
        this.shotgunnerEnemy = shotgunnerEnemy;
    }
    
    public override void Enter()
    {
        //Rotation should only come from ShotgunnerEnemy.cs script while attacking
        shotgunnerEnemy.Agent.updateRotation = false;
    }

    public override void Perform()
    {
        retreatDirection = shotgunnerEnemy.transform.forward;

        if (Vector3.Distance(shotgunnerEnemy.transform.position, shotgunnerEnemy.Player.transform.position) > shotgunnerEnemy.moveCloserDistance || !shotgunnerEnemy.CanSeePlayer())
        {
            stateMachine.ChangeState(new ShotgunnerMoveState(shotgunnerEnemy));
        }
        else if (shotgunnerEnemy.CanSeePlayer())
        {
            shotTimer += Time.deltaTime;
            if (shotTimer > shotgunnerEnemy.fireRate)
            {
                Shoot();
            }
        }
        
        if(Vector3.Distance(shotgunnerEnemy.transform.position, shotgunnerEnemy.Player.transform.position) <= shotgunnerEnemy.panicDistance)
        {
            stateMachine.ChangeState(new ShotgunnerPanicState(shotgunnerEnemy));
        }
        
        if(Vector3.Distance(shotgunnerEnemy.transform.position, shotgunnerEnemy.Player.transform.position) < shotgunnerEnemy.moveAwayDistance)
        {
            NavMeshHit hitInfo = new();
            if(NavMesh.SamplePosition(shotgunnerEnemy.transform.position - retreatDirection * 1f, out hitInfo, 2f, NavMesh.AllAreas))
            {
                shotgunnerEnemy.Agent.SetDestination(hitInfo.position);
            }  
        }
    }

    public override void Exit()
    {
        shotgunnerEnemy.Agent.updateRotation = true;
    }

    public void Shoot()
    {
        Transform gunbarrel = shotgunnerEnemy.gunBarrel;
        Vector3 shootDirection = gunbarrel.forward;

        //Always shoot a center bullet
        GameObject bulletSingle = Object.Instantiate(shotgunnerEnemy.BulletPrefab, gunbarrel.position, shotgunnerEnemy.transform.rotation);
        bulletSingle.GetComponent<Rigidbody>().linearVelocity = shootDirection * 10;

        if (shotgunnerEnemy.NumberOfBullets > 1)
        {
            float angleStep = shotgunnerEnemy.totalBulletSpreadAngle / (shotgunnerEnemy.NumberOfBullets - 1);
            int counter = 0;
            //Spawn a mirrored pair of bullets with mirrored angles
            for (int i = 0; i < (shotgunnerEnemy.NumberOfBullets - 1) / 2; i++) 
            {
                counter++;
                GameObject bulletPositive = Object.Instantiate(shotgunnerEnemy.BulletPrefab, gunbarrel.position, shotgunnerEnemy.transform.rotation);
                GameObject bulletNegative = Object.Instantiate(shotgunnerEnemy.BulletPrefab, gunbarrel.position, shotgunnerEnemy.transform.rotation);
                

                Vector3 rotatedDirectionPositive = Quaternion.AngleAxis(angleStep * counter, Vector3.up) * shootDirection;
                Vector3 rotatedDirectionNegative = Quaternion.AngleAxis(-angleStep * counter, Vector3.up) * shootDirection;

                bulletPositive.GetComponent<Rigidbody>().linearVelocity = rotatedDirectionPositive * 10;
                bulletNegative.GetComponent<Rigidbody>().linearVelocity = rotatedDirectionNegative * 10;
            }  
        }
        shotTimer = 0;
    }
}
