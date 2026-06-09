using Sirenix.Utilities;
using System.Collections.Generic;
using UnityEngine;

public class Player3DInventory : MonoBehaviour
{
    [SerializeField] public ItemBase itemInHandLeft;
    [SerializeField] public ItemBase itemInHandRight;
    [SerializeField] private List<ItemBase> inventoryCapacity = new ();

    [SerializeField] private int sizeBase = 10;

    void Start()
    {
        inventoryCapacity.SetLength(sizeBase);
    }


    void Update()
    {
        
    }
}
