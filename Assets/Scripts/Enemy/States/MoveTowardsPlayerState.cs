public class MoveTowardsPlayerState : BaseState
{

    public override void Enter()
    {
        
    }
    public override void Perform()
    {
        walkTowardsPlayer();
    }
    public void walkTowardsPlayer()
    {
        enemyMovingTowardsPlayer.Agent.SetDestination(enemyMovingTowardsPlayer.Player.transform.position);
        
    }
    public override void Exit()
    {
        
    }
}
