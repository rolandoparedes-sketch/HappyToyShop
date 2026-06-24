using Sirenix.OdinInspector;
using System;
using UnityEngine;

public class PlayerMechanics2D : MonoBehaviour
{
    [FoldoutGroup("Inventory")]
    public ToyData ToyData;
    [FoldoutGroup("Inventory")]
    public GameObject Gift;
    [FoldoutGroup("Inventory")]
    public bool HasGift;
    [FoldoutGroup("Interact")]
    public ShelfStorage CurrentShelf;
    [FoldoutGroup("Interact")]
    [SerializeField] private Transform InteractController;
    [FoldoutGroup("Interact")]
    [SerializeField] private Vector2 DimensionBox;
    [FoldoutGroup("Interact")]
    [SerializeField] private LayerMask InteractuableObjects;
    [FoldoutGroup("Interact")]
    [SerializeField] private IInteractuable currentInteractable;
    [FoldoutGroup("Interact")]
    [SerializeField] private  GameObject interactUI;

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
    private void CheckInteractable()
    {
        Collider2D objectTouched = Physics2D.OverlapBox(InteractController.position, DimensionBox, 0f, InteractuableObjects);

        if (objectTouched != null && objectTouched.TryGetComponent<IInteractuable>(out var interactable))
        {
            currentInteractable = interactable;
            interactUI.SetActive(true);
        }
        else
        {
            currentInteractable = null;
            interactUI.SetActive(false);
        }
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
    private void OnTriggerEnter2D(Collider2D other)
    {
       


    }
    void Update()
    {
        CheckInteractable();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(InteractController.position, DimensionBox);
    }


}
