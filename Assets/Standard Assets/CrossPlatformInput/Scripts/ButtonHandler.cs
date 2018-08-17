using System;
using UnityEngine;

namespace UnityStandardAssets.CrossPlatformInput
{
    
    public class ButtonHandler : MonoBehaviour
    {
        
        public static GameObject useButton;
        public string Name;

        private void Awake() {
            if (!useButton) {
                useButton = GameObject.FindGameObjectWithTag("UseButton");

            }
           
        }
        private void Start() {
            useButton.SetActive(false);
        } 


        public void SetDownState()
        {
            print("down registered");
            CrossPlatformInputManager.SetButtonDown(Name);
        }


        public void SetUpState()
        {
            print("Up registered");
            CrossPlatformInputManager.SetButtonUp(Name);
        }


        public void SetAxisPositiveState()
        {
            CrossPlatformInputManager.SetAxisPositive(Name);
        }


        public void SetAxisNeutralState()
        {
            CrossPlatformInputManager.SetAxisZero(Name);
        }


        public void SetAxisNegativeState()
        {
            CrossPlatformInputManager.SetAxisNegative(Name);
        }

    }
}
