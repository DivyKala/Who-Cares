using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontdestroy : MonoBehaviour {
    public GameObject gh;
	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(gh);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
