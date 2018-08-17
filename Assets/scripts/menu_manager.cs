using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menu_manager : MonoBehaviour
{
    public GameObject sound_on, sound_off,back_b,option,about,help,messagebox,start,leaderboard,exit;
    public GameObject help_text, about_text,level_select,maincanvas,leaderboardcanvas;
    bool in_options;
    public Text score, ekilled, lightsturnedof;
	void Start()
    {
        in_options = false;
        option.SetActive(true);
        start.SetActive(true);
        exit.SetActive(true);
        leaderboard.SetActive(true);
        if (database_main.soundon == true)
        {
            sound_on.SetActive(true);
            sound_off.SetActive(false);
        }
        else
        {
            sound_on.SetActive(false);
            sound_off.SetActive(true);
        }
    }
    public void sounon()
    {
        sound_on.SetActive(true);
        sound_off.SetActive(false);
        database_main.soundon = true;
    }
    public void sounoff()
    {
        sound_on.SetActive(false);
        sound_off.SetActive(true);
        database_main.soundon =false;
    }
    public void option_on()
    {
        option.SetActive(false);
        back_b.SetActive(true);
        start.SetActive(false);
        help.SetActive(true);
        about.SetActive(true);
        exit.SetActive(false);
        leaderboard.SetActive(false);
        sound_off.SetActive(false);
        sound_on.SetActive(false);
    }
    public void option_off()
    {
        if (in_options==false)
        {
            level_select.SetActive(false);
            option.SetActive(true);
            back_b.SetActive(false);
            start.SetActive(true);
            help.SetActive(false);
            about.SetActive(false);
            exit.SetActive(true);
            leaderboard.SetActive(true);
            if (database_main.soundon == true)
            {
                sound_on.SetActive(true);
                sound_off.SetActive(false);
            }
            else
            {
                sound_on.SetActive(false);
                sound_off.SetActive(true);
            }
        }
        if (in_options == true)
        {
            level_select.SetActive(false);
            option.SetActive(false);
            back_b.SetActive(true);
            start.SetActive(false);
            help.SetActive(true);
            about.SetActive(true);
            exit.SetActive(false);
            leaderboard.SetActive(false);
            sound_off.SetActive(false);
            sound_on.SetActive(false);
            messagebox.SetActive(false);
            about_text.SetActive(false);
            help_text.SetActive(false);
            in_options = false;
        }
    }
    public void about_on()
    {
        about.SetActive(false);
        help.SetActive(false);
        messagebox.SetActive(true);
        about_text.SetActive(true);
        in_options = true;
    }
    public void help_on()
    {
        about.SetActive(false);
        help.SetActive(false);
        messagebox.SetActive(true);
        help_text.SetActive(true);
        in_options = true;
    }
    public void selectlevel()
    {
        level_select.SetActive(true);
        start.SetActive(false);
        sound_on.SetActive(false);
        sound_off.SetActive(false);
        exit.SetActive(false);
        leaderboard.SetActive(false);
        option.SetActive(false);
        back_b.SetActive(true);
    }
    public void exitgame()
    {
        Application.Quit();
    }
    public void leveltoload(int a)
    {
        database_main.leveltoloadlogic = a;
        SceneManager.LoadScene(4);
    }
    public void leaderboardon()
    {
        leaderboardcanvas.SetActive(true);
        maincanvas.SetActive(false);
        score.text ="SCORE  "+database_main.master_score;
        ekilled.text = "ENEMY KILLED  " + database_main.master_kills;
        lightsturnedof.text = "LIGHTS TURNED OFF  " + database_main.master_turned_off;
    }
    public void backfromlb()
    {
        maincanvas.SetActive(true);
        leaderboardcanvas.SetActive(false);
    }
}
