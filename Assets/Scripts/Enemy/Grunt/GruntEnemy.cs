using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(StateMachine))]
public class GruntEnemy : BaseEnemy
{
    public float attackDistance = 6f;
    public float sightDistance = 20f;
    public float fieldOfView = 85f;
    public float playerCloseDistance = 1f;
    public GameObject AttackHitboxCenter;

    public override void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        stateMachine = GetComponent<StateMachine>();
        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");
        stateMachine.ChangeState(new GruntMoveState(this));
    }

    public override void Update()
    {
        
    }

    public override bool CanSeePlayer()
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
                    Ray ray = new(transform.position, targetDirection);
                    RaycastHit hitInfo = new();
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
}
