using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class guidance_ui : MonoBehaviour
{
    public GameObject messagecanvas,uiCanvas;
    public Text message;
    public bool first_time,first_warning,first_intract,first_advice,first_lasor;
    // Use this for initialization
    void Start()
    {
    }
    IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PollutionBar") {
            other.gameObject.SetActive(false);
            uiCanvas.SetActive(false);
            messagecanvas.SetActive(true);
            Time.timeScale = 0;
            message.text = "At the bottom of the screen is the Pollution Bar, try to keep the pollution level as low as possible to get a highscore.";
        }
        if (other.gameObject.name == "Introduction") {
            other.gameObject.SetActive(false);
            uiCanvas.SetActive(false);
            messagecanvas.SetActive(true);
            Time.timeScale = 0;
            message.text = "Welcome to the Who Cares Tutorial. In this game you will need to solve puzzles and think smartly to reach the Big Tree at the end of every level to win. The tutorial is supported by a guidance system to help you get started.";
        }
        if (other.gameObject.tag =="lasor"&&first_lasor==false&&first_advice==false)
        {
            uiCanvas.SetActive(false);
            first_lasor = true;
            messagecanvas.SetActive(true);          
            Time.timeScale = 0;
            message.text = "There must be some way to disable this laser.";
            //print(other.gameObject.name);
        }
        if (other.gameObject.tag == "plate"&&first_advice==false)
        {
            
            first_advice = true;
            uiCanvas.SetActive(false);
            
            Time.timeScale = 0;
            yield return new WaitForSecondsRealtime(1);
            
            messagecanvas.SetActive(true);

             
            message.text = "Looks like placing weight on this plate deactivates the laser, Maybe I can drag one of these boxes here.";
        }
        if (other.gameObject.tag == "Movable"&&first_time==false)
        {
            uiCanvas.SetActive(false);
            first_time = true;
            messagecanvas.SetActive(true);
            Time.timeScale = 0;
            message.text = "Looks like this box can be pushed.";
        }
        if (other.gameObject.tag == "warningpoint"&&first_warning==false)
        {
            uiCanvas.SetActive(false);
            first_warning = true;
            messagecanvas.SetActive(true);
            Time.timeScale = 0;
            message.text = "That monster will eat me alive if I don't do something. I'll try to switch the Street Lamp off when he's not looking. There's no way he will see me in the dark";
           // yield return new WaitForSeconds(2);
          //  message.text = "THEY CANT SEE YOU IN THE DARK IF YOU METAINE SOME DITANCE ";
           // yield return new WaitForSeconds(2);
           // message.text = "MAY BE YOU CAN TURN OF THE LIGHT WHEN HE IS AWAY ";
        }
        if (other.gameObject.tag == "Lamp"&&first_intract==false)
        {
            uiCanvas.SetActive(false);
            first_intract = true;
            messagecanvas.SetActive(true);
            Time.timeScale = 0;
            message.text = "Whenever near an interactive object, you will see a button. Press the button to interact with it. Turning this lamp off will decrease the pollution and increase your score!";               
        }
    }
    public void resume()
    {
        Time.timeScale = 1;
        messagecanvas.SetActive(false);
        uiCanvas.SetActive(true);
    }
}
