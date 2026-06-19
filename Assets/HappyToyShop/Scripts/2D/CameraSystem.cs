using HappyToyShop.Collections.Graphs;
using Sirenix.OdinInspector;
using System.Collections;
using Unity.Cinemachine;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraSystem : MonoBehaviour
{
    public CinemachineCamera camera1;
    public CinemachineCamera camera2;


    public float timeFade;
    void Start()
    {
        PlayerController2D.instance.playerMovement.OnEnterDoor += ChangeCameras;

    }
    [Button]
    public void ChangeCameras()
    {
        ScenesManager.instance.FadeAnim.SetTrigger("TransitionEffectCamera");

        PlayerController2D.instance.playerMovement.MethodWaitForPlay(timeFade);


        StartCoroutine(DelayChangeCameras());
   

    }
    public IEnumerator DelayChangeCameras()
    {
        yield return new WaitForSeconds(1);

        Vector3 originalPos = PlayerController2D.instance.transform.position;

        if (camera1.Priority > camera2.Priority)
        {

            Vector3 newpos = new Vector3(originalPos.x - 9.5f, originalPos.y, originalPos.z);


            PlayerController2D.instance.transform.position = newpos;

        }
        else
        {

            Vector3 newpos = new Vector3(originalPos.x + 9.5f, originalPos.y, originalPos.z);

            PlayerController2D.instance.transform.position = newpos;
        }


        if (camera1.Priority > camera2.Priority)
        {
            camera1.Priority = 0;
            camera2.Priority = 1;
        }
        else
        {

            camera1.Priority = 1;
            camera2.Priority = 0;
        }
    }

    void Update()
    {
        
    }
}
