using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadinglogic : MonoBehaviour
{
	void Start ()
    {

        SceneManager.LoadSceneAsync(database_main.leveltoloadlogic);             
	}
}
