using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerBarBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject playerComp;

    private Transform followCameraTransform;

    void OnEnable()
    {
        if (playerComp != null)
        {
            followCameraTransform = CameraManager.Instance.GetFollowCameraTransform();
        }
    }

    void OnDisable()
    {
        followCameraTransform = null;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (followCameraTransform != null)
        {
            transform.LookAt(transform.position + followCameraTransform.rotation * Vector3.forward, followCameraTransform.rotation * Vector3.up);
        }    
    }
}
