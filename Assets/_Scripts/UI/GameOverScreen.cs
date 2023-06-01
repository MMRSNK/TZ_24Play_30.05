using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject UI_elements;


    private void Start()
    {
        PlayerStacking.OnGameOver += OnGameOver;
    }
    private void OnDisable()
    {
        PlayerStacking.OnGameOver -= OnGameOver;

    }

    private void OnGameOver()
    {
        UI_elements.SetActive(true);
    }
    
    public void OnRetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
