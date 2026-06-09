using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int ItemID;
    public int Amount;

    public Inventory(int itemID, int amount)
    {
        ItemID = itemID;
        Amount = amount;
    }
}
