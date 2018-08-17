using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmArea : MonoBehaviour {

    public Transform alarm;

    private void OnTriggerEnter(Collider other) {

        // To detect the enemies once the alarm has been set
        if (other.CompareTag("Enemy")) {

         other.GetComponent<Enemy1>().CheckAlarm(alarm.position);
            print("prompted" + other.name);
        }
    }
}
