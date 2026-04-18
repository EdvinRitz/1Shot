using UnityEditor;
using UnityEngine;

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
        
    }

    public override void Perform()
    {
        shotgunnerEnemy.transform.LookAt(shotgunnerEnemy.Player.transform);
        if (Vector3.Distance(shotgunnerEnemy.transform.position, shotgunnerEnemy.Player.transform.position) > shotgunnerEnemy.moveCloserDistance || !shotgunnerEnemy.CanSeePlayer())
        {
            stateMachine.ChangeState(new ShotgunnerMoveState(shotgunnerEnemy));
        }
        if (shotgunnerEnemy.CanSeePlayer())
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
        else if(Vector3.Distance(shotgunnerEnemy.transform.position, shotgunnerEnemy.Player.transform.position) < shotgunnerEnemy.moveAwayDistance)
        {
            retreatDirection = -(shotgunnerEnemy.Player.transform.position - shotgunnerEnemy.transform.position).normalized;
            shotgunnerEnemy.Agent.SetDestination(shotgunnerEnemy.transform.position + retreatDirection * 2f);
        }
        
    }

    public override void Exit()
    {

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
            float angleStep = shotgunnerEnemy.totalSpreadAngle / (shotgunnerEnemy.CalculateNumberOfBullets() - 1);
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
        //float angleStep = shotgunnerEnemy.totalSpreadAngle / (shotgunnerEnemy.CalculateNumberOfBullets() - 1);
        //Store reference to gun barrel
        //Transform gunbarrel = shotgunnerEnemy.gunBarrel;
        //instanciate a new bullet
        //GameObject bullet = GameObject.Instantiate(Resources.Load("Bullet") as GameObject, gunbarrel.position, shotgunnerEnemy.transform.rotation);
        //GameObject bullet1 = GameObject.Instantiate(Resources.Load("Bullet") as GameObject, gunbarrel.position, shotgunnerEnemy.transform.rotation);
        //GameObject bullet2 = GameObject.Instantiate(Resources.Load("Bullet") as GameObject, gunbarrel.position, shotgunnerEnemy.transform.rotation);
        
        //Vector3 shootDirection = gunbarrel.forward;
        //Vector3 rotatedDirection1 = Quaternion.AngleAxis(angleStep, Vector3.up) * shootDirection;
        //Vector3 rotatedDirection2 = Quaternion.AngleAxis(-angleStep, Vector3.up) * shootDirection;


        //add force rigidbody to the bullet
        //bullet.GetComponent<Rigidbody>().linearVelocity = shootDirection * 10;
        //bullet1.GetComponent<Rigidbody>().linearVelocity = rotatedDirection1 * 10;
        //bullet2.GetComponent<Rigidbody>().linearVelocity = rotatedDirection2 * 10;
        shotTimer = 0;
    }
}
