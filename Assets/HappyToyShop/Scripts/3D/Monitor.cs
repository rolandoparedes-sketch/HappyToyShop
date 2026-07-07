using System;
using Unity.Cinemachine;
using UnityEngine;

public class Monitor : MonoBehaviour , IInteractuable
{
    public CinemachineCamera[] securityCameras;

    public CameraNode currentCamera;
    public CinemachineBrain cinemachineBrain;

    public bool usingMonitor = false;


    public static Action OnWatchingCameras;
    public static Action OnExitCameras;

    private void Start()
    {
        CreateCameraList();
        ExitMonitor();
    }
    public void Interact()
    {
        Debug.Log("Entró al monitor");
        EnterMonitor();
    }

    private void CreateCameraList()
    {
        CameraNode first = null;// +1
        CameraNode previous = null;//+1

        foreach (CinemachineCamera cam in securityCameras) // N
        {


            CameraNode node = new CameraNode(cam); //+1

            if (first == null) // +1
            {
                first = node;  //+1
            }

            if (previous != null) //+1
            {
                previous.next = node; //+1
                node.previous = previous; //+1
            }

            previous = node; //+1
        }
        previous.next = first; //+1
        first.previous = previous; //+1

        currentCamera = first; //+1

    }
    public void NextCamera()
    {

        currentCamera.camera.Priority = 10;

        currentCamera = currentCamera.next;

        currentCamera.camera.Priority = 50;

    }
    public void PreviousCamera()
    {
        currentCamera.camera.Priority = 10;

        currentCamera = currentCamera.previous;

        currentCamera.camera.Priority = 50;

    }

    private void EnterMonitor()
    {
        usingMonitor = true;

        var playerCamera = GameManager3D.instance.ShadowSpawner.Target.GetComponent<Player3DMovement>().characterCamera;

        playerCamera.Priority = 10;
        currentCamera.camera.Priority = 50;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        OnWatchingCameras?.Invoke();
    }

    public void ExitMonitor()
    {
        usingMonitor = false;

        var playerCamera = GameManager3D.instance.ShadowSpawner.Target.GetComponent<Player3DMovement>().characterCamera;

        currentCamera.camera.Priority = 10;
        playerCamera.Priority = 50;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        OnExitCameras?.Invoke();
    }

}
