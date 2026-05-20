using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicData", menuName = "Happy Toy Shop/MusicDataBase/MusicData")]
[InlineEditor]
public class MusicData : ScriptableObject
{
    #region Privates/Properties
    [SerializeField]private AudioClip clip;

    [SerializeField,Range(0f, 1f)]private float volume = 1.0f;

    #endregion

    #region Getters
    public AudioClip Clip => clip;

    public float Volume => volume;

    #endregion
}
