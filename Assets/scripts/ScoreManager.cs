using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour {
    public static ScoreManager instance;
    public static int startingPollution = 500;
    public Slider pollution_bar;
    public Image polution_colour;
    public pointadd padd;
    public static int pollution, enemiesKilled, lightOffCount, score;
    public Canvas uiControlCanvas, loseCanvas;
	// Use this for initialization
	void Awake()
    {
        instance = this;
        pollution = startingPollution;
        enemiesKilled = lightOffCount =  score = 0;
        if (pollution < 200)
            polution_colour.color = Color.green;
        else if (pollution < 500)
            polution_colour.color = Color.yellow;
        else
            polution_colour.color = Color.red;
        pollution_bar.value = pollution;
    }
   	
	

    public void UpdatePollution (int amount)
    {
        pollution += amount;
        if(amount >= 0 ) {
            padd.pointss(amount);
        }
        else if(amount < 0) {
            padd.pointsn(amount);
        }
        if (pollution < 200)
            polution_colour.color = Color.green;
        else if (pollution < 500)
            polution_colour.color = Color.yellow;
        else
            polution_colour.color = Color.red;
        pollution_bar.value = pollution;
        //Spawn waves in future if pollution exceeds 1000
        if(pollution >= 1000 ) {
            pollution -= 1000;
            if (EnemySpawner.instance) {
                StartCoroutine(EnemySpawner.instance.SpawnWaveRandomly(8)); //spawn 7 monsters
                guidancescript3.instance.OhNoMessage();
            }
        }
    }
    public void UpdateEnemiesKilled(int amount)
    {
        enemiesKilled += amount;
    }
    public void UpdateLightOffCount(int amount) {
        lightOffCount += amount;
        
    }
    public void UpdateScore (int amount) {
        score += amount;
    }
    public void ShowLoseScreen () {
        
        loseCanvas.gameObject.SetActive(true);
        uiControlCanvas.gameObject.SetActive(false);
        Time.timeScale = 0;

    }

}
