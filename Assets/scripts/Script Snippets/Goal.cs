using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {
    public GameObject weight;
    public Door door;
    bool player = false, box = false;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other) {
    
        if(other.gameObject.Equals(weight)) {
            box = true;
            if(door)
                door.turnOff(); 
        }
        if (other.CompareTag("Player1")) {
            player = true;
            if (door)
                door.turnOff();
        }
    } 
    void OnTriggerExit(Collider other) {
        if (other.gameObject.Equals(weight)) {
            box = false;
            
        }
        if(other.CompareTag("Player1")) {
            player = false;
        }



        if(!box && !player && !door.gameObject.activeInHierarchy) {
            door.gameObject.SetActive(true);
        }
    }
}
