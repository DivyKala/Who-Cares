using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Shooter : MonoBehaviour {
    public enum PlayerType { Player1, Player2 };
    public PlayerType playerType;
    public int damagePerShot = 20;                  // The damage inflicted by each bullet.
    public float timeBetweenBullets = 0.15f;        // The time between each shot.
    public float range = 25f;                      // The distance the gun can fire.
    public int ammo = 5;
    public Transform firePoint;
    public GameObject aimPoint;
    public LineRenderer aimLine;
    public Color fromColor = Color.red, toColor = Color.yellow;
    public ParticleSystem gunParticles;                    // Reference to the particle system.
    public Text ammo_text;
    public GameObject muzzlePrefab;    //For player1, so that the PS can spawned out of player's hierarchy
    public AudioClip shootClip;


    private AudioSource aSource;
    float timer;                                    // A timer to determine when to fire.
    Ray shootRay,aimRay;                                   // A ray from the gun end forwards.
    RaycastHit shootHit,aimHit;                            // A raycast hit to get information about what was hit.
    int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
    
    LineRenderer gunLine;                           // Reference to the line renderer.
 //   AudioSource gunAudio;                           // Reference to the audio source.
    Light gunLight;                                 // Reference to the light component.
    float effectsDisplayTime = 0.2f;                // The proportion of the timeBetweenBullets that the effects will display for.
    private Transform muzzleStart;

    void Awake() {
        // Create a layer mask for the Shootable layer.
        aSource = GetComponent<AudioSource>();
        shootableMask = LayerMask.GetMask("Shootable");
 
        // Set up the references.
     
        gunLine = GetComponent<LineRenderer>();

    //    gunAudio = GetComponent<AudioSource>();
        gunLight = firePoint.GetComponent<Light>();
        if(ammo_text)
            ammo_text.text = "" + ammo;
        muzzleStart = gunParticles.transform;
    }

    void OnStart()
    {
       
    }
    void Update() {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        if (CrossPlatformInputManager.GetButton("Aim"))
        {         
          Aim();       
        }
        else if(CrossPlatformInputManager.GetButtonUp("Aim")) {
            aimLine.enabled = false;
        }
        


        // If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
        if (timer >= timeBetweenBullets * effectsDisplayTime) {
            // ... disable the effects.
            DisableEffects();
        }
    }

    private void Aim() {
           aimLine.enabled = true;

           aimLine.material.color =   Color.Lerp(fromColor , toColor , Mathf.PingPong(Time.time, 1));   
        aimLine.SetPosition(0 , aimPoint.transform.position);
           aimRay.origin = aimPoint.transform.position;
           aimRay.direction = aimPoint.transform.forward;

           if (Physics.Raycast(aimRay , out aimHit , range, Physics.DefaultRaycastLayers ,QueryTriggerInteraction.Ignore ))
        {
           
            aimLine.SetPosition(1 ,aimHit.point);

           }
           else {
               // ... set the second position of the line renderer to the fullest extent of the gun's range.
               aimLine.SetPosition(1 , aimRay.origin + aimRay.direction * range);
           }
           


    }

    public void DisableEffects() {  
        // Disable the line renderer and the light.
        gunLine.enabled = false;
        gunLight.enabled = false;
    }
    public void StartShoot () {

        if (timer >= timeBetweenBullets && ammo > 0) {

            print("timer is ready");
            Shoot();
        }
        
    }

    public void UpdateAmmoUI () {
        if (ammo_text) {
            ammo--;
            ammo_text.text = "" + ammo;
        }
    }
    public void Shoot() {
        UpdateAmmoUI();
        // Reset the timer.
        timer = 0f;

        // Play the gun shot audioclip.
        aSource.PlayOneShot(shootClip);

        // Enable the light.
        gunLight.enabled = true;

        // Stop the particles from playing if they were, then start the particles.
        if (playerType == PlayerType.Player1) {
            GameObject muzzle = Instantiate(muzzlePrefab , muzzleStart.position , muzzleStart.rotation);
            ParticleSystem mz = muzzle.GetComponent<ParticleSystem>();
            mz.Stop();
            mz.Play();
        }
        else {
            gunParticles.Stop();
            gunParticles.Play();
        }
        // Enable the line renderer and set it's first position to be the MUZZLE.
        gunLine.enabled = true;
        gunLine.SetPosition(0 , muzzleStart.position);

        // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
        shootRay.origin = firePoint.position;
        shootRay.direction = firePoint.forward;

        // Perform the raycast against gameobjects on the shootable layer and if it hits something...
        if (Physics.Raycast(shootRay , out shootHit , range , Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore)) {
            // Try and find an EnemyHealth script on the gameobject hit.
            print("ray hit " + shootHit.collider.gameObject.name);
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

            // If the EnemyHealth component exist...
            if (enemyHealth != null) {
                //     print("enemy found" + enemyHealth.gameObject.name);
                // ... the enemy should take damage.
                enemyHealth.TakeDamage(damagePerShot);
                //   print("enemy damage called");
            }


            else {
                Explosive explosive = shootHit.collider.GetComponent<Explosive>();
                if (explosive) {
                    explosive.Explode(shootHit.point);

                }
            }




            // Set the second position of the line renderer to the point the raycast hit.
            gunLine.SetPosition(1 , shootHit.point);
        }
        // If the raycast didn't hit anything on the shootable layer...
        else {
            // ... set the second position of the line renderer to the fullest extent of the gun's range.
            gunLine.SetPosition(1 , shootRay.origin + shootRay.direction * range);
        }
    }
}