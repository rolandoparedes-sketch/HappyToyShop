using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager3D : MonoBehaviour
{

    public Button NextCamera;
    public Button PreviusCamera;


    void OnEnable()
    {
        Monitor.OnWatchingCameras += ActiveButtons;
    }

    private void ActiveButtons()
    {
        NextCamera.gameObject.SetActive(true);

        NextCamera.gameObject.SetActive(false);
    }

    void Start()
    {
        
    }

   

    void Update()
    {
        
    }
}
