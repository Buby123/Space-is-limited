using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private bool _gameIsRunning = true;
    public bool gameIsRunning{
        get { return _gameIsRunning; }
    }

    [ContextMenu("Pause")]
    public void PauseGame()
    {
        _gameIsRunning = false;
        Time.timeScale = 0f;
    }

    [ContextMenu("Resume")]
    public void ResumeGame()
    {
        _gameIsRunning = true;
        Time.timeScale = 1f;
        
    }
}
