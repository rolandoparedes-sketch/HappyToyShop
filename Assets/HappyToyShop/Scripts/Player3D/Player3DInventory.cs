using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Player3DInventory : MonoBehaviour
{
    [SerializeField] private ItemBase itemInHandLeft;
    [SerializeField] private ItemBase itemInHandRight;
    [SerializeField] private List<ItemBase> inventoryCapacity = new ();

    [SerializeField] private int sizeBase = 10;
    private void Awake()
    {
        Player3DMovement.OnFlashOn += ActiveFlashlight;
    }

    private void ActiveFlashlight()
    {
        if(itemInHandRight.gameObject.activeSelf)
            itemInHandRight.gameObject.SetActive (false);
        else
            itemInHandRight.gameObject.SetActive (true);
    }

    void Start()
    {
        inventoryCapacity.SetLength(sizeBase);
    }


    void Update()
    {
        
    }
}
