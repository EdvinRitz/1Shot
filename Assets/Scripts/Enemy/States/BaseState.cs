public abstract class BaseState
{
    public GruntEnemy enemyMovingTowardsPlayer;
    public StateMachine stateMachine;

    public abstract void Enter();
    public abstract void Perform();
    public abstract void Exit();
}
