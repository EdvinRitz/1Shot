using System.Collections;
using UnityEngine;

public class DieState : BaseState
{
    public DieState(BaseEnemy baseEnemy)
    {
        this.baseEnemy = baseEnemy;
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
