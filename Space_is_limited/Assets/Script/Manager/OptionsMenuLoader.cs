using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenuLoader : MonoBehaviour
{

    /// <summary>
    /// This script unloads the additively Loaded OptionsMenu. The player gets back where he was before.
    /// </summary>
    public void UnloadOptionsMenu()
    {
        SceneManager.UnloadSceneAsync("OptionsMenu");
    }
}