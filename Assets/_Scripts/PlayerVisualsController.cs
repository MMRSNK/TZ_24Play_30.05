using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualsController : MonoBehaviour
{
    public Animator animator;
    public ParticleSystem PoofEffect;
    public ParticleSystem WarpEffect;

    private ObjectPool<PopupTextController> PopupTextPool;
    public PopupTextController prefab;
    public Transform PopupTextSpawnPosition;

    private bool _isGameOver = false;


    private void Awake()
    {
        PopupTextPool = new ObjectPool<PopupTextController>(prefab, 4, null);
    }
    private void Start()
    {
        PlayerStacking.OnCubeStacked += OnCubesStacked;
        PlayerStacking.OnGameOver += OnGameOver;
        PlayerInputManager.Instance.OnStartTouch += OnStartTouch;
        PlayerInputManager.Instance.OnEndTouch += OnEndTouch;

    }
    private void OnDisable()
    {
        PlayerStacking.OnCubeStacked -= OnCubesStacked;
        PlayerStacking.OnGameOver -= OnGameOver;
        PlayerInputManager.Instance.OnStartTouch -= OnStartTouch;
        PlayerInputManager.Instance.OnEndTouch -= OnEndTouch;
    }

    private void OnGameOver()
    {
        _isGameOver = true;
        WarpEffect.gameObject.SetActive(false);
    }

    private void OnStartTouch()
    {
        if (!_isGameOver)
            WarpEffect.gameObject.SetActive(true);
    }
    private void OnEndTouch()
    {
        WarpEffect.gameObject.SetActive(false);
    }


    private void OnCubesStacked()
    {
        animator.SetTrigger("Jump");
        PoofEffect.Play();
        var popUpText = PopupTextPool.GetFreeElement();
        popUpText.transform.position = PopupTextSpawnPosition.position;
        popUpText.PopText();

    }
}
