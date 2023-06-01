using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform FollowTransform;
    private Vector3 _offset;
    public float LerpSpeed;
    private Vector3 _lerpFollow;

    private void Start()
    {
        _offset = transform.position - FollowTransform.position;
    }
    private void Update()
    {

        _lerpFollow = FollowTransform.position + _offset;
        transform.position = Vector3.Lerp(transform.position, _lerpFollow, LerpSpeed * Time.deltaTime);
    }
}
