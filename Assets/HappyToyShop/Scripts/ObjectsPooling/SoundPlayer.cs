using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{


    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {

    }
    public void PlayAudio(AudioClip clip, float volume)
    {
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();

        //audioSource.clip.length

        //->Corrutinas
        //->Invoke
        Invoke(nameof(ReturnToPool), audioSource.clip.length);
    }
    public void ReturnToPool()
    {
        //->comunicarme con mi music pool
        //-> regresar a casa :c 
        audioSource.clip = null;
        SoundManager.OnFinishAudio?.Invoke(this);
    }
}