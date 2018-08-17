using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_switch : MonoBehaviour
{
    public static bool scean1;// no use now may be in future
    public Camera camera_scene1, camera_scene2;
    public GameObject p1,p2;
    public Texture overlay;
	void Start ()
    {
        scean1 = true;
        camera_scene2.rect=new Rect(0f,0.53f,0.29f,0.48f);
        camera_scene1.rect = new Rect(0, 0, 1, 1);
        camera_scene2.depth = 0;
        camera_scene1.depth = -1;
        p1.GetComponent<Player>().enabled = true;
        p2.GetComponent<Player>().enabled = false;
        camera_scene1.GetComponent<AudioListener>().enabled = true;
        camera_scene2.GetComponent<AudioListener>().enabled = false;
        
    }
    public void scene_switch()
    {
        if (scean1 == true)
        {
            camera_scene1.rect = new Rect(0f, 0.5f, 0.3f, 0.5f);
            camera_scene2.rect = new Rect(0, 0, 1, 1);
            camera_scene1.depth = 0;
            camera_scene2.depth = -1;
            scean1 = false;
            p1.GetComponent<Player>().enabled = false;
            p2.GetComponent<Player>().enabled = true;
            camera_scene1.GetComponent<AudioListener>().enabled = false;
            camera_scene2.GetComponent<AudioListener>().enabled = true;

        }
        else
        {
            camera_scene2.rect = new Rect(0f, 0.5f, 0.3f, 0.5f);
            camera_scene1.rect = new Rect(0, 0, 1, 1);
            camera_scene1.depth = -1;
            camera_scene2.depth = 0;
            scean1 = true;
            p1.GetComponent<Player>().enabled = true;
            p2.GetComponent<Player>().enabled = false;
            camera_scene1.GetComponent<AudioListener>().enabled = true;
            camera_scene2.GetComponent<AudioListener>().enabled =false;
        }
    }
       
            
}
