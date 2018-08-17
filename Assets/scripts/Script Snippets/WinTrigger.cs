using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger :levecomplete
{
    public GameObject maincanvas, levelcompletecanvas;
    public AudioClip winSound;
    private AudioSource auSource;
    private bool alreadyShown = false;
	// Use this for initialization
	void Start () {
        auSource = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other) {
        // show win canvas if player reaches win area, future exists, futures has no enemies.
 
        if (other.CompareTag("Player1") && ((EnemySpawner.instance && EnemySpawner.instance.EnemyCount() <= 0) || !EnemySpawner.instance) && !alreadyShown)
        {
            alreadyShown = true;
            auSource.PlayOneShot(winSound);
            levelcompletecanvas.SetActive(true);
            maincanvas.SetActive(false);
            //     setstars(); 
        }

    } 
}
