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

    public Transform Door1;
    public Transform Door2;

    public float timeFade;
    public Transform Target;
    void Start()
    {
        PlayerController2D.instance.playerMovement.OnEnterDoor += ChangeCameras;

        Target = camera2.Target.TrackingTarget;

    }
    [Button]
    public void ChangeCameras()
    {
        ScenesManager.instance.FadeAnim.SetTrigger("TransitionEffectCamera");

        PlayerController2D.instance.playerMovement.MethodWaitForPlay(timeFade);


        camera2.Target.TrackingTarget = null;
        StartCoroutine(DelayChangeCameras());
   

    }
    public IEnumerator DelayChangeCameras()
    {
        yield return new WaitForSeconds(1);

        Vector3 originalPos = PlayerController2D.instance.transform.position;

        if (camera1.Priority > camera2.Priority)
        {

            Vector3 newpos = new Vector3(Door2.position.x - 1f, originalPos.y, originalPos.z);


            PlayerController2D.instance.transform.position = newpos;

        }
        else
        {

            Vector3 newpos = new Vector3(Door1.position.x + 2f, originalPos.y, originalPos.z);

            PlayerController2D.instance.transform.position = newpos;
        }


        if (camera1.Priority > camera2.Priority)
        {
            camera1.Priority = 0;
            camera2.Priority = 1;

            camera2.Target.TrackingTarget = Target;
            camera2.gameObject.SetActive(true);
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
