using System.Collections.Generic;
using UnityEngine;

public class ShielderEnemy : BaseEnemy
{
    public float attackDistance = 6f;
    public float sightDistance = 20f;
    public float fieldOfView = 60f;
    public float stopDistance = 3f;
    public static List<ShielderEnemy> activeShielders = new();


    public void OnEnable()
    {
        activeShielders.Add(this);
    }

    void OnDisable()
    {
        activeShielders.Remove(this);
    }

    public override void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        stateMachine = GetComponent<StateMachine>();
        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");
        stateMachine.ChangeState(new ShielderMoveState(this));
    }

    // Update is called once per frame
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

    public bool PlayerInFieldOfView()
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
                    return true;
                }
            }
        }
        return false;
    }
}
