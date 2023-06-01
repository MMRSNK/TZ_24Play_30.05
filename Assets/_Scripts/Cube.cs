using System;
using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public static Action<GameObject> OnCubeCollisionWithWall;
    public static Action<GameObject> OnCubeCollisionWithCube;

    private bool _inStack = false;
    private Transform originalParentTranform;
    private Vector3 originalLocalPosition;

    private void Awake()
    {
        originalParentTranform = transform.parent;
        originalLocalPosition = transform.localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Wall")        
            OnCubeCollisionWithWall?.Invoke(this.gameObject);
        
        if (_inStack)        
            return;
        
        if(other.tag == "Cube" || other.tag == "Player")
        {
            OnCubeCollisionWithCube?.Invoke(this.gameObject);
            _inStack = true;
        }
    }
  
    public void ReloadCube()
    {
        StartCoroutine(CubeReload());
    }
    public bool IsInStack()
    {
        return _inStack;
    }

    private IEnumerator CubeReload()
    {
        yield return new WaitForSeconds(1f);
        transform.SetParent(originalParentTranform);
        transform.localPosition = originalLocalPosition;
        _inStack = false;
    }
}
