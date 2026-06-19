using Sirenix.OdinInspector;
using System;
using UnityEngine;

public class PlayerMechanics2D : MonoBehaviour
{
    [FoldoutGroup("Inventory")]
    public ToyData ToyData;


    [FoldoutGroup("Interact")]
    public ShelfStorage CurrentShelf;
    [SerializeField] private Transform InteractController;
    [SerializeField] private Vector2 DimensionBox;
    [SerializeField] private LayerMask InteractuableObjects;


    void Start()
    {
        PlayerController2D.instance.playerMovement.OnInteract += Interact;
    }
    public void AddToy(ToyData toy, ShelfStorage shelf)
    {
        ToyData = toy;
        CurrentShelf = shelf;
    }
    public void RemoveToy()
    {
        ToyData = null;
        CurrentShelf = null;
    }
    private void Interact()
    {
        Debug.Log("Interacturar");
        Collider2D objectTouched = Physics2D.OverlapBox(InteractController.position, DimensionBox, 0f, InteractuableObjects);

        if(objectTouched != null && objectTouched.TryGetComponent<IInteractuable>(out var interactuable))
        {
            Debug.Log(interactuable);

            interactuable.Interact();

        }

    }

    void Update()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(InteractController.position, DimensionBox);
    }


}
