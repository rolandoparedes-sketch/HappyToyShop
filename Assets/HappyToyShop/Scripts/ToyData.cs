using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ToyData", menuName = "Happy Toy Shop/ToyDataBase/ToyData")]
[InlineEditor]
public class ToyData : BaseEntityData
{
    #region Properties/Privates
    [FoldoutGroup("ToyObject")]
    [SerializeField] private GameObject toyPrefab;
    [FoldoutGroup("Value")]
    [SerializeField] private float salePrice;
    [FoldoutGroup("Value")]
    [SerializeField] private float dozenCost;
    [FoldoutGroup("Value")]
    [SerializeField] private float halfDozenCost;

    #endregion
    #region Getters
    public GameObject ToyPrefab => toyPrefab;
    public float SalePrice => salePrice;
    public float DozenCost => dozenCost;
    public float HalfDozenCost => halfDozenCost;
    #endregion
}
