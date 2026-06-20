using System;
using Unity.VisualScripting;
using UnityEngine;

public class ShelfStorage : MonoBehaviour, IInteractuable
{
    public int ShelfID;
    public ToyData data;
    public int CurrentAmount;
    public int MaxCapacity = 18;


    public static event Action OnTakeToy;

    void Start()
    {
        



    }

    void Update()
    {
        
    }
    public void Interact()
    {
        Debug.Log("INTERACTUO");

        var player = PlayerController2D.instance.playerMechanics;
        if (player.HasGift)
        {
            Debug.Log("Debes entregar el regalo primero");

            return;
        }

        if (player.ToyData == null)
        {

            GiveToy();
            return;
        }
        if (player.CurrentShelf == this)
        {
            ReturnToy();
            return;
        }

        player.CurrentShelf.ReturnToy();
        GiveToy();
    }
    public void GiveToy()
    {
        if (CurrentAmount <= 0)
            return;

        var player = PlayerController2D.instance.playerMechanics;

        player.AddToy(data, this);

        CurrentAmount--;


        Debug.Log("DIO JUGUETE: " + data.EntityName);


        OnTakeToy?.Invoke();
    }
    public void ReturnToy()
    {
        if (CurrentAmount >= MaxCapacity)
            return;

        var player = PlayerController2D.instance.playerMechanics;

        player.RemoveToy();
        CurrentAmount++;



        Debug.Log("DEVOLVIO JUGUETE: " + data.EntityName);
        OnTakeToy?.Invoke();
    }
}
