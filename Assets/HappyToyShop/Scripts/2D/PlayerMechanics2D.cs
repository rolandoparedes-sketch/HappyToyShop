using Sirenix.OdinInspector;
using System;
using UnityEngine;

public class PlayerMechanics2D : MonoBehaviour
{
    [FoldoutGroup("Inventory")]
    public ToyData ToyData;


    [FoldoutGroup("Interact")]
    public Transform InteractController;
    public Vector2 DimensionBox;


    void Start()
    {
        PlayerController2D.instance.playerMovement.OnInteract += Interact;
    }
    public void AddToy(ToyData toy)
    {
        ToyData = toy;
    }
    public void RemoveToy()
    {
        ToyData = null;
    }
    private void Interact()
    {
        Debug.Log("Interacturar");
        Collider2D objectTouched = Physics2D.OverlapBox(InteractController.position, DimensionBox, 0f);

        if( objectTouched.TryGetComponent<IInteractuable>(out var interactuable))
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
