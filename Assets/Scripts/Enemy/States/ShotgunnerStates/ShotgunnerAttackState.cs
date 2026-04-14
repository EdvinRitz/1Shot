using UnityEditor;
using UnityEngine;

public class ShotgunnerAttackState : BaseState
{
    private float shotTimer;
    ShotgunnerEnemy shotgunnerEnemy;

    public ShotgunnerAttackState(ShotgunnerEnemy shotgunnerEnemy)
    {
        this.shotgunnerEnemy = shotgunnerEnemy;
    }
    
    public override void Enter()
    {
        
    }

    public override void Perform()
    {
                if (shotgunnerEnemy.CanSeePlayer())
        {
            shotTimer += Time.deltaTime;
            if (shotTimer > shotgunnerEnemy.fireRate)
            {
                Shoot();
                Debug.Log("Shot");
            }
        }
        
    }

    public override void Exit()
    {

    }

    public void Shoot()
    {
        //Store reference to gun barrel
        Transform gunbarrel = shotgunnerEnemy.gunBarrel;
        //instanciate a new bullet
        GameObject bullet = GameObject.Instantiate(Resources.Load("Bullet") as GameObject, gunbarrel.position, shotgunnerEnemy.transform.rotation);
        //calculate the direction to the player
        Vector3 shootDirection = gunbarrel.forward;
        //add force rigidbody to the bullet
        bullet.GetComponent<Rigidbody>().linearVelocity = shootDirection * 10;
        Debug.Log("Shoot");
        shotTimer = 0;
    }
}
