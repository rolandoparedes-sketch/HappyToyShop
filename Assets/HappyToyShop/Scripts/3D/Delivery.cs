using UnityEngine;
using Sirenix.OdinInspector;

public class Delivery : MonoBehaviour
{
    public Box boxPrefab;
    public Transform spawnPoint;

    [Header("Grid")]
    public int boxesPerColumn = 5; 
    public float spacingX = 2f;
    public float spacingZ = 2f;

    private void Start()
    {
        SpawnBoxes();
    }
    [Button]
    public void SpawnBoxes()
    {
        var dataGame = GameManager3D.instance.DataGame;

        for (int i = 0; i < dataGame.CurrentShelfsID.Count; i++)
        {
            int row = i % boxesPerColumn;
            int column = i / boxesPerColumn;

            Vector3 position = spawnPoint.position +
                               new Vector3(column * spacingX, 0, row * spacingZ);

            GameObject boxGO = Instantiate(boxPrefab.gameObject, position, Quaternion.identity);

            Box box = boxGO.GetComponent<Box>();

            if (box != null)
            {
                int id = dataGame.CurrentShelfsID[i];
                int amount = dataGame.CurrentShelfsAmount[i];

                box.toyData = GameManager3D.instance.FactorySystem.TakeToy(id);
                box.amount = amount;
            }
        }
    }
}