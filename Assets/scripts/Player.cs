using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {
    public AudioClip[] coughClip, footstepsClip;
    public AudioClip dragClip, pickUpClip;
    public enum PlayerType { Player1, Player2};
    public PlayerType playerType;
   public float rangeDebug;
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float turnSpeed = 8f;


    private AudioSource aSource;
    private bool isLitBool = false;
    private PlayerHealth playerHealth;
    private Shooter shooter;
    private Rigidbody playerRigidbody;
    private Animator animator;
    private Collider dummyLit;

   
        
    


    void Start() {
        aSource = GetComponent<AudioSource>();
        playerHealth = GetComponent<PlayerHealth>();
        shooter = GetComponent<Shooter>();
        playerRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        camera_switch.scean1 = true;

    }
    public void shoot()
    {
        animator.SetTrigger("shoot");
    }
    void Update () {
        //Range testing code 
        Debug.DrawRay(transform.position , transform.forward.normalized * rangeDebug);



        if (CrossPlatformInputManager.GetButton("Shoot"))
        {
            if (shooter.ammo >= 0) {
                animator.SetBool("shoot" , true);
                print("shoot = " + animator.GetBool("shoot"));
            }
            else {
                animator.SetBool("shoot" , false);
                print("shoot = " + animator.GetBool("shoot"));
            }
        }
        else if(CrossPlatformInputManager.GetButtonUp("Shoot"))
        {
            animator.SetBool("shoot",false);
        }



        if (isLitBool) {
            if(!dummyLit.gameObject.activeInHierarchy) {
                isLitBool = false;
            }

        }

       
        if(playerType == PlayerType.Player2 && !IsInvoking("PlayCough")) {
            int wait = Random.Range(6 , 15);
            
            Invoke("PlayCough" , wait);
        }
     
    }

    void PlayCough() {
        
        int i = Random.Range(0 , coughClip.Length);
        aSource.PlayOneShot(coughClip[i]);
                
    }


    void FixedUpdate() {
        // Store the input axes.
        float h = CrossPlatformInputManager.GetAxisRaw("Horizontal1");
        float v = CrossPlatformInputManager.GetAxisRaw("Vertical1");

        // Move the player around the scene.
        Move(h , v);

        // Turn the player according to WASD and Joystick
        Turning(h, v);

        // Animate the player
        Animate(h , v);
    }

    void Animate (float h, float v) {
        if (h != 0 || v != 0) {
            animator.SetBool("running" , true);
            int i = Random.Range(0 , footstepsClip.Length);
            if (!aSource.isPlaying) {
                aSource.PlayOneShot(footstepsClip[i]);
            }
        }
        else
            animator.SetBool("running" , false);
    }

    void Move(float h , float v) {
        
        // Move the player to it's current position plus the input.
        playerRigidbody.MovePosition(transform.position + (new Vector3(h, 0f, v).normalized * moveSpeed * Time.deltaTime));
        
    }

    void Turning(float h, float v) {
        Vector3 lookDir = new Vector3(CrossPlatformInputManager.GetAxis("HorizontalLook") , 0f , CrossPlatformInputManager.GetAxis("VerticalLook")) * Time.deltaTime; ;
        if (lookDir != Vector3.zero) {

            transform.rotation = Quaternion.LookRotation(lookDir);
        }
        else{
            Vector3 direction = new Vector3(h, 0f, v);
            Vector3 currentDirection = Vector3.Slerp(transform.forward ,direction  , Time.deltaTime * turnSpeed);

            transform.rotation = Quaternion.LookRotation(currentDirection);
            

        }
    }

    void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Lamp")) {
            
            if(!ButtonHandler.useButton.activeInHierarchy)
              ButtonHandler.useButton.SetActive(true);

            if(CrossPlatformInputManager.GetButtonDown("Use")) {
                print("received down");
            }
            if (CrossPlatformInputManager.GetButtonUp("Use")) {
                print("received Up");
            }
            if (CrossPlatformInputManager.GetButtonUp("Use")) {
                print("player called Use()" + other.name);
                
                other.gameObject.GetComponent<ToggleLight>().Use();
            }
            /* TURRETS WERE REMOVED
            else if (other.gameObject.CompareTag("Turret")) {
                other.gameObject.transform.FindChild("Head").FindChild("Fire point").GetComponent<Turret>().Fire();
            }
            */
        }
        
    }


    void OnTriggerEnter(Collider col) {
        if (col.CompareTag("Litzone")) {
            dummyLit = col;
            isLitBool = true;
        }
        if(col.CompareTag ("ammo")) {
            shooter.ammo += col.GetComponent<AmmoPickUp>().ammo;
            shooter.UpdateAmmoUI();
            aSource.PlayOneShot(pickUpClip);
            if(col.gameObject) {
                Destroy(col.gameObject);
            }
        }
    }
    void OnTriggerExit(Collider col) {
        if (col.CompareTag("Litzone")) {
            dummyLit = null;
            isLitBool = false;
        }
        if (col.gameObject.CompareTag("Lamp")) {
            ButtonHandler.useButton.SetActive(false);
        }
    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Movable")) {

        
                aSource.clip = dragClip;
            aSource.volume = 0.2f;
                aSource.Play();
            Invoke("SetVolume" , dragClip.length);
           
                 
        
        }
    }
    private void SetVolume () {
        aSource.volume = 1;
    }


    public bool isLit() {
        return isLitBool;
    }



}
