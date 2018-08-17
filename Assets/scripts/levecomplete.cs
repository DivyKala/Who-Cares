using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class levecomplete : MonoBehaviour
{
    public Text polution,score, kills, lightsoff;
    public GameObject s1, s2, s3;
    public  int maxlights,maxenemyes,newscore,leveltoloadnext;
    HighscoresDemo u;
    public static bool level_complete;
    public void setstars()
    {
 
    }
    void Start()
    {
      //DIVY  ScoreManager.pollution = ScoreManager.startingPollution +ScoreManager.pollution;
        if (maxenemyes != 0)
            ScoreManager.score = (((ScoreManager.lightOffCount / maxlights) + (ScoreManager.enemiesKilled / maxenemyes)) / 2) - (ScoreManager.pollution - ScoreManager.startingPollution);
        else
            ScoreManager.score = ((ScoreManager.lightOffCount / maxlights)) - (ScoreManager.pollution - ScoreManager.startingPollution);       
        newscore = ScoreManager.score;
        level_complete = true;
        if (newscore < 0)
        newscore = 10;
        if (newscore > 90)
        {
            s1.SetActive(true);
            s2.SetActive(true);
            s3.SetActive(true);
        }
        else if (newscore > 60)
        {
            s1.SetActive(true);
            s2.SetActive(true);
        }
        else
        {
            s1.SetActive(true);
        }
        polution.text = "POLUTION " + ScoreManager.pollution;
        score.text = "SCORE " + newscore;
        kills.text = "KILLS " + ScoreManager.enemiesKilled;
        lightsoff.text = "LIGHTS OFF " + ScoreManager.lightOffCount;
        database_main.master_score +=newscore;
        database_main.master_turned_off += ScoreManager.lightOffCount;
        database_main.master_kills += ScoreManager.enemiesKilled;
        PlayerPrefs.SetInt("master_score", database_main.master_score);
        PlayerPrefs.SetInt("master_kills", database_main.master_kills);
        PlayerPrefs.SetInt("master_turnedoff", database_main.master_turned_off);
    }
    public void loadmenu()
    {
        database_main.leveltoloadlogic = 0;
        SceneManager.LoadScene(4);
    }
    public void nextlevel()
    {
        database_main.leveltoloadlogic = leveltoloadnext;

        SceneManager.LoadScene(4);
    }

}
