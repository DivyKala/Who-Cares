using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CameraFollow : MonoBehaviour {
   

    public Transform player;
    public float smooth = 1.5F, zoom = 3f;

    public Transform zoomTrasform;
    private Vector3 offset,zoomOffset;
    private Camera cam;
   
    private void Awake() {

    } 

    // Use this for initialization
    void Start() {
        cam = GetComponent<Camera>();
        if (player) {
            offset = player.position - transform.position;
            
        }
        
        if(zoomTrasform) {
            zoomOffset = player.position - zoomTrasform.position;
        }
        
      
    }
 

    void LateUpdate() {

        if (player) {
            Vector3 abovePos = player.position - offset;
          
            transform.position = Vector3.Lerp(transform.position , abovePos , smooth * Time.deltaTime);

        }
        else {
            return;
        }
        if(player.CompareTag("player2") && !camera_switch.scean1) {
            if(CrossPlatformInputManager.GetButton("Aim") || CrossPlatformInputManager.GetButton("Shoot")) {
                Vector3 zoomPos = player.position - zoomOffset;
                
               
                transform.position = Vector3.Lerp(transform.position , zoomPos , zoom* Time.deltaTime);
            }


        }



     
    }

}
