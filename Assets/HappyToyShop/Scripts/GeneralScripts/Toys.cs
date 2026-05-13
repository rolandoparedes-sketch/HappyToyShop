using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Toys", menuName = "Happy Toy Shop/Toys")]
public class Toys : BaseEntityData
{
    #region Properties/Privates
    [FoldoutGroup("References")]
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
