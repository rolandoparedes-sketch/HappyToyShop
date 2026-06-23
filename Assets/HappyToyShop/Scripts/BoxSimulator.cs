using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class BoxSimulator : MonoBehaviour
{
    public int boxCount = 1;

    [Button]
    public void IncreaseBoxes()
    {
        boxCount++;
    }

    [Button]
    public void DecreaseBoxes()
    {
        if (boxCount > 0)
            boxCount--;
    }
}
