using Sirenix.OdinInspector;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class Cinematic : MonoBehaviour
{
    public GameObject enemy;

    public GameObject targetCameraPlayer;
    public GameObject targetCameraEnemy;

    public CinemachineCamera targetCamera;

    public CinemachineCamera targetCamera2;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    [Button]
    public void StartCinematic()
    {
        targetCamera.Priority = 2;
        targetCameraPlayer.GetComponent<Animator>().enabled = true;
        targetCameraEnemy.GetComponent<Animator>().enabled = false;
        enemy.GetComponent<Animator>().enabled = false;

        StartCoroutine(ChangeCamera());
    }

    public IEnumerator ChangeCamera()
    {



        yield return new WaitForSeconds(3f);

        targetCamera.Priority = -1;

        targetCamera2.Priority = 2;

        targetCameraEnemy.GetComponent<Animator>().enabled = true;


        yield return new WaitForSeconds(3f);

        enemy.GetComponent<Animator>().enabled = true;
    }

}
