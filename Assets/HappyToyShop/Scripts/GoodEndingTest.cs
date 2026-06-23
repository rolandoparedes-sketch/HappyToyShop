using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoodEndingTest : MonoBehaviour
{
    public Transform endingStartPoint;
    public Transform player;

    public bool goodEnding;

    public CinemachineCamera playerCamera;
    public CinemachineCamera endingSplineCamera;


    public Animator fadeAnimator;
    public Animator playerAnimator;

    public GameObject TextGoodEnding;

    public float TimeofCamera = 95f;
    public float timeofText = 3f;

    bool played = false;

    void Start()
    {
        playerAnimator.enabled = false;

        if (TextGoodEnding != null)
            TextGoodEnding.SetActive(false);
        
    }

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

        playerCamera.enabled = false;
        endingSplineCamera.Priority = 100;

        var spline = endingSplineCamera.GetComponent<CinemachineSplineDolly>();

        spline.CameraPosition = 0f;
        spline.AutomaticDolly.Enabled = true;


        playerAnimator.enabled = true;
        playerAnimator.Play("FinalBueno");

        while (spline.CameraPosition < 0.999f)
        {
            endingSplineCamera.Priority = 100;
            yield return null;
        }

        fadeAnimator.Play("FadeIn 0");

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("Win");

    }
}