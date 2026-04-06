public abstract class BaseState
{
    public GruntEnemy gruntEnemy;
    public StateMachine stateMachine;

    public abstract void Enter();
    public abstract void Perform();
    public abstract void Exit();
}
