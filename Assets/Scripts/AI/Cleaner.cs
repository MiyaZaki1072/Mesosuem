using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class Cleaner : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public Transform curpos;
    public LayerMask whatIsGround, whatIsPlayer;
    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked =false;
    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public GameObject selectedObject;
    public BuildingManager buildingManager;
    public GameManager gameManager;
    public float avoidanceDistance = 0.5f;
    public bool isAttacking;
    private float offsetZ;
    private float offsetX;
    private Vector3 standing,looking;
    private int Ltime=0;
    private int Stime=0;
    private int Mtime =0;
    private int wanttowatch=0;
    public int countGarbage=0;
    public GameObject Garbage;
    public SpawnVisitor spawnVisitor;
    public GameObject[] AllObject;
    public GameObject NearestTrash;
    float distance;
    float nearestDistance=1000000;
    public int WorkSpeed; 
    public int Cost;
    public string Name;
    public void Start()
    {
        spawnVisitor = GameObject.Find("VIsitorSpawn").GetComponent<SpawnVisitor>();
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        selectedObject = GameObject.Find("Door");
        Garbage = GameObject.Find("Garbage");
        //player = GameObject.Find("P1").transform;
        agent = GetComponent<NavMeshAgent>();
        Choose();
    }
     private void Update()
    {
        if(player == null){
            Patroling();
            Choose();
            return;
        }
    // Update state logic based on playerInSightRange, playerInAttackRange, and foundObstacle
        playerInSightRange = Physics.Raycast(transform.position, (player.position - transform.position).normalized, sightRange);
        playerInAttackRange = Physics.Raycast(transform.position, (player.position - transform.position).normalized, attackRange);
        //Debug.Log(playerInAttackRange);
        //Debug.Log(playerInSightRange);
        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange && alreadyAttacked == false) AttackPlayer();
    }
    public void Choose(){
        nearestDistance =100000;
        AllObject = GameObject.FindGameObjectsWithTag("Garbage");
        //if(AllObject.Length == 0)return;
        for(int i=0;i<AllObject.Length;i++){
            distance = Vector3.Distance(this.transform.position,AllObject[i].transform.position);
            if(distance < nearestDistance){
                player = AllObject[i].transform;
                NearestTrash = AllObject[i];
                nearestDistance = distance;
            }
        }
        if(NearestTrash == AllObject[0])player=null;
    }
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }
    private void ChasePlayer()
    {
        Vector3 looking = new Vector3(player.position.x, player.position.y, player.position.z);
        agent.SetDestination(looking);
    }
    IEnumerator AttackPlayerCoroutine() {
        alreadyAttacked = true;
        agent.SetDestination(transform.position);
  // Wait for 10 seconds
        int waittime  = Random.Range(5,15);
        yield return new WaitForSeconds(6-WorkSpeed);
        player = null;
        Destroy(NearestTrash);
        gameManager.countGarbage--;
  // Code to execute after the wait
        alreadyAttacked = false; // Reset attack flag
    }

    private void AttackPlayer() {
        StartCoroutine(AttackPlayerCoroutine());
    }
}