using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Laser : Door {
    public ParticleSystem l1,l2,l3;
    public float deactivateTime = 2f;
    public Light lightToDestroy;




    private AudioSource aSource;

    bool deactivating = false;

    private ShimmerLight sl;
    private NavMeshObstacle navMeshObs;
    private float initIntensity;
	// Use this for initialization
	void Start () {
        aSource = GetComponent<AudioSource>();
        sl = lightToDestroy.GetComponent<ShimmerLight>();
        navMeshObs = GetComponent<NavMeshObstacle>();
        initIntensity = lightToDestroy.intensity;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void turnOn () {
        aSource.Stop();
        /*
        if (IsInvoking("deactivate"))
            CancelInvoke("deactivate");

        l1.Play();
        l2.Play();
        l3.Play();
        lightToDestroy.intensity = initIntensity;
        sl.enabled = true;
        */

    }
    public override void turnOff () {
        aSource.Play();
        gameObject.SetActive(false);
        
       /*
        l1.Stop();
        l2.Stop();
        l3.Stop();
        sl.enabled = false;
        lightToDestroy.intensity -= 25 * Time.deltaTime;
        Invoke("deactivate", deactivateTime);
        */
       
    }

    void deactivate () {
       
       
        gameObject.SetActive(false);
        
    }


}
