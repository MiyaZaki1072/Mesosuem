using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class Visitor : MonoBehaviour
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
    private float timer = 0.0f;
    private bool haveathing = false;
    public void Start()
    {
        spawnVisitor = GameObject.Find("VIsitorSpawn").GetComponent<SpawnVisitor>();
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        selectedObject = GameObject.Find("Door");
        Garbage = GameObject.Find("Garbage");
        //player = GameObject.Find("P1").transform;
        agent = GetComponent<NavMeshAgent>();
        if(buildingManager.CountCageL+buildingManager.CountCageS+buildingManager.CountCageM == 0){
            return;
        }
        wanttowatch = Random.Range(1,buildingManager.CountCageL+buildingManager.CountCageS+buildingManager.CountCageM+1);
        ///Debug.Log(wanttowatch);
        RandomITEM();
    }
    public void RandomITEM(){
        
        bool HaveCM = (buildingManager.CountCageM >0);
        bool HaveCL = (buildingManager.CountCageL >0);
        bool HaveCS = (buildingManager.CountCageS >0);
        int want=0;
        int R  = Random.Range(1, 100); 
        if(HaveCM && HaveCL && HaveCS){
            if(R >=50){
                want = 3;
            }
            else if(R>=20){
                want=2;
            }
            else want=1;
        }
        else if(HaveCL == false && HaveCM && HaveCS){
            if(R >=40)want=2;
            else want=1;
        }
        else if(HaveCM == false && HaveCL && HaveCS){
            if(R >=30)want=3;
            else want=1;
        }
        else if(HaveCS == false && HaveCL && HaveCM){
            if(R >=30)want=3;
            else want=1;
        }
        else if (HaveCL || HaveCM || HaveCS){
            if(HaveCS)want=1;
            if(HaveCM)want=2;
            if(HaveCL)want=3;
        }
        else{
            want=0;
        }
        ///Debug.Log(want);
        if(want ==1 ){
            offsetX = Random.Range(0,5);
            offsetZ = Random.Range(0,5);
            int WR = Random.Range(0,buildingManager.CountCageS);
            ///Debug.Log(WR);
            player = buildingManager.SCage[WR].transform;
            Item item = buildingManager.SCage[WR].GetComponent<Item>();
            if(item.equipped)haveathing=true;
            else haveathing=false;
            looking = new Vector3(player.position.x+offsetX,player.position.y,player.position.z+offsetZ);
            Stime+=1;
        }
        else if(want == 2){
            offsetX = Random.Range(0,5);
            offsetZ = Random.Range(0,5);
            int WR = Random.Range(0,buildingManager.CountCageM);
            player = buildingManager.MCage[WR].transform;
            Item item = buildingManager.MCage[WR].GetComponent<Item>();
            if(item.equipped)haveathing=true;
            else haveathing=false;
            looking = new Vector3(player.position.x+offsetX,player.position.y,player.position.z+offsetZ);
            Mtime+=1;
        }
        else if(want == 3){
            offsetX = Random.Range(0,5);
            offsetZ = Random.Range(0,5);
            int WR = Random.Range(0,buildingManager.CountCageL);
            player = buildingManager.LCage[WR].transform;
            Item item = buildingManager.LCage[WR].GetComponent<Item>();
            if(item.equipped)haveathing=true;
            else haveathing=false;
            looking = new Vector3(player.position.x+offsetX,player.position.y,player.position.z+offsetZ);
            Ltime+=1;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
  // Check for collision with door object only when wanttowatch is 0
  if (wanttowatch == 0 && other.gameObject.tag == "Door")
  {
    Destroy(gameObject); // Destroy the visitor object
    spawnVisitor.cnt -= 1; // Decrement visitor counter (assuming this is in another script)
  }
    }
     private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 30.0f)
        {
            wanttowatch = 0;
            // Reset timer after reaching 30 seconds
        }
        if(wanttowatch == 0){
            curpos = selectedObject.transform;
            Vector3 door = new Vector3(curpos.position.x,curpos.position.y,curpos.position.z);  
            agent.SetDestination(door);
        }
        else {
    // Update state logic based on playerInSightRange, playerInAttackRange, and foundObstacle
        playerInSightRange = Physics.Raycast(transform.position, (player.position - transform.position).normalized, sightRange);
        playerInAttackRange = Physics.Raycast(transform.position, (player.position - transform.position).normalized, attackRange);
        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange && alreadyAttacked == false) AttackPlayer();
        }
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
        agent.SetDestination(looking);
    }
    IEnumerator AttackPlayerCoroutine() {
        alreadyAttacked = true;
        agent.SetDestination(transform.position);
        transform.LookAt(looking);
  // Wait for 10 seconds
        int waittime;
        if(haveathing)waittime  = Random.Range(5,15);
        else waittime = 3;
        yield return new WaitForSeconds(waittime);
        int Chance = Random.Range(1,100);
        if(Chance <=15){
            gameManager.UpdateClean(3);
        }
        if(Chance <=15){
            GameObject NewGarbage = Instantiate(Garbage,transform.position,transform.rotation);
            if(countGarbage <=5 && countGarbage >0)gameManager.UpdateClean(4);
            else if(countGarbage <=10)gameManager.UpdateClean(3);
            else if(countGarbage <=20)gameManager.UpdateClean(2);
            else if(countGarbage <=25)gameManager.UpdateClean(1);
            else gameManager.UpdateClean(0);
        }
        if (wanttowatch > 0) {
            wanttowatch =wanttowatch- 1;
            RandomITEM();
        }
  // Code to execute after the wait
        alreadyAttacked = false; // Reset attack flag
    }

    private void AttackPlayer() {
        StartCoroutine(AttackPlayerCoroutine());
    }
}