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
    private bool movedAway = false;
    private bool finished = false;

    void Start()
    {
        startPos = introcamera.transform.position;
    }

    void Update()
    {
        if (finished) return;

        float distance = Vector3.Distance(introcamera.transform.position, startPos);

        if (!movedAway && distance > 10f)
        {
            movedAway = true;
           
        }

        if (movedAway && distance < 2f)
        {
           
            finished = true;
            StartCoroutine(FinishIntro());
        }
    }

    IEnumerator FinishIntro()
    {
        
        fadeAnimator.Play("FadeIn 0");

        yield return new WaitForSeconds(0.02f);


       
        introcamera.Priority = 10;
        playerCamera.Priority = 50;

     

        fadeAnimator.SetTrigger("TransitionEffectCamera");

        playerCamera.Priority = 100;
        introcamera.Priority = 0;
        

    }
}