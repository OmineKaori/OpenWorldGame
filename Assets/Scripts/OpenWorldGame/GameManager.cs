using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }   

    #endregion
    
    public void EndGame()
    {
        SceneManager.LoadScene("Menu");
    }

    public void GameClear()
    {
        SceneManager.LoadScene("Menu");
    }
    
    public void Resume()
    {
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }
}
