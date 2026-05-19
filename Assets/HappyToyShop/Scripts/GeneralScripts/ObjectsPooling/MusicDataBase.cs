using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "MusicDatabase", menuName = "Happy Toy Shop/MusicDatabase")]
public class MusicDatabase : SerializedScriptableObject
{
    public Dictionary<string, AudioClip> ClipDatabase = new();

    public AudioClip GetAudio(string audioName)
    {
        if (ClipDatabase.TryGetValue(audioName, out AudioClip clip))
        {
            return clip;
        }
        else
        {
            throw new System.Exception("El audio al que intentas acceder no existe o esta mal escrito");
        }
    }
}