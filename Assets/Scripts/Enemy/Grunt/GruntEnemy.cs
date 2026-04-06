using System.Collections;
using UnityEngine;

public class GruntEnemy : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;
    public UnityEngine.AI.NavMeshAgent Agent { get => agent; }
    private GameObject player;
    public GameObject Player { get => player; }
    public GruntEnemy gruntEnemy;
    private StateMachine stateMachine;
    public float attackDistance = 6f;
    public float sightDistance = 20f;
    public float fieldOfView = 85f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        stateMachine = GetComponent<StateMachine>();
        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");
        stateMachine.ChangeState(new GruntMoveState(this));
    }

    // Update is called once per frame
    void Update()
    {
        //agent.SetDestination(player.transform.position);
    }

    public bool CanSeePlayer()
    {
        if (player != null)
        {
            //is the player close enough to be seen?
            if (Vector3.Distance(transform.position, player.transform.position) < sightDistance)
            {
                Vector3 targetDirection = player.transform.position - transform.position;
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                {
                    Ray ray = new Ray(transform.position, targetDirection);
                    RaycastHit hitInfo = new RaycastHit();
                    if (Physics.Raycast(ray, out hitInfo, sightDistance))
                    {
                        if (hitInfo.transform.gameObject == player)
                        {
                            Debug.DrawRay(ray.origin, ray.direction * sightDistance);
                            return true;
                            
                        }
                    }
                }
            }
        }
        return false;
    }
    
    public void Die()
    {
        stateMachine.ChangeState(new GruntDieState(this));
    }
    //{
        //agent.isStopped = true;
        //StartCoroutine(DisableAfterDelay());
    //}

    //IEnumerator DisableAfterDelay()
    //{
        //yield return new WaitForSeconds(1f);
        //gameObject.SetActive(false);
    //}

}
