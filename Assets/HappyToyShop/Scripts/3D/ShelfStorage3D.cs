using System.Collections.Generic;
using UnityEngine;

public class ShelfStorage3D : MonoBehaviour, IInteractuable
{
    [Header("Shelf")]
    public ToyData toyData;
    public int maxCapacity = 3;

    private readonly List<Toy> toys = new();

    public bool IsFull => toys.Count >= maxCapacity;

    [Header("Effects")]
    [SerializeField] private ParticleSystem completeParticles;
    public void Interact()
    {
        Player3DInventory inventory = Player3DController.instance.inventory3D;

        if (inventory.itemInHandLeft == null)
            return;

        Toy toy = inventory.itemInHandLeft;

        if (toy.toyData != toyData)
        {
            Debug.Log($"Toy {toy.toyData.name} does not match shelf toy {toyData.name}");
            return;
        }

        if (IsFull)
        {
            Debug.Log($"Shelf is full. Cannot add toy {toy.toyData.name}");
            return;
        }
            

        AddToy(toy);
    }

    private void AddToy(Toy toy)
    {
        toys.Add(toy);

        Player3DController.instance.inventory3D.itemInHandLeft = null;

        RestockManager.instance.AddToy(toy.toyData);

        Destroy(toy.gameObject);

        if (IsFull && completeParticles != null)
        {
            completeParticles.Play();
        }
    }
}