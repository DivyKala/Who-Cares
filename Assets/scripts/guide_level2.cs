using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class guide_level2 : MonoBehaviour
{
  public GameObject messagecanvas;
    public Text message;
    private static int guide_part;
    private bool aumo;
  void Start () 
  {
        guide_part = 1;
   }
    void  OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "guide"&&guide_part==1)
        {
            other.gameObject.SetActive(false);
            guide_part=2;
            messagecanvas.SetActive(true);
            Time.timeScale = 0;
            message.text = "Maybe I can push this truck to trap the monsters ahead";    
        }
        if (other.gameObject.tag == "guide2"&&guide_part!=3)
        {
            other.gameObject.SetActive(false);
            guide_part++;
            messagecanvas.SetActive(true);       
            Time.timeScale = 0;
            message.text = "I must think of another way, going out in the open is too dangerous!";
        }
        if (other.gameObject.tag == "ammo")
        {
            other.gameObject.SetActive(false);
            messagecanvas.SetActive(true);
            Time.timeScale = 0;
            message.text = "Picked up some ammo. I can shoot now";
            aumo = true;
        }
        if (other.gameObject.tag == "guide"&&aumo==true)
        {
            other.gameObject.SetActive(false);
            guide_part=3;
            messagecanvas.SetActive(true);
            Time.timeScale = 0;
            message.text = "If I shoot this fuel barrel to make a loud sound, I could use that as a distraction";    
        }
    }
    public void resume()
    {
        
        Time.timeScale = 1;
        messagecanvas.SetActive(false);
   }   
}
