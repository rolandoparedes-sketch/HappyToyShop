using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager3D : MonoBehaviour
{

    public GameObject UICamera;

    [Header("Electroshock")]
    public RandomGuy enemy;

    private void OnEnable()
    {
        Monitor.OnWatchingCameras += ActiveButtons;
        Monitor.OnExitCameras += DeactiveButtons;
    }

    private void OnDisable()
    {
        Monitor.OnWatchingCameras -= ActiveButtons;
        Monitor.OnExitCameras -= DeactiveButtons;
    }

    private void DeactiveButtons()
    {
        UICamera.SetActive(false);
    }

    private void ActiveButtons()
    {
        UICamera.SetActive(true);
    }

    public void ActivateElectroshock()
    {
        if (enemy != null)
        {
            enemy.ActivateElectroshock();
        }
    }
}