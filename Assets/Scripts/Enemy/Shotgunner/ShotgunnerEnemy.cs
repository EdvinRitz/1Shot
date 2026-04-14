using UnityEngine;

public class ShotgunnerEnemy : BaseEnemy
{
    public float sightDistance = 20f;
    public float fieldOfView = 85f;
    public Vector3 lookDirection;
    [Range(0.1f,10f)]
    public float fireRate;
    public Transform gunBarrel;
    public float moveCloserDistance;
    public float moveAwayDistance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        stateMachine = GetComponent<StateMachine>();
        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");
        stateMachine.ChangeState(new ShotgunnerMoveState(this));
    }

    // Update is called once per frame
    public override void Update()
    {
        //lookDirection = (shotgunnerEnemy.Player.transform.position - shotgunnerEnemy.transform.position).normalized;
        //shotgunnerEnemy.transform.rotation = Quaternion.LookRotation(lookDirection);
        this.transform.LookAt(this.Player.transform);
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
}
