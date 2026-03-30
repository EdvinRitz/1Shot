using System.Collections;
using UnityEngine;

public class EnemyMovingTowardsPlayer : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;
    public UnityEngine.AI.NavMeshAgent Agent { get => agent; }
    private GameObject player;
    public GameObject Player { get => player; }
    public EnemyMovingTowardsPlayer enemyMovingTowardsPlayer;
    private StateMachine stateMachine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        stateMachine = GetComponent<StateMachine>();
        stateMachine.Initialise();
    }

    // Update is called once per frame
    void Update()
    {
        //agent.SetDestination(player.transform.position);
    }
    
    public void Die()
    {
        agent.isStopped = true;
        StartCoroutine(DisableAfterDelay());
    }

    IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

}
