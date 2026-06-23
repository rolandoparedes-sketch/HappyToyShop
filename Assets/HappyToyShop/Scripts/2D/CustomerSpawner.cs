
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private int customerPerDay = 15;

    [SerializeField] private int customerSpawnedToday;


    [SerializeField] private float timeToFirstSpawn = 5;
    [SerializeField] private float timeMinToSpawnCustomers = 10;
    [SerializeField] private float timeMaxToSpawnCustomers = 15;
    [SerializeField] private float currentTimeToSpawnCustomers;

    [SerializeField] private int currentCustomers;

    [SerializeField] private int maxCustomersinStore;



    void Start()
    {

        StartCoroutine(SpawnRoutine());
    }

    void Update()
    {

    }
    private void OnEnable()
    {
        CustomerManager.OnCustomerLeft += RemoveCustomerInStore;
    }

    private void OnDisable()
    {
        CustomerManager.OnCustomerLeft -= RemoveCustomerInStore;
    }

    private void RemoveCustomerInStore(NPCCustomer customer)
    {
        currentCustomers--;
    }
    public void ApplySpawnTime()
    {
        currentTimeToSpawnCustomers = Random.Range(timeMinToSpawnCustomers, timeMaxToSpawnCustomers + 1);
    }
    private IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(timeToFirstSpawn);
        SpawnCustomer();
        customerSpawnedToday++;

        while (customerSpawnedToday < customerPerDay)
        {
            ApplySpawnTime();

            yield return new WaitForSeconds(currentTimeToSpawnCustomers);

            yield return new WaitUntil(() => currentCustomers < maxCustomersinStore);

            SpawnCustomer();

            customerSpawnedToday++;
        }
    }

    [Button]
    public void SpawnCustomer()
    {

        GameManager2D.instance.SoundManager.CheckTypeAudio(SoundType.SFX, 1);
        NPCCustomer customer = GameManager2D.instance.CustomerManager.NextCustomer();

        if (customer == null)
        {
            Debug.Log("No hay clientes disponibles en el pool.");
            return;
        }

        customer.transform.position = transform.position;

        customer.gameObject.SetActive(true);
        customer.Initializer();

        GameManager2D.instance.CustomerManager.CustomerQueue.AddCustomerInQueue(customer);
      
        currentCustomers++;
    }

    public int CustomerSpawnedToday => customerSpawnedToday;
    public int CustomerPerDay => customerPerDay;
    public int CurrentCustomers => currentCustomers;

}

