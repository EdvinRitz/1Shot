public abstract class BaseState
{
    public BaseEnemy baseEnemy;
    public StateMachine stateMachine;

    public abstract void Enter();
    public abstract void Perform();
    public abstract void Exit();
}
