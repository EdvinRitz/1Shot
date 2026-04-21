using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(StateMachine))]
public class ShotgunnerEnemy : BaseEnemy
{
    [Header("Vision")]
    public float sightDistance = 20f;
    public float fieldOfView = 85f;
    [Header("Shooting")]
    [Range(0.1f,10f)]
    public float fireRate;
    [Tooltip("number + number - 1")]
    [Range(1,5)]
    public int bulletsPerShotSliderValue;
    public int totalBulletSpreadAngle = 90;
    public Transform gunBarrel;
    [Header("Movement")]
    [Tooltip("Distance from Player that activates the move state")]
    public float moveCloserDistance;
    [Tooltip("Distance from Player that activates the move away logic in the attack state")]
    public float moveAwayDistance;
    [Tooltip("Distance from Player that activates the panic state")]
    public float panicDistance;
    [Tooltip("Distance moved when paniced and no Shielders are active")]
    public float panicMoveDistance = 10f;
    private bool panicState = false;
    public bool PanicState {get => panicState; set => panicState = value;}   
    private GameObject bulletPrefab;
    public GameObject BulletPrefab { get => bulletPrefab; }
    private int numberOfBullets;
    public int NumberOfBullets { get => numberOfBullets; }

    private int mask;

    public override void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        stateMachine = GetComponent<StateMachine>();
        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");
        stateMachine.ChangeState(new ShotgunnerMoveState(this));

        //Shotgunner can see through enemies and objects on layer "RayCastStop" (like shielders shields)
        int layerMasks = LayerMask.GetMask("RayCastStop", "Enemy");
        mask = ~layerMasks;

        //Loads bullet once
        bulletPrefab = Resources.Load<GameObject>("Bullet");
        numberOfBullets = CalculateNumberOfBullets();
    }

    public override void Update()
    {
        if (!panicState && stateMachine.activeState.ToString() != "DieState")
        {
        Vector3 directionToPlayer = (Player.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(directionToPlayer), 180 * Time.deltaTime);
        }
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
                    if (Physics.Raycast(ray, out hitInfo, sightDistance, mask))
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

    public int CalculateNumberOfBullets()
    {
        return bulletsPerShotSliderValue + bulletsPerShotSliderValue - 1;
    }

}
