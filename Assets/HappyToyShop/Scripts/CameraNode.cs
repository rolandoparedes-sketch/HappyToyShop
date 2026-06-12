using UnityEngine;

public class CameraNode 
{
    public Camera camera;
    public CameraNode next;
    public CameraNode previous;

    public CameraNode(Camera camera)
    {
        this.camera = camera;
    }
}