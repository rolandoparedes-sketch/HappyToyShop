using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SpawnBox : MonoBehaviour
{
    public GameObject boxPrefab;
    public Transform spawnPoint;

    public BoxSimulator simulator;

    [Button]
    public void SpawnBoxes()
    {
        for (int i = 0; i < simulator.boxCount; i++)
        {
            Instantiate(
                boxPrefab,
                spawnPoint.position + new Vector3(i * 2f, 0, 0),
                Quaternion.identity
            );
        }
    }
}