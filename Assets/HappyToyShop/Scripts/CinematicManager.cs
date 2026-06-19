using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CinematicManager : MonoBehaviour
{

    public CinemachineCamera introcamera;
    public CinemachineCamera playerCamera;
    public Animator fadeAnimator;

    private Vector3 startPos;
    public float cinematicDuration = 5f;

    void Start()
    {
        introcamera.Priority = 100;
        playerCamera.Priority = 10;
        StartCoroutine(PlayCinematicRoutine());
    }
    IEnumerator PlayCinematicRoutine()
    {
        yield return new WaitForSeconds(cinematicDuration);
        if (fadeAnimator != null)
        {
            fadeAnimator.Play("FadeIn 0");
        }

        yield return new WaitForSeconds(0.1f);
        introcamera.Priority = 0;
        playerCamera.Priority = 100;

        enabled = false;
    }
}

   

  