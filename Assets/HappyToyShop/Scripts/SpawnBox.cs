using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SpawnerCaja : MonoBehaviour
{
    [Header("Prefab Caja Grande")]
    public GameObject cajaPrefab;

    [Header("Punto de Spawn")]
    public Transform spawnPoint;

    [Header("Lista de Cajas")]
    public List<Box> boxes = new();

    [Button]
    public void SpawnCajas()
    {
        for (int i = 0; i < boxes.Count; i++)
        {
            GameObject nuevaCaja = Instantiate(
                cajaPrefab,
                spawnPoint.position + new Vector3(i * 2f, 0, 0),
                Quaternion.identity
            );

            BoxFisic cajaFisica =
                nuevaCaja.GetComponent<BoxFisic>();

            cajaFisica.toys = boxes[i].toys;
        }
    }
}
