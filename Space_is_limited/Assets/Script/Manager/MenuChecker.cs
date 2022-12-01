using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuChecker : MonoBehaviour
{
    [SerializeField] GameObject Canvas;

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        //Open the ingame menu and stop the game Time
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Canvas.SetActive(!Canvas.activeSelf);

            if (Canvas.activeSelf)
            {
                GameManager.Instance.PauseGame();
            }else
            {
                GameManager.Instance.ResumeGame();
            }
        }
    }
}
