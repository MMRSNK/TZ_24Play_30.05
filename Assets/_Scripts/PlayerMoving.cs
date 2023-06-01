using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    [SerializeField] private float _xMovementLimit;
    [SerializeField] private float _moveSpeedForward;
    [SerializeField] private float _moveSpeedAdditional;
    [SerializeField] private float _moveSpeedHorizontal;


    private float _horizontalMoveInput = 0;
    private float _newXpos;

    private bool _isMoving;
    private bool _isGameOver;

    private void OnEnable()
    {
        PlayerInputManager.Instance.OnStartTouch += PlayerInputManager_OnStartTouch;
        PlayerInputManager.Instance.OnEndTouch += PlayerInputManager_OnEndTouch;
        PlayerStacking.OnGameOver += OnGameOver;
    }
    private void OnDisable()
    {
        PlayerInputManager.Instance.OnStartTouch -= PlayerInputManager_OnStartTouch;
        PlayerInputManager.Instance.OnEndTouch -= PlayerInputManager_OnEndTouch;
        PlayerStacking.OnGameOver -= OnGameOver;
    }

    private void OnGameOver()
    {
        _isGameOver = true;
    }

    private void PlayerInputManager_OnStartTouch()
    {
        _isMoving = true;
        _moveSpeedForward += _moveSpeedAdditional;
    }
    private void PlayerInputManager_OnEndTouch()
    {
        // _isMoving = false;
        _moveSpeedForward -= _moveSpeedAdditional;
    }

    private void Update()
    {
        HandleMovingInput();
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (_isMoving && !_isGameOver)
        {
            _newXpos = transform.position.x + _horizontalMoveInput * _moveSpeedHorizontal * Time.deltaTime;
            _newXpos = Mathf.Clamp(_newXpos, -_xMovementLimit, _xMovementLimit);
            transform.position = new Vector3(_newXpos, transform.position.y, transform.position.z);
            transform.Translate(Vector3.forward * _moveSpeedForward * Time.deltaTime);
        }
    }

    private void HandleMovingInput()
    {
        if (_isMoving && !_isGameOver)
            _horizontalMoveInput = PlayerInputManager.Instance.GetMoveAxis();
        else
            _horizontalMoveInput = 0;
    }

    

}
