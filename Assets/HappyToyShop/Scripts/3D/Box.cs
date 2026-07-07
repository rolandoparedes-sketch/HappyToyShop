using UnityEngine;

public class Box : MonoBehaviour, IInteractuable
{
    public ToyData toyData;

    public int amount;


    [Header("Spawn")]
    public float radius = 1.5f;
    public float spawnHeight = 0.5f;

    public void Interact()
    {
        Debug.Log("Interacting with box");

        if (toyData == null || toyData.ToyPrefab == null)
            return;

        for (int i = 0; i < amount; i++)
        {
            Vector2 random = Random.insideUnitCircle * radius;

            Vector3 position = transform.position + new Vector3(random.x, spawnHeight, random.y);

            Quaternion rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);

            GameObject toyGO = Instantiate(toyData.ToyPrefab, position, rotation);

            Toy toy = toyGO.GetComponent<Toy>();

            if (toy != null)
            {
                toy.toyData = toyData;
            }
        }

        Destroy(gameObject);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
