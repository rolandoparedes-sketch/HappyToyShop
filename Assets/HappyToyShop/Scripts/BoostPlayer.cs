using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "BoostPlayer", menuName = "Happy Toy Shop/BoostPlayer")]
public class BoostPlayer : BaseEntityData
{
    #region Properties/Privates
    [FoldoutGroup("Levels")]
    [SerializeField]private int currentLevel;
    [FoldoutGroup("Levels")]
    [SerializeField] private int maxLevel;
    [FoldoutGroup("Value")]
    [SerializeField] private float cost;
    [FoldoutGroup("Value")]
    [SerializeField] private float costPerLevel;
    [FoldoutGroup("Value")]
    [SerializeField] private Quality type;

    #endregion

    #region Getters
    public int CurrentLevel => currentLevel;
    public int MaxLevel => maxLevel;
    public float Cost => cost;
    public float CostPerLevel => costPerLevel;
    public Quality Type => type;
    #endregion

}
