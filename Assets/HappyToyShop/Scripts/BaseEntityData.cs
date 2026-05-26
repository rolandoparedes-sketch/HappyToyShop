using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseEntityData", menuName = "Happy Toy Shop/BaseEntityData")]
public class BaseEntityData : ScriptableObject
{
    #region Properties/Privates
    [FoldoutGroup("Settings")]
    [SerializeField]private int iD;
    [FoldoutGroup("Settings")]
    [SerializeField] private string entityName;
    [FoldoutGroup("Settings/References"), PreviewField(150)]
    [SerializeField] private Sprite icon;
    [FoldoutGroup("Settings"), TextArea(3, 10)]
    [SerializeField] private string description;
    #endregion
    #region Getters
    public int ID => iD;
    public string EntityName => entityName;
    public Sprite Icon => icon;
    public string Description => description;

    #endregion

}
