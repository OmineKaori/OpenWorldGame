using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager instance;
    public Animator transition;
    public Text crossFadeText;
    
    void Awake()
    {
        instance = this;
    }   

    #endregion
    
    public void EndGame()
    {
        crossFadeText.text = "GAME OVER...";
        StartCoroutine(LoadScene());
    }

    public void GameClear()
    {
        crossFadeText.text = "GAME CLEAR!";
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        transition.SetTrigger("Start");
        
        yield return new WaitForSeconds(1);
        
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
