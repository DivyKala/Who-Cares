using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class guidancescript3 : MonoBehaviour
{   
    public static guidancescript3 instance;
    public GameObject messagecanvas;
    public  Text message;
    private bool once;
    // Use this for initialization
    void Start ()
    {
        instance = this;

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "guide")
        {

            other.gameObject.SetActive(false);          
            messagecanvas.SetActive(true);
            Time.timeScale = 0;
            message.text = "There's an enemy ahead, sometimes violence is the only option";
        }
        if (other.gameObject.tag == "guide2")
        {

            other.gameObject.SetActive(false);      
            messagecanvas.SetActive(true);
            Time.timeScale = 0;              
            message.text = "I should turn the Street Lamps off before advancing, or they'll see me!";
        }
        if (other.gameObject.tag == "guide3")
        {

            other.gameObject.SetActive(false);
            messagecanvas.SetActive(true);
            Time.timeScale = 0;
            message.text = "Maybe I can blow up these explosives to kill these monsters";
        }
        if (other.gameObject.tag == "guide4" && EnemySpawner.instance.EnemyCount() > 0)
        {

            
            messagecanvas.SetActive(true);
            Time.timeScale = 0;
            message.text = "Pie must kill the enemies in the future before continuing.";
        }
    }
    public void resume()
    {

        Time.timeScale = 1;
        messagecanvas.SetActive(false);
    }
  
    public void OhNoMessage () {
       
            messagecanvas.SetActive(true);
            Time.timeScale = 0;
            message.text = "Too bad, exploding fuel has created pollution that Pie must now deal with in Future! Press the mini-view on the top left corner to switch to the Future.";
           
        
    }
}
