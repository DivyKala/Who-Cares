using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioMixerManager : MonoBehaviour {
    public AudioMixerSnapshot unpaused, paused;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale == 0)
            paused.TransitionTo(0.01f);
        else
            unpaused.TransitionTo(0.01f);
	}
}
