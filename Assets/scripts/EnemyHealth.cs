using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    public enum EnemyType { Present, Future};
    public EnemyType enemyType;

    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;
    public AudioClip[] hitClip;

    private AudioSource enemyAudio;
    Animator anim;
    //   AudioSource enemyAudio;
    ParticleSystem hitParticles; //particle system activated when enemy is hit 
    Collider sphereCollider;//Used to make enemy passable when dying 
    bool isDead;
    bool isSinking = false;
    private Rigidbody rb;

    void Awake() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        sphereCollider = GetComponent<Collider>();

        currentHealth = startingHealth;
    }


    void Update() {
        if (isSinking) {
         
            rb.MovePosition(rb.position + Vector3.down * sinkSpeed * Time.deltaTime);
         }
    }


    public void TakeDamage(int amount) {
        if (isDead)
            return;

        //     enemyAudio.Play ();

        currentHealth -= amount;


        int i = Random.Range(0 , hitClip.Length);
        enemyAudio.clip = hitClip[i];
        enemyAudio.Play();


        /*   TODO:BLOOD particles
         *     hitParticles.transform.position = hitPoint;
             hitParticles.Play();
             */
        if (currentHealth <= 0) {
            Death();

        }

    }


    void Death() {
        if(enemyType == EnemyType.Future) {
            EnemySpawner.instance.UnregisterEnemy(gameObject);
        }
        ScoreManager.instance.UpdateEnemiesKilled(1);
        ScoreManager.instance.UpdateScore(scoreValue);
        isDead = true;
        
    //    
      //    

  //TODO: set up animation      anim.SetTrigger("Dead");
        StartSinking();

        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }


    public void StartSinking() {

        //TODO: ENTER SCRIPT HERE TO STOP MOVING  (currently disabling AI to make this work)   GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        
        if (sphereCollider)
            sphereCollider.isTrigger = true;
        rb.isKinematic = false;
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        Enemy1 e1 = GetComponent<Enemy1>();

        if (e1) {
            e1.enabled = false;

        }
        else {
            Enemy2 e2 = GetComponent<Enemy2>();
            if (e2) {
                e2.enabled = false;
            }
        }
        //ScoreManager.score += scoreValue;
        isSinking = true;
        Destroy(gameObject , 2f);
    }


}
