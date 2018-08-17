using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fpssd200 : MonoBehaviour {
    Rect fpsrect;
    GUIStyle style;
    static float fps;
	// Use this for initialization
	void Start ()
    {
        fpsrect = new Rect(0, 100,400,100);
        style = new GUIStyle();
        style.fontSize= 40;
        
        
	}
     void OnGUI()
    {
        GUI.color = Color.red;
        fps = 1 / Time.deltaTime;
        GUI.Label(fpsrect , "FPS :: " + fps);//,style);
    }

}
