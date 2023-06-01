using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStacking : MonoBehaviour
{
    [SerializeField] private float _yCubeOffset;
    [SerializeField] private List<GameObject> _playerCubes = new List<GameObject>();

    private Vector3 lastCubePosition = Vector3.zero;

    public static Action OnCubeStacked;
    public static Action OnGameOver;
    

    private void OnEnable()
    {
        Cube.OnCubeCollisionWithWall += OnCubeCollisionWithWall;
        Cube.OnCubeCollisionWithCube += OnCubeCollisionWithCube;

    }


    private void OnDisable()
    {
        Cube.OnCubeCollisionWithWall += OnCubeCollisionWithWall;
        Cube.OnCubeCollisionWithCube -= OnCubeCollisionWithCube;

    }
    private void OnCubeCollisionWithWall(GameObject obj)
    {
        RemovePlayerCube(obj);
    }
    private void OnCubeCollisionWithCube(GameObject obj)
    {
        AddPlayerCube(obj);
    }

    private void RemovePlayerCube(GameObject obj)
    {
        obj.transform.SetParent(null);
        _playerCubes.Remove(obj);
        obj.GetComponent<Cube>().ReloadCube();
    }

    private void AddPlayerCube(GameObject cube)
    {
        if (cube.GetComponent<Cube>().IsInStack())
        {
            return;
        }

        OnCubeStacked?.Invoke();

        UpdateCubePositions();
        _playerCubes.Add(cube);
        SetNewCubePosition(cube);
    }

    private void SetNewCubePosition(GameObject cube)
    {
        cube.transform.SetParent(this.transform.parent);
        cube.transform.localPosition = lastCubePosition;        
    }

    private void UpdateCubePositions()
    {
        foreach (var cube in _playerCubes)
        {
            cube.transform.localPosition = new Vector3(0, cube.transform.localPosition.y + _yCubeOffset, 0);
        }
    }

}
