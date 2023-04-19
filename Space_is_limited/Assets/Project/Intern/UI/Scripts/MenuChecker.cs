using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuChecker : MonoBehaviour
{
    [SerializeField] private GameObject Canvas;

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        IngameMenuChecker();
    }

    /// <summary>
    /// Check if the ingame menu should be opened or closed. 
    /// 
    /// Also takes into consideration if the options menu is loaded.
    /// If the options menu is loaded, the ingame menu should not react to any inputs!
    /// </summary>
    private void IngameMenuChecker()
    {
        if (Canvas.activeSelf)
        {
            if (CheckOptionsLoaded())
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Canvas.SetActive(false);
                OutgameManager.Instance.ResumeGame();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Canvas.SetActive(true);
                OutgameManager.Instance.PauseGame();
            }
        }
    }

    /// <summary>
    /// Check if the Options/Settings menu is loaded
    /// </summary>
    private bool CheckOptionsLoaded()
    {
        Scene optionsScene = SceneManager.GetSceneByName("OptionsMenu");
        return optionsScene.isLoaded;
    }
}
