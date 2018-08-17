using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;


public class ArrowKeys : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public string arrowKeyName = "Up";
    private CrossPlatformInputManager.VirtualButton vbutton;
    ButtonHandler button;
    private void Awake() {

        button = GetComponent<ButtonHandler>();
        vbutton = new CrossPlatformInputManager.VirtualButton(arrowKeyName);
        CrossPlatformInputManager.RegisterVirtualButton(vbutton);
    } 
    public void OnPointerEnter(PointerEventData eventData) {
        if (arrowKeyName.Equals("Up") || arrowKeyName.Equals("Right")) {
            button.SetAxisPositiveState();
        }
        else if (arrowKeyName.Equals("Down") || arrowKeyName.Equals("Left")) {
            button.SetAxisNegativeState();
        }

    }
    public void OnPointerExit(PointerEventData eventData) {
        button.SetAxisNeutralState();
    }
    private void OnDisable() {
        button.SetAxisNeutralState();
        CrossPlatformInputManager.UnRegisterVirtualButton(arrowKeyName);
        
    } 
}
