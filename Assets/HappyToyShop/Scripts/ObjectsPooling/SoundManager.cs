using HappyToyShop.Collections;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.InputSystem;
using UnityEngine;
using static UnityEngine.Rendering.PostProcessing.SubpixelMorphologicalAntialiasing;

public class SoundManager : MonoBehaviour
{
    public SoundPlayer SoundPlayerPrefab;
    public List<SoundPlayer> SoundPlayerList;
    
    public MyQueue<SoundPlayer> MusicPool = new();
    public MyQueue<SoundPlayer> SFXPool = new();
    public MyQueue<SoundPlayer> UIPool = new();
    public MyQueue<SoundPlayer> AmbientPool = new();
    public MyQueue<SoundPlayer> VoicePool = new();

    public int size = 20;

    public static Action<SoundPlayer> OnFinishAudio;

    void Start()
    {
        ExpandSoundPlayerMusic(size);
        ExpandSoundPlayerSFX(size);
        ExpandSoundPlayerUI(size);
        ExpandSoundPlayerAmbient(size);
        ExpandSoundPlayerVoice(size);
    }
    private void OnEnable()
    {
        OnFinishAudio += EnqueueAudio;
    }
    private void OnDisable()
    {
        OnFinishAudio -= EnqueueAudio;
    }

    public void CheckTypeAudio(SoundType type,int id)
    {
        switch (type)
        {
            case SoundType.None:
                break;
        
            case SoundType.Music:
                PlayMusic(type, id);
                break;

            case SoundType.UI:
                PlayUI(type, id);
                break;

            case SoundType.Ambient:
                PlayAmbient(type, id);
                break;
        
            case SoundType.Voice:
                PlayVoice(type, id);
                break;
           
            case SoundType.SFX:
                PlaySFX(type, id);
                break;

            default: 
                break;

        }
    }

    public void PlayMusic(SoundType type, int id)
    {
        if (MusicPool.Head == null || MusicPool.Count == 0)
        {
            Debug.Log("Se agrando la lista");
            ExpandSoundPlayerMusic(5);
            // PlayAudio(audioName);
            return;
        }
        AudioData data = GameManager.instance.musicDatabase.GetAudio(type, id);

        SoundPlayer soundPlayer = SoundPlayerList[0];
        soundPlayer = MusicPool.Dequeue();
        soundPlayer.gameObject.SetActive(true);
        soundPlayer.PlayAudio(data.Clip, data.Volume);
    }

    public void PlaySFX(SoundType type, int id)
    {
        if (SFXPool.Head == null || SFXPool.Count == 0)
        {
            Debug.Log("Se agrando la lista");
            ExpandSoundPlayerSFX(5);
            // PlayAudio(audioName);
            return;
        }
        AudioData data = GameManager.instance.musicDatabase.GetAudio(type, id);
        SoundPlayer soundPlayer = SoundPlayerList[1];
        soundPlayer = SFXPool.Dequeue();
        soundPlayer.gameObject.SetActive(true);
        soundPlayer.PlayAudio(data.Clip, data.Volume);
    }
    public void PlayUI(SoundType type, int id)
    {
        if (UIPool.Head == null || UIPool.Count == 0)
        {
            Debug.Log("Se agrando la lista");
            ExpandSoundPlayerUI(5);
            // PlayAudio(audioName);
            return;
        }
        AudioData data = GameManager.instance.musicDatabase.GetAudio(type, id);

        SoundPlayer soundPlayer = SoundPlayerList[2];
        soundPlayer = UIPool.Dequeue();
        soundPlayer.gameObject.SetActive(true);
        soundPlayer.PlayAudio(data.Clip, data.Volume);
    }
    public void PlayAmbient(SoundType type, int id)
    {
        if (AmbientPool.Head == null || AmbientPool.Count == 0)
        {
            Debug.Log("Se agrando la lista");
            ExpandSoundPlayerAmbient(5);
            // PlayAudio(audioName);
            return;
        }
        AudioData data = GameManager.instance.musicDatabase.GetAudio(type, id);

        SoundPlayer soundPlayer = SoundPlayerList[3];
        soundPlayer = AmbientPool.Dequeue();
        soundPlayer.gameObject.SetActive(true);
        soundPlayer.PlayAudio(data.Clip, data.Volume);
    }
    public void PlayVoice(SoundType type, int id)
    {
        if (VoicePool.Head == null || VoicePool.Count == 0)
        {
            Debug.Log("Se agrando la lista");
            ExpandSoundPlayerVoice(5);
            // PlayAudio(audioName);
            return;
        }
        AudioData data = GameManager.instance.musicDatabase.GetAudio(type, id);

        SoundPlayer soundPlayer = SoundPlayerList[4];
        soundPlayer = VoicePool.Dequeue();
        soundPlayer.gameObject.SetActive(true);
        soundPlayer.PlayAudio(data.Clip, data.Volume);
    }
    private void EnqueueAudio(SoundPlayer soundPlayer)
    {
        soundPlayer.gameObject.SetActive(false);
        MusicPool.Enqueue(soundPlayer);
    }
    [Button]
    private void ExpandSoundPlayerMusic(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            SoundPlayer Music = Instantiate(SoundPlayerList[0], transform);
            Music.gameObject.SetActive(false);
            MusicPool.Enqueue(Music);

        }

    }
    private void ExpandSoundPlayerSFX(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            SoundPlayer SFX = Instantiate(SoundPlayerList[1], transform);
            SFX.gameObject.SetActive(false);
            SFXPool.Enqueue(SFX);

        }
    }
    private void ExpandSoundPlayerUI(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            SoundPlayer UI = Instantiate(SoundPlayerList[2], transform);
            UI.gameObject.SetActive(false);
            UIPool.Enqueue(UI);

        }
    }
    private void ExpandSoundPlayerAmbient(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            SoundPlayer Ambient = Instantiate(SoundPlayerList[3], transform);
            Ambient.gameObject.SetActive(false);
            AmbientPool.Enqueue(Ambient);

        }
    }
    private void ExpandSoundPlayerVoice(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {

            SoundPlayer Voices = Instantiate(SoundPlayerList[4], transform);
            Voices.gameObject.SetActive(false);
            VoicePool.Enqueue(Voices);

        }
    }

    [Button]
    public void TestAudio(SoundType type,int id)
    {
        CheckTypeAudio(type, id);
        Debug.Log(MusicPool.Count);
    }
    [Button]
    public void TestCount()
    {

        Debug.Log(MusicPool.Count);
    }


}