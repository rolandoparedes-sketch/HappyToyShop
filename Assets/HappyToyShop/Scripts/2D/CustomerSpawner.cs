
using HappyToyShop.Collections;
using Sirenix.OdinInspector;
using System;
using System.Collections;
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







    [SerializeField] private NPCCustomer customerPrefab;
    [SerializeField] private MyQueue<NPCCustomer> customerPool = new();

    [SerializeField] private int size = 5;



    //public Action OnCutomerEnter;
    public static Action<NPCCustomer> OnCustomerLeft;

    void Start()
    {

        CreatPoolCustomers(size);
        StartCoroutine(SpawnRoutine());
    }

    [Button]

    public NPCCustomer NextCustomer()
    {
        if (customerPool.Count == 0)
            return null;

        return customerPool.Dequeue();
    }

    [Button]
    public void CreatPoolCustomers(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            NPCCustomer npc = Instantiate(customerPrefab, transform);

            npc.gameObject.SetActive(false);
            customerPool.Enqueue(npc);

        }
    }
    [Button]
    private void ReturnNPCToPool(NPCCustomer customer)
    {


        customerPool.Enqueue(customer);
        customer.gameObject.SetActive(false);



    }

    void Update()
    {

    }
    private void OnEnable()
    {
        OnCustomerLeft += ReturnNPCToPool;
        OnCustomerLeft += RemoveCustomerInStore;
    }

    private void OnDisable()
    {
        OnCustomerLeft -= ReturnNPCToPool;
        OnCustomerLeft -= RemoveCustomerInStore;
    }

    private void RemoveCustomerInStore(NPCCustomer customer)
    {
        currentCustomers--;
    }
    public void ApplySpawnTime()
    {
        currentTimeToSpawnCustomers = UnityEngine.Random.Range(timeMinToSpawnCustomers, timeMaxToSpawnCustomers + 1);
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

        if (ParanormalSuccess2D.paranormalSuccessActive)
            return;

        GameManager2D.instance.SoundManager.CheckTypeAudio(SoundType.SFX, 1);
        NPCCustomer customer = GameManager2D.instance.CustomerManager.CustomerSpawner.NextCustomer();

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

    #region Getters

    public int CustomerSpawnedToday => customerSpawnedToday;
    public int CustomerPerDay => customerPerDay;
    public int CurrentCustomers => currentCustomers;



    public NPCCustomer CustomerPrefab => customerPrefab;
    public MyQueue<NPCCustomer> CustomerPool => customerPool;
    public int Size => size;


    #endregion
}

