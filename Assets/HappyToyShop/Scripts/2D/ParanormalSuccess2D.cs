using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class ParanormalSuccess2D : MonoBehaviour
{
    public float TimeDelaySounDoors = 4;
    public float durationSoundDoors = 6;
    
    void Start()
    {

    }
    [Button]
    public void KnockKnock()
    {
        FreezeALL();
        GameManager2D.instance.SoundManager.StopMusicBackground(TimeDelaySounDoors);


        StartCoroutine(PlaySoundDoors());
    }
    public IEnumerator PlaySoundDoors()
    {
        yield return new WaitForSeconds(TimeDelaySounDoors);


        GameManager2D.instance.SoundManager.PlayMusic(SoundType.SFX, 2);
        yield return new WaitForSeconds(durationSoundDoors);


        GameManager2D.instance.SoundManager.PlayMusicBackground();
    }

    public void FreezeALL()
    {
        StartCoroutine(TimeToFreeze());
    }
    public IEnumerator TimeToFreeze()
    { 
        GameManager2D.instance.CameraSystem.enabled = false;

        yield return new WaitForSeconds(5 );

        GameManager2D.instance.CameraSystem.enabled = true;
    }
}
