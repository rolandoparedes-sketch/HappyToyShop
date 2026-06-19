using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataGame", menuName = "Happy Toy Shop/DataGame")]
public class DataGame : ScriptableObject
{
    public float money;

    public int day;

    public List<int> currentAmountInShelfs;

}
