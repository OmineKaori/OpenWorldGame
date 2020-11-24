using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    #region Singleton

    public static Player instance;

    void Awake()
    {
        instance = this;
    }   

    #endregion
    
    public AudioClip fallingSound;
    public AudioSource audioSource;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void KillPlayer()
    {
        audioSource.clip = fallingSound;
        audioSource.Play();
        gameManager.EndGame();
    }
}
