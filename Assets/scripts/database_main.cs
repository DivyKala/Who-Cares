using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class database_main : MonoBehaviour
{
    public static bool soundon,second_time_login;
    public static int leveltoloadlogic,master_score,master_kills,master_turned_off;
    public static string player_id,playerusername;
    void Start()
    {
     //   Debug.Log(PlayerPrefs.GetInt("secondlogin"));
         //PlayerPrefs.DeleteAll();
        if (PlayerPrefs.GetInt("secondlogin") == 1)
            second_time_login = true;
        else
            second_time_login = false;
        player_id = PlayerPrefs.GetString("playerid");
        playerusername = PlayerPrefs.GetString("username");
        master_score = PlayerPrefs.GetInt("master_score");
        master_kills = PlayerPrefs.GetInt("master_kills");
        master_turned_off = PlayerPrefs.GetInt("master_turnedoff");
        if (PlayerPrefs.GetInt("sound") == 1)
            soundon = true;
        else
            soundon = false;
    }
	void Update ()
    {
     
	}
}
