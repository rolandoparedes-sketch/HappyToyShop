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

    public List<int> currentAmountInShelfs;

    public List<OrderData> pedidos;
}
[System.Serializable]
public struct OrderData
{
    public int toyID;
    public int amount;
}