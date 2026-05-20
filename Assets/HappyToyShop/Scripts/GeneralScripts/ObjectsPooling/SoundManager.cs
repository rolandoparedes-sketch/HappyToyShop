using Sirenix.OdinInspector;

using System;
using HappyToyShop.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public SoundPlayer SoundPlayerPrefab;

    public MyQueue<SoundPlayer> Pool = new();

    public int size = 20;

    public static Action<SoundPlayer> OnFinishAudio;

    void Start()
    {
        CreateSoundPlayerObjs(size);
    }
    private void OnEnable()
    {
        OnFinishAudio += EnqueueAudio;
    }
    private void OnDisable()
    {
        OnFinishAudio -= EnqueueAudio;
    }
 

    public void PlayAudio(string audioName)
    {
        if (Pool.Head == null || Pool.Count == 0)
        {
            Debug.Log("Se agrando la lista");
            CreateSoundPlayerObjs(5);
            // PlayAudio(audioName);
            return;
        }
        MusicData data = GameManager.instance.MusicDatabase.GetAudio(audioName);

        SoundPlayer soundPlayer = Pool.Dequeue();
        soundPlayer.gameObject.SetActive(true);
        soundPlayer.PlayAudio(data.Clip, data.Volume);
    }
    private void EnqueueAudio(SoundPlayer soundPlayer)
    {
        soundPlayer.gameObject.SetActive(false);
        Pool.Enqueue(soundPlayer);
    }
    [Button]
    private void CreateSoundPlayerObjs(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            SoundPlayer obj = Instantiate(SoundPlayerPrefab, transform);
            obj.gameObject.SetActive(false);
            Pool.Enqueue(obj);
        }
    }
    [Button]
    public void TestAudio(string audioName)
    {
        PlayAudio(audioName);
        Debug.Log(Pool.Count);
    }
   
}