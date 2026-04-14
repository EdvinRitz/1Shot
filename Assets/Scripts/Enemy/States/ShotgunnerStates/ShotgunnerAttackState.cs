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
        Vector3 shootDirection = (shotgunnerEnemy.Player.transform.position - gunbarrel.transform.position).normalized;
        //add force rigidbody to the bullet
        bullet.GetComponent<Rigidbody>().linearVelocity = Quaternion.AngleAxis(Random.Range(-3f, 3f), Vector3.up) * shootDirection * 40;
        Debug.Log("Shoot");
        shotTimer = 0;
    }
}
