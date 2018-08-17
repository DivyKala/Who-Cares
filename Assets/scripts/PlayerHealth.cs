using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour {
    public int startingHealth = 100;
    public int currentHealth;
    public Slider health;
    public Image fillimage;
    public AudioClip[] hitClip;
    public AudioClip deathClip;
    

    public float sinkSpeed = 0.5f;
    
    //    public Slider healthSlider;
    //  public Image damageImage;
    //  public AudioClip deathClip;
    //  public float flashSpeed = 5f;
    //  public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    private AudioSource aSource;
    Animator anim;
    // AudioSource playerAudio;
    Player playerMovement;
    Shooter shooter;
    bool isDead;
    bool damaged;
    bool isSinking;


    void Awake()
    {
        aSource = GetComponent<AudioSource>();
        health.value = startingHealth;
        anim = GetComponent <Animator> ();
        //    playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <Player> ();
        shooter = GetComponentInChildren <Shooter> ();
        currentHealth = startingHealth;
        fillimage.color = Color.green;
    }


    void Update() {

        if(isSinking) {
            transform.Translate( Vector3.down * sinkSpeed * Time.deltaTime);
        }

        /* if(damaged)
         {
             damageImage.color = flashColour;
         }

         else
         {
             damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
         }
         damaged = false;
         */

    }


    public void TakeDamage(int amount) {
        if(isPlayerDead()) {
            return;
        }
        damaged = true;
        int i = Random.Range(0 , hitClip.Length);
        aSource.clip = hitClip[i];
        aSource.Play();

        currentHealth -= amount;
        health.value = currentHealth;
        if(currentHealth <= 40)
            fillimage.color = Color.red;
        //        healthSlider.value = currentHealth;

        //      playerAudio.Play ();

        if (currentHealth <= 0 && !isDead) {
            Death();
        }
    }


    void Death() {
        health.value = 0;
        StartSinking();

        shooter.DisableEffects();
        isDead = true;
        anim.SetTrigger("Die");



        aSource.clip = deathClip;
        aSource.Play();



        playerMovement.enabled = false;
        shooter.enabled = false;
        Invoke("LoadLoseUI" , 3f);
    }

    void LoadLoseUI() {
        ScoreManager.instance.ShowLoseScreen();
    }


  

    public bool isPlayerDead () {
        return isDead;
    }

    void StartSinking() {
        Collider col = GetComponent<Collider>();
        if (col)
            col.isTrigger = true;        
        Destroy(gameObject , 5f);
        isSinking = true;
    }

}
