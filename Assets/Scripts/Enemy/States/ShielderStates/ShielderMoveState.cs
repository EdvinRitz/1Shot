using UnityEngine;
using UnityEngine.SocialPlatforms;

public class ShielderMoveState : BaseState
{
    ShielderEnemy shielderEnemy;

    public ShielderMoveState(ShielderEnemy shielderEnemy)
    {
        this.shielderEnemy = shielderEnemy;
    }

    public override void Enter()
    {
        
    }

    // Update is called once per frame
    public override void Perform()
    {
        WalkTowardsPlayer();
    }

    public void WalkTowardsPlayer()
    {
        //shielderEnemy.Agent.SetDestination(shielderEnemy.Player.transform.position);
        if (!shielderEnemy.PlayerInFieldOfView() || Vector3.Distance(shielderEnemy.transform.position, shielderEnemy.Player.transform.position) <= shielderEnemy.stopDistance)
        {
            shielderEnemy.Agent.isStopped = true;
            Vector3 directionToPlayer = (shielderEnemy.Player.transform.position - shielderEnemy.transform.position).normalized;
            shielderEnemy.transform.rotation = Quaternion.RotateTowards(shielderEnemy.transform.rotation, Quaternion.LookRotation(directionToPlayer), 40 * Time.deltaTime);
        }
        else
        {
            shielderEnemy.Agent.isStopped = false;
            shielderEnemy.Agent.SetDestination(shielderEnemy.Player.transform.position);
        }

        if((Vector3.Distance(shielderEnemy.transform.position, shielderEnemy.Player.transform.position) < shielderEnemy.attackDistance) && shielderEnemy.CanSeePlayer()) 
        {
            //TODO switch to attack state
        }
    }

    public override void Exit()
    {
        
    }
}
