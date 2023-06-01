using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager Instance { get; private set; }

    public delegate void StartTouch();
    public event StartTouch OnStartTouch;
    public delegate void EndTouch();
    public event EndTouch OnEndTouch;


    private PlayerControlls playerControlls;

    private int _moveAxis = 0;

    private Vector2 _startTouchPosition;
    private Vector2 _touchCurrentPosition;


    [SerializeField] private float _sideMovingOffset;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        playerControlls = new PlayerControlls();
    }
    private void OnEnable()
    {
        playerControlls.Enable();
    }
    private void OnDisable()
    {
        playerControlls.Disable();
    }
    private void Start()
    {
        playerControlls.Touch.PrimaryTouch.started += ctx => StartTouchPrimary(ctx);
        playerControlls.Touch.PrimaryTouch.canceled += ctx => EndTouchPrimary(ctx);
    }
    private void StartTouchPrimary(InputAction.CallbackContext ctx)
    {
        _startTouchPosition = playerControlls.Touch.PrimaryPosition.ReadValue<Vector2>();
        OnStartTouch?.Invoke();
    }
    private void EndTouchPrimary(InputAction.CallbackContext ctx)
    {
        OnEndTouch?.Invoke();
    }

    public int GetMoveAxis()
    {
        if (!playerControlls.Touch.PrimaryTouch.inProgress)
            return 0;
        _touchCurrentPosition = playerControlls.Touch.PrimaryPosition.ReadValue<Vector2>();

        if (_startTouchPosition == Vector2.zero)
        {
            _startTouchPosition = _touchCurrentPosition;
        }


        if (_touchCurrentPosition.x - _sideMovingOffset > _startTouchPosition.x)        
            _moveAxis = 1;        
        else if (_touchCurrentPosition.x + _sideMovingOffset < _startTouchPosition.x)
            _moveAxis = -1;
        else
            _moveAxis = 0;

        return _moveAxis;
    }

}