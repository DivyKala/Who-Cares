using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2 : MonoBehaviour {

    
    public float approach = 2f;
    
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;
    public AudioClip[] attackClip;
    public AudioClip spawnClip;


    private TreeHealth treeHealth;
    private EnemyHealth enemyHealth;


    private NavMeshAgent navMeshAgent;
    private AudioSource audioSource;
    private Animator animator;

    private Transform treeTransform;
    private float timer = 0f;
    private bool attacking = false, attack = false;
   
    private float nextFire = 0f;
    private bool finding = false, initialized = false;
    
    public void Initialize () {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        initialized = true;
    }
    
    public IEnumerator SetTarget (Transform target, float wait = 1f) {
       
        yield return new WaitForSeconds(wait);
        treeHealth = target.GetComponent<TreeHealth>();
        
        navMeshAgent.SetDestination(target.position);
        treeTransform = target;

        
        finding = false;
    }

    void Awake() {
       
        //Will be called after SetTarget


    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(spawnClip);
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("FutureTree")) {
            attack = true;
            if (navMeshAgent.hasPath) {
                
                navMeshAgent.ResetPath();
            }
        }
    }


    void Update() {
        
        if(attack) {
            LookToTarget();
            Attack();
            print("attacking");
        }
        if (treeHealth && treeHealth.isTreeDead()) {
            attack = false;
            print("looking");
            LookForNewTree();
        }
        
     /* Find new target
      *   if(treeHealth && isTreeDead() && !finding) {
            print("new target finding");
            Transform target = EnemySpawner.FindTarget(this);
            StartCoroutine(SetTarget(target,0f));
            finding = true;

        }
        */

    }
    //set target to the closest located tree in the enemyspawner's list
    private void LookForNewTree () {
        
        Transform target = EnemySpawner.FindTarget(this);
        
        if(target)
            StartCoroutine(SetTarget(target));
        

    }

    private void LookToTarget() {
        if(treeTransform)
            transform.rotation = Quaternion.RotateTowards(transform.rotation , Quaternion.LookRotation(treeTransform.position) , 5 * Time.deltaTime);
    }

    public bool isTreeDead () {
        if (treeHealth)
            return treeHealth.isTreeDead();
        else
            return false;
    }


   



    void Attack() {
    //   if(!initialized || finding) {
      //      return;
        //}
        if (treeHealth.currentHealth > 0 && Time.time >= nextFire && enemyHealth.currentHealth > 0) {
            animator.SetTrigger("attack");
            treeHealth.TakeDamage(attackDamage);
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

