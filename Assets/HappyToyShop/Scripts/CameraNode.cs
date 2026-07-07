using Unity.Cinemachine;
using UnityEngine;

public class CameraNode
{
    public CinemachineCamera camera;
    public CameraNode next;
    public CameraNode previous;

    public CameraNode(CinemachineCamera cam)
    {
        camera = cam;
    }
}