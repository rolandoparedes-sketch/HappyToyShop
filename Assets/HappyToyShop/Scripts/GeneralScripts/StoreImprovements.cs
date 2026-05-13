using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "StoreImprovements", menuName = "Happy Toy Shop/StoreImprovements")]
public class StoreImprovements : ScriptableObject
{

    #region Properties/Privates
    [FoldoutGroup("Levels")]
    [SerializeField] private int currentLevel;
    [FoldoutGroup("Levels")]
    [SerializeField] private int maxLevel;
    [FoldoutGroup("Value")]
    [SerializeField] private float cost;
    [FoldoutGroup("Value")]
    [SerializeField] private float cosPerLevel;
    [FoldoutGroup("Value")]
    [SerializeField] private Quality type;
    #endregion


    #region Getters
    public int CurrentLevel => currentLevel;
    public int MaxLevel => maxLevel;
    public float Cost => cost;
    public float CosPerLevel => cosPerLevel;
    public Quality Type => type;

    #endregion

}
