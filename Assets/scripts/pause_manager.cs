using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pause_manager : MonoBehaviour
{
    public GameObject pausemenu;
    public Text pause_status;
    // Use this for initialization
    void Start()
    {

    }
    public void pausegame()
    {
        Time.timeScale = 0;
        pausemenu.SetActive(true);
        if (database_main.soundon == true)
            pause_status.text = "SOUND ON";
        else
            pause_status.text = "SOUND OFF";
    }
    public void resume_game()
    {
        Time.timeScale = 1;
        pausemenu.SetActive(false);
    }
    public void exittomenu()
    {
        database_main.leveltoloadlogic = 0;
        Time.timeScale = 1;
        SceneManager.LoadScene(4);

    }
    public void soundonoff()
    {
        if (database_main.soundon == true)
        {
            database_main.soundon = false;
            pause_status.text = "SOUND OFF";
        }
        else
        {
            database_main.soundon = true;
            pause_status.text = "SOUND ON";
        }
    }
    public void RestartLevel () {
        database_main.leveltoloadlogic = 0;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
