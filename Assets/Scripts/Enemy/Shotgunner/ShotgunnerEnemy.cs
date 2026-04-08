using UnityEngine;

public class ShotgunnerEnemy : BaseEnemy
{
    public ShotgunnerEnemy shotgunnerEnemy;
    public Vector3 lookDirection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        stateMachine = GetComponent<StateMachine>();
        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");
        shotgunnerEnemy = GetComponent<ShotgunnerEnemy>();
        //stateMachine.ChangeState(new GruntMoveState(this));
    }

    // Update is called once per frame
    public override void Update()
    {
        //lookDirection = (shotgunnerEnemy.Player.transform.position - shotgunnerEnemy.transform.position).normalized;
        //shotgunnerEnemy.transform.rotation = Quaternion.LookRotation(lookDirection);
        shotgunnerEnemy.transform.LookAt(shotgunnerEnemy.Player.transform);
    }
    public override bool CanSeePlayer()
    {
        return true;
    }
}
