using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* manages cameras  */
public class CameraManager : MonoBehaviour
{
    [SerializeField] List<CinemachineVirtualCamera> _cameras;

    private InputManager _inputManager;

    public static CameraManager Instance;

    public CinemachineVirtualCamera CurrentCamera {get; private set;}

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            _inputManager = new InputManager();
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        if (_cameras.Count == 0)
        {
            Debug.LogError("CameraManager: no cameras defined!");
        }

        _cameras.ForEach(c => c.gameObject.SetActive(false));

        CurrentCamera = _cameras[0];
        CurrentCamera.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        _inputManager.Enable();
        _inputManager.Camera.Switch.performed += ToggleCameras;
    }

    private void OnDisable()
    {
        _inputManager.Camera.Switch.performed -= ToggleCameras;
        _inputManager.Disable();
    }

    private void ToggleCameras(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {        
        _cameras.ForEach(c => c.gameObject.SetActive(false));
        var currentCameraIndex = _cameras.FindIndex(c => c == CurrentCamera);
        var nextCameraIndex = (currentCameraIndex + 1) % _cameras.Count;
        CurrentCamera = _cameras[nextCameraIndex];
        CurrentCamera.gameObject.SetActive(true);
    }

    public (Vector3, Vector3) GetNormalizedCameraVectors()
    {
        var cameraForward = CurrentCamera.transform.forward;
        var cameraRight = CurrentCamera.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        return (cameraForward, cameraRight);
    }
}
