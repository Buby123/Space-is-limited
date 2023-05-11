using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is responsible for showing or hiding the ingame menu.
/// It also has to stop or resume the game.
/// </summary>
public class MenuChecker : MonoBehaviour
{
    [SerializeField] private GameObject Canvas;

    /// <summary>
    /// Add Event-Listener for key presses
    /// </summary>
    void Start()
    {
        PlayerInput.Instance.OnMenu.AddListener(SwitchMenu);
    }

    /// <summary>
    /// only implemented for making debugging easier.
    /// If the player presses "Backspace", the game is restarted.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown("backspace"))
        {
            OutgameManager.Instance.LoadOtherScene(OutgameManager.MainScenes.Game);
        }
    }

    /// <summary>
    /// Opens or closes the Ingame Menu UI if the Options Menu is not displayed currently.
    /// This also stops or resumes the Game.
    /// </summary>
    private void SwitchMenu()
    {
        if (CheckOptionsLoaded())
        {
            return;
        }

        if (Canvas.activeSelf)
        {
            Canvas.SetActive(false);
            OutgameManager.Instance.ResumeGame();
        }
        else
        {
            Canvas.SetActive(true);
            OutgameManager.Instance.PauseGame();
        }

    }

    /// <summary>
    /// Closes the Ingame Menu UI if called.
    /// A call usually happens from a Ingame Menu Button (Resume)
    /// </summary>
    public void DisableMenu()
    {
        Canvas.SetActive(false);
        OutgameManager.Instance.ResumeGame();
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
