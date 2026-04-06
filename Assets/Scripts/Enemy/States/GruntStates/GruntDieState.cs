using System.Collections;
using UnityEngine;

public class GruntDieState : BaseState
{

    public override void Enter()
    {
        enemyMovingTowardsPlayer.Agent.isStopped = true;
        enemyMovingTowardsPlayer.StartCoroutine(DisableAfterDelay());
    }
    IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        enemyMovingTowardsPlayer.gameObject.SetActive(false);
    }
    public override void Perform()
    {
        
    }
    public override void Exit()
    {
        
    }
}
