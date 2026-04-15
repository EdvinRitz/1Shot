using UnityEditor;
using UnityEngine;

public class ShotgunnerAttackState : BaseState
{
    private float shotTimer;
    readonly ShotgunnerEnemy shotgunnerEnemy;
    Vector3 retreatDirection;
    int totalSpreadAngle = 90;

    public ShotgunnerAttackState(ShotgunnerEnemy shotgunnerEnemy)
    {
        this.shotgunnerEnemy = shotgunnerEnemy;
    }
    
    public override void Enter()
    {
        
    }

    public override void Perform()
    {
        if (Vector3.Distance(shotgunnerEnemy.transform.position, shotgunnerEnemy.Player.transform.position) > shotgunnerEnemy.moveCloserDistance)
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
        if(Vector3.Distance(shotgunnerEnemy.transform.position, shotgunnerEnemy.Player.transform.position) < shotgunnerEnemy.moveAwayDistance)
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
        float angleStep = totalSpreadAngle / (shotgunnerEnemy.CalculateNumberOfBullets() - 1);
        //Store reference to gun barrel
        Transform gunbarrel = shotgunnerEnemy.gunBarrel;
        //instanciate a new bullet
        GameObject bullet = GameObject.Instantiate(Resources.Load("Bullet") as GameObject, gunbarrel.position, shotgunnerEnemy.transform.rotation);
        
        Vector3 shootDirection = gunbarrel.forward;
        //add force rigidbody to the bullet
        bullet.GetComponent<Rigidbody>().linearVelocity = shootDirection * 10;
        shotTimer = 0;
    }
}
