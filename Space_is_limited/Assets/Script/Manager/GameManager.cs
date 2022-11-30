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
    public void pauseGame()
    {
        _gameIsRunning = false;
        Time.timeScale = 0f;
    }

    [ContextMenu("Resume")]
    public void resumeGame()
    {
        _gameIsRunning = true;
        Time.timeScale = 1f;
        
    }
}
