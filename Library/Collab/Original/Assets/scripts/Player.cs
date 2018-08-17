using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {


    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float turnSpeed = 8f;



    private bool isLitBool = false;
    private PlayerHealth playerHealth;
    private Shooter shooter;
    private Rigidbody playerRigidbody;
    private Animator animator;



    void Start() {

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
        if (CrossPlatformInputManager.GetButton("Shoot") && !playerHealth.isPlayerDead() && camera_switch.scean1 == true) {

            animator.SetTrigger("shoot");
        }
     
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
        if (CrossPlatformInputManager.GetButtonDown("Use")) {


            if (other.gameObject.CompareTag("Lamp")) {
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
            isLitBool = true;
        }
    }
    void OnTriggerExit(Collider col) {
        if (col.CompareTag("Litzone")) {
            isLitBool = false;
        }
    }
    public bool isLit() {
        return isLitBool;
    }



}
