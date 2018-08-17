using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class JoystickRange : MonoBehaviour {

    public Joystick j;
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position , j.MovementRange);
        Gizmos.DrawWireSphere(transform.position , j.shootRange);
    }
}
