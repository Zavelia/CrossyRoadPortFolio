using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{

    public void ReStart()
    {
        if ("StartGameScene" == SceneManager.GetActiveScene().name)
        {
            SceneManager.LoadSceneAsync("ReStartGameScene");
        }
        else SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        Debug.Log("<color=cyan>æ¿ √ ±‚»≠!</color>");
    }
}
