using System.Collections;
using UnityEngine;

public class GruntDieState : BaseState
{
    public GruntDieState(BaseEnemy gruntEnemy)
    {
        this.baseEnemy = gruntEnemy;
    }

    public override void Enter()
    {
        baseEnemy.Agent.isStopped = true;
        baseEnemy.StartCoroutine(DisableAfterDelay());
    }
    IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        baseEnemy.gameObject.SetActive(false);
    }
    public override void Perform()
    {
        
    }
    public override void Exit()
    {
        
    }
}
