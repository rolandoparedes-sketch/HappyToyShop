using UnityEngine;

public class ShelfStorage : MonoBehaviour, IInteractuable
{
    public int ShelfID;
    public ToyData data;
    public int CurrentAmount;
    public int MaxCapacity = 18;

    

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void Interact()
    {

        if (PlayerController2D.instance.playerMechanics.ToyData != null)
            GiveToy();
        else
            ReturnToy();
    }
    public void GiveToy()
    {
        if (CurrentAmount <= 0)
            return;

        PlayerController2D.instance.playerMechanics.AddToy(data);

        CurrentAmount--;

        GameManager2D.instance.DataGame.currentAmountInShelfs[ShelfID]--;
    }
    public void ReturnToy()
    {
        if (CurrentAmount >= MaxCapacity)
            return;

        PlayerController2D.instance.playerMechanics.RemoveToy();
        CurrentAmount++;

        GameManager2D.instance.DataGame.currentAmountInShelfs[ShelfID]++;
    }
}
