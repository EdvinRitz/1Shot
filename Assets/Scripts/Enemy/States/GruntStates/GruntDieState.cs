using System.Collections;
using UnityEngine;

public class GruntDieState : BaseState
{
    public GruntDieState(GruntEnemy gruntEnemy)
    {
        this.gruntEnemy = gruntEnemy;
    }

    public override void Enter()
    {
        gruntEnemy.Agent.isStopped = true;
        gruntEnemy.StartCoroutine(DisableAfterDelay());
    }
    IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        gruntEnemy.gameObject.SetActive(false);
    }
    public override void Perform()
    {
        
    }
    public override void Exit()
    {
        
    }
}
