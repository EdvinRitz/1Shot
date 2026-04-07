using UnityEngine;

public class ShotgunnerEnemy : MonoBehaviour
{
    private ShotgunnerEnemy shotgunnerEnemy;
    private UnityEngine.AI.NavMeshAgent agent;
    public UnityEngine.AI.NavMeshAgent Agent { get => agent; }
    private GameObject player;
    public GameObject Player { get => player; }
    private StateMachine stateMachine;
    public Vector3 lookDirection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        stateMachine = GetComponent<StateMachine>();
        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");
        shotgunnerEnemy = GetComponent<ShotgunnerEnemy>();
        //stateMachine.ChangeState(new GruntMoveState(this));
    }

    // Update is called once per frame
    void Update()
    {
        lookDirection = (shotgunnerEnemy.Player.transform.position - shotgunnerEnemy.transform.position).normalized;
        shotgunnerEnemy.transform.rotation = Quaternion.LookRotation(lookDirection);
    }
}
