using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public bool buildMode;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake ()
    {
        if (buildMode)
        {
            SceneManager.LoadScene ("Main UI Scene", LoadSceneMode.Additive);
            SceneManager.LoadScene ("Lighting", LoadSceneMode.Additive);
        }
    }
}