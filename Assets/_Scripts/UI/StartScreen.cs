using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    public GameObject DragToMoveGO;

    private void Start()
    {
        PlayerInputManager.Instance.OnStartTouch += OnStartTouch;
    }
    private void OnDisable()
    {
        PlayerInputManager.Instance.OnStartTouch -= OnStartTouch;
    }

    private void OnStartTouch()
    {
        if(!gameObject.activeInHierarchy)
            return;

        DragToMoveGO.SetActive(true);
        gameObject.SetActive(false);
    }
}
