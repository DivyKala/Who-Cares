using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class azureloginturnoff : MonoBehaviour {
    public GameObject login, leaderboard;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (database_main.second_time_login == false)
        {
            login.SetActive(true);
            leaderboard.SetActive(false);
        }
        if (database_main.second_time_login == true)
        {
            login.SetActive(false);
            leaderboard.SetActive(true);
        }
    }
}
