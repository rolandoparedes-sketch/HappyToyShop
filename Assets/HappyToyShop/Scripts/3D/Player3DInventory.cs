using Sirenix.Utilities;
using System.Collections.Generic;
using UnityEngine;

public class Player3DInventory : MonoBehaviour
{
    [Header("Hand Points")]
    public Transform leftHandPoint;
    public Transform rightHandPoint;


    [Header("Items")]

    [SerializeField] public Toy itemInHandLeft;
    [SerializeField] public ItemBase itemInHandRight;

    [SerializeField] private int sizeBase = 10;

    void Start()
    {
    }


    void Update()
    {
        
    }
}
