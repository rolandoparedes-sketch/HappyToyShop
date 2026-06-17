using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class GoodEndingTest : MonoBehaviour
{
    public Transform endingStartPoint;
    public Transform player;

    public bool goodEnding;
    public CinemachineCamera playerCamera;
    public CinemachineCamera endingCamera;

    public Animator fadeAnimator;
    public Animator playerAnimator;

    bool played = false;

    void Update()
    {
        if (goodEnding && !played)
        {
            played = true;
            StartCoroutine(PlayEnding());
        }
    }

    IEnumerator PlayEnding()
    {
        fadeAnimator.Play("FadeIn 0");

        yield return new WaitForSeconds(1f);

        player.GetComponent<Player3DMovement>().enabled = false;
        player.GetComponent<CharacterController>().enabled = false;

        player.position = endingStartPoint.position;
        player.rotation = endingStartPoint.rotation;

        yield return new WaitForSeconds(0.1f);

        endingCamera.enabled = true;
        endingCamera.Priority = 100;

        playerAnimator.enabled = true;
        playerAnimator.Play("FinalBueno");

        fadeAnimator.SetTrigger("TransitionEffectCamera");
    }
    void Start()
    {
        playerAnimator.enabled = false;

    }
}
