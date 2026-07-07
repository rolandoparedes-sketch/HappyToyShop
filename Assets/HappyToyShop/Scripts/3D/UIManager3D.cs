using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager3D : MonoBehaviour
{

    public GameObject UICamera;

    void OnEnable()
    {
        Monitor.OnWatchingCameras += ActiveButtons;
        Monitor.OnExitCameras += DeactiveButtons;
    }

    private void DeactiveButtons()
    {
        UICamera.SetActive(false);
    }

    private void ActiveButtons()
    {
        UICamera.SetActive(true);

    }

    void Start()
    {
        
    }

   

    void Update()
    {
        
    }
}
