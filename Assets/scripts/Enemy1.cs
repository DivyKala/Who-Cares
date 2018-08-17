using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1 : MonoBehaviour {

    public float minPlayerDetectDistance = 5f;  //Will instantly detect the player regardless of whether the player is lit (but in fov)
    public float maxDetectDistance = 20f;   //Can't detect player after this range, even if player is lit or in field of view
    public float approach = 2f;
    public float fieldOfView = 90.0f;
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;
    public float delayInScriptUpdate = 0.5f;
    public float escapeDistance = 7f;
    public AudioClip[] attackClip;
    public AudioClip alarmClip;



    public Transform[] patrolPoints;


    public float patrolSpeed = 3.5f, followSpeed = 6f, checkAlarmSpeed = 5f;
    public float enemyFollowDuration = 5f;


    private AudioSource audioSource;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;

    private GameObject player;
    private Transform rayCastPlayer;
    private Player playerController;


    private Rigidbody rigidbody;
    private Animator animator;
    bool detectedOnce = false;
    private int destPoint = 0;
    private float timer =0f;
    private bool attacking = false;
    private bool checkingAlarm = false;
    private float nextFire = 0f;
    private bool playerFound = false;
    private NavMeshAgent navMeshAgent;
    private LayerMask shootable;

    void Start () {
        audioSource = GetComponent<AudioSource>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        shootable = LayerMask.GetMask("Shootable");

        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player1");
        playerController = player.GetComponent<Player>();
        enemyHealth = GetComponent<EnemyHealth>();
        playerHealth = player.GetComponent<PlayerHealth>();
        rayCastPlayer = player.transform.FindChild("player").transform;

        //Starts patrolling by default
        GotoNextPoint();

    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position , minPlayerDetectDistance);
    }
    IEnumerator exhaustEnemy() {
        playerFound = true;
        yield return new WaitForSeconds(enemyFollowDuration);
        if(navMeshAgent.remainingDistance > escapeDistance)
          playerFound = false;
    }
    void Update () {
        
        //Find player and attack him
        if (!playerHealth.isPlayerDead() && (PlayerInView() || playerFound)) { // Check OR playerFound as well to make enemy always follow the player, once found.
            
            navMeshAgent.SetDestination(player.transform.position);
            navMeshAgent.autoBraking = true;
            navMeshAgent.speed = followSpeed;
            if ( !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= approach) {
                navMeshAgent.Stop();
                
                Attack();
            }
            else {
                navMeshAgent.Resume();
            }
        }
        //Check if any alarm goes off
        else if (checkingAlarm) {
            navMeshAgent.autoBraking = true;
            navMeshAgent.speed = checkAlarmSpeed;
            if (navMeshAgent.remainingDistance <= approach && !navMeshAgent.pathPending) {
                checkingAlarm = false;
                print(gameObject.name + "approached");
                navMeshAgent.Stop();
            }
        }
        //Patrol the patrol points
        else {
            navMeshAgent.speed = patrolSpeed;
            if(navMeshAgent.autoBraking)
                navMeshAgent.autoBraking = false;
            if (navMeshAgent.remainingDistance < approach)
                GotoNextPoint();

        }

    }


    void GotoNextPoint() {
        // Returns if no points have been set up
        if (patrolPoints.Length == 0)
            return;

      
        navMeshAgent.destination = patrolPoints[destPoint].position;
        destPoint = (destPoint + 1) % patrolPoints.Length;
    }

    public void CheckAlarm(Vector3 alarmPos) {
        checkingAlarm = true;
        audioSource.PlayOneShot(alarmClip , 0.5f);
        navMeshAgent.SetDestination(alarmPos);
        print("checking alarm " + gameObject.name);
    }
    /*
    IEnumerator Shout () {
        shoutRange.enabled = true;
        print("shouting");
        yield return new WaitForSeconds(0.2f);
        shoutRange.enabled = false;
        yield return new WaitForSeconds(5.0f);


    }
     */
    bool PlayerInView() {

        RaycastHit hit;
        
        Vector3 rayDirection = (rayCastPlayer.position - transform.position).normalized;
      

            if (Physics.Raycast(transform.position , rayDirection ,  out hit ,maxDetectDistance ,~shootable, QueryTriggerInteraction.Ignore)) {
            
                
            /*       //DEBUGGING CODE - DO NOT DELETE 
                Debug.Log("hit" + hit.collider.gameObject.tag + hit.collider.gameObject.name + " by " + gameObject.name);
                Debug.DrawRay(transform.position , rayDirection.normalized * maxDetectDistance);
                
                    
                if (Vector3.Angle(rayDirection , transform.forward) <= fieldOfView)
                    Debug.Log("Player in fov");
                if (playerController.isLit())
                    Debug.Log("Player lit");

    */
                if ((hit.transform.CompareTag("Player1"))) {
                    if (playerController.isLit() && (Vector3.Angle(rayDirection , transform.forward) <= fieldOfView)) {
                    //  StartCoroutine(Shout());
                    //                 Debug.LogError("Player found (lit)");
                    //Makes noise only on first detection
                    if (!detectedOnce) {
                        audioSource.PlayOneShot(alarmClip , 0.5f);
                        detectedOnce = true;
                    }
                        StartCoroutine(exhaustEnemy());
                    return true;
                    }
                    else if ((hit.transform.position - transform.position).sqrMagnitude <= minPlayerDetectDistance * minPlayerDetectDistance && (Vector3.Angle(rayDirection , transform.forward) <= fieldOfView)) {
                    // StartCoroutine(Shout());
                    //                Debug.LogError("Player in minrange");
                    if (!detectedOnce) {
                        audioSource.PlayOneShot(alarmClip , 0.5f);
                        detectedOnce = true;
                    }
                    StartCoroutine(exhaustEnemy());
                        return true;

                    }
                    else return false;
                }

                else return false;

            }
        
        
        return false;



    }



    void Attack() {
    
        if (playerHealth.currentHealth > 0 && Time.time >= nextFire && enemyHealth.currentHealth > 0 ) {
            animator.SetTrigger("attack");
            playerHealth.TakeDamage(attackDamage);
            nextFire = Time.time + timeBetweenAttacks;
            attacking = true;


            //play audio
            int i = Random.Range(0 , attackClip.Length);
            audioSource.PlayOneShot(attackClip[i] , 0.5f);
        }
        else 
            attacking = false; 
    }

  


}

