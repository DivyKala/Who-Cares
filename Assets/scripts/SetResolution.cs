using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetResolution : MonoBehaviour {
      
     
    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene , LoadSceneMode mode) {
        Screen.SetResolution(900 , 506 , true);
        Camera.main.aspect = 16f / 9f;
    }
}
