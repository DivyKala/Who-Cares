using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class TreeHealth : MonoBehaviour {
    public int startingHealth = 100;
    public int currentHealth;
    public Slider health;
    public Image fillimage;
    public AudioClip deathClip;
    //    public Slider healthSlider;
    //  public Image damageImage;
    //  public AudioClip deathClip;
    //  public float flashSpeed = 5f;
    //  public Color flashColour = new Color(1f, 0f, 0f, 0.1f);



   

 //   private AudioSource aSource;
    bool isDead = false;
    bool damaged;


    void Awake() {
        health.value = startingHealth;
     //   aSource = GetComponent<AudioSource>();
      
        //    playerAudio = GetComponent <AudioSource> ();

        currentHealth = startingHealth;
        fillimage.color = Color.green;
    }


    void Update() {


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
        damaged = true;
        health.value = currentHealth;
        if (currentHealth <= 150)
            fillimage.color = Color.red;
        currentHealth -= amount;



        //      playerAudio.Play ();

        if (currentHealth <= 0 && !isDead) {
            Death();
        }
    }


    void Death() {
        EnemySpawner.instance.UnregisterTree(transform);
        health.value = 0;
        isDead = true;
        AudioSource.PlayClipAtPoint(deathClip , transform.position);
        
        gameObject.SetActive(false);
        print("YOU LOSE!!!"); //Make sure this is not called multiple times by destroying multiple trees  

              


        //    playerAudio.clip = deathClip;
        //  playerAudio.Play ();

        
        
    }


    public void RestartLevel() {
        // SceneManager.LoadScene (0);
    }

    public bool isTreeDead() {
        return isDead;
    }

}
