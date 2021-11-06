using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] private GameObject followCamera;

    private Transform followCameraTransform;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (followCamera != null)
        {
            followCameraTransform = followCamera.GetComponent<CinemachineVirtualCamera>().transform;
        }
        
    }

    public Transform GetFollowCameraTransform()
    {
        return followCameraTransform;
    }
}
