using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLight : MonoBehaviour {
    //For AI to detect the light, every light object must have "litzone" child. 
    //All litzones must be on the IgnoreRaycastLayer
    public int pollution = 10;
    public AudioClip switchClip;

    private GameObject GameManager;
    private AudioSource audioSource;
    bool alreadyCountedOff = false, alreadyCountedOn = false;
    // Use this for initialization
    void Start() {
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update() {
        
          //  Use();
    }

    private void OnEnable() {
        
       
        
    } 

    public void Use() {
        audioSource.clip = switchClip;
        audioSource.Play();
             List<Light> lights = new List<Light>();

             GetComponentsInChildren<Light>(true , lights);

             alreadyCountedOff = false;
             alreadyCountedOn = false;
             foreach (Light l in lights) {
                 Toggle(l.gameObject);

             }
         
   //     ToggleOneLight(GetComponentInChildren<Light>(true));


    }
 /*   private void ToggleOneLight (Light l) {
        l.gameObject.SetActive(!l.gameObject.activeSelf);
    }
*/

    private void Toggle(GameObject light) {
        if (light.activeSelf) {
            light.SetActive(false);
            if (!alreadyCountedOff) {
                alreadyCountedOff = true;
                ScoreManager.instance.UpdateLightOffCount(1);
                ScoreManager.instance.UpdatePollution(-pollution);
            }
        }
        else {
            light.SetActive(true);
            if (!alreadyCountedOn) {
                alreadyCountedOn = true;
                ScoreManager.instance.UpdatePollution(pollution);
                ScoreManager.instance.UpdateLightOffCount(-1);
            }
            
        }
    }
}
