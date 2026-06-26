using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataGame", menuName = "Happy Toy Shop/DataGame")]
public class DataGame : ScriptableObject
{
    public float money;

    public int day;

    public int weekDayIndex;

    public int monthIndex;
    public int MaxCapacityShelf;


    public List<ShelfData> CurrentShelfs;

    public List<int> CurrentShelfsID;

    public List<int> CurrentShelfsAmount;




    public List<int> currentAmountInShelfs;


    public List<OrderData> pedidos;
}
[System.Serializable]
public struct OrderData
{
    public int toyID;
    public int amount;
}
[System.Serializable]
public struct ShelfData
{
    public int shelfID;
    public int currentAmount;
}