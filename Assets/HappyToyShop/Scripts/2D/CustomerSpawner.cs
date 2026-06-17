
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private int customerPerDay = 15;
    [SerializeField] private float timeMinToSpawnCustomers = 10;
    [SerializeField] private float timeMaxToSpawnCustomers = 15;
    [SerializeField] private float currentTimeToSpawnCustomers;

    [SerializeField] private int currentCustomers;

    [SerializeField] private int maxCustomersinStore;




    void Start()
    {
        ApplySpawnTime();
    }

    void Update()
    {

    }

    public void ApplySpawnTime()
    {
        currentTimeToSpawnCustomers = Random.Range(timeMinToSpawnCustomers, timeMaxToSpawnCustomers + 1);
    }
    private IEnumerator SpawnRoutine()
    {
        ApplySpawnTime();

        while (true)
        {
            yield return new WaitForSeconds(currentTimeToSpawnCustomers);

            yield return new WaitUntil(() => currentCustomers < maxCustomersinStore);

            SpawnCustomer();

            ApplySpawnTime();
        }
    }

    [Button]
    public void SpawnCustomer()
    {
        NPCCustomer customer = GameManager2D.instance.CustomerManager.NextCustomer();

        if (customer == null)
        {
            Debug.Log("No hay clientes disponibles en el pool.");
            return;
        }

        customer.transform.position = transform.position;

        customer.gameObject.SetActive(true);
        customer.Initializer();

        currentCustomers++;
    }


    private void HandleCustomerLeft()
    {
        currentCustomers--;
    }
}

