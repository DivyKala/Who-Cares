using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDrag : MonoBehaviour {

    public float thrust = 5.0f;
    public AudioClip dragClip;


    private AudioSource aSource;
    private Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void move ()
    {//use Addforce to add force on oncollisionenter, disable player movement until box's rigidbody.velocity = 0, then enable it

    }

    void OnCollisionEnter(Collision collision) {
        if (collision.transform.CompareTag("Player1")) {
            if(!aSource.isPlaying) {
                aSource.PlayOneShot(dragClip);
                print("playing");
            }
            Vector3 dir = transform.position - collision.transform.position;
            rb.AddForce(dir * thrust);
        }

    }



 
}
