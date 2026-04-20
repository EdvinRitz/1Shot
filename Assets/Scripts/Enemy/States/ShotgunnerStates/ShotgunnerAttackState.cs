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

        if(shotgunnerEnemy.CalculateNumberOfBullets() == 1){
            GameObject bulletSingle = GameObject.Instantiate(Resources.Load("Bullet") as GameObject, gunbarrel.position, shotgunnerEnemy.transform.rotation);
            bulletSingle.GetComponent<Rigidbody>().linearVelocity = shootDirection * 10;
        }
        else
        {
            GameObject bulletSingle = GameObject.Instantiate(Resources.Load("Bullet") as GameObject, gunbarrel.position, shotgunnerEnemy.transform.rotation);
            bulletSingle.GetComponent<Rigidbody>().linearVelocity = shootDirection * 10;
            float angleStep = shotgunnerEnemy.totalBulletSpreadAngle / (shotgunnerEnemy.CalculateNumberOfBullets() - 1);
            int counter = 0;
            for (int i = 0; i < (shotgunnerEnemy.CalculateNumberOfBullets() - 1) / 2; i++) 
            {
                counter++;
                GameObject bulletPosetive = GameObject.Instantiate(Resources.Load("Bullet") as GameObject, gunbarrel.position, shotgunnerEnemy.transform.rotation);
                GameObject bulletNegative = GameObject.Instantiate(Resources.Load("Bullet") as GameObject, gunbarrel.position, shotgunnerEnemy.transform.rotation);

                Vector3 rotatedDirectionPosetive = Quaternion.AngleAxis(angleStep * counter, Vector3.up) * shootDirection;
                Vector3 rotatedDirectionNegative = Quaternion.AngleAxis(-angleStep * counter, Vector3.up) * shootDirection;

                bulletPosetive.GetComponent<Rigidbody>().linearVelocity = rotatedDirectionPosetive * 10;
                bulletNegative.GetComponent<Rigidbody>().linearVelocity = rotatedDirectionNegative * 10;
            }  
        }
        shotTimer = 0;
    }
}
