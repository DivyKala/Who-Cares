using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.EventSystems;
public class UseButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler { 



    public string Name = "Use";


    private CrossPlatformInputManager.VirtualButton vbutton;


    private void Awake() {
        
        if (!CrossPlatformInputManager.ButtonExists(Name)) {
            vbutton = new CrossPlatformInputManager.VirtualButton(Name);
            CrossPlatformInputManager.RegisterVirtualButton(vbutton);
            print("registered");
        }
        if (CrossPlatformInputManager.ButtonExists(Name))
            print("verfied registration");

          //  gameObject.SetActive(false);
    }


    public void OnPointerDown(PointerEventData eventData) { 
        print("down registered");
        CrossPlatformInputManager.SetButtonDown(Name);

  
    }


    public void OnPointerUp(PointerEventData eventData) {
        CrossPlatformInputManager.SetButtonUp(Name);
        print("Up registered");
     //   vbutton.Released();
        
    }
    private void OnDisable() {
        vbutton.Remove();
    }


}
