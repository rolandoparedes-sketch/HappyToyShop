using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class ParanormalSuccess2D : MonoBehaviour
{
    public static bool paranormalSuccessActive;

    public float TimeDelaySounDoors = 4;
    public float durationSoundDoors = 6;
    public float TimeToActiveSound;
    public int probability;
    public float timer;
    void Start()
    {

    }
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > TimeToActiveSound)
        { 
            timer = 0;
            int n = Random.Range(0, 101);
            Debug.Log(n);
            if (n < probability)
            {
                KnockKnock();
            }
        }
    }
    [Button]
    public void KnockKnock()
    {
        GameManager2D.instance.SoundManager.StopMusicBackground(TimeDelaySounDoors);


        StartCoroutine(PlayStrangeSound());
    }
    public IEnumerator PlayStrangeSound()
    {
        StartCoroutine(TimeToFreeze());
        yield return new WaitForSeconds(TimeDelaySounDoors);

        int n = Random.Range(0, 2);

        switch (n)
        {

                case 0:
                GameManager2D.instance.SoundManager.CheckTypeAudio(SoundType.Ambient, 0);
                break;
            case 1:
                GameManager2D.instance.SoundManager.CheckTypeAudio(SoundType.Ambient, 1);
                break;
        }


        yield return new WaitForSeconds(durationSoundDoors);


        GameManager2D.instance.SoundManager.PlayMusicBackground();
    }

    public void FreezeALL()
    {
       // StartCoroutine(TimeToFreeze());
    }
    public IEnumerator TimeToFreeze()
    {

        paranormalSuccessActive = true;
        GameManager2D.instance.UIManager.ChangeDialoguePlayer(".......");

        yield return new WaitForSeconds(durationSoundDoors);


        GameManager2D.instance.UIManager.ChangeDialoguePlayer("New days, new smiles");

        paranormalSuccessActive = false;
    }
}
