using HappyToyShop.Collections;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor.PackageManager;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private CustomerQueue customerQueue;
    [SerializeField] private CustomerSpawner customerSpawner;


    [SerializeField] private NPCCustomer customerPrefab;
    [SerializeField] private MyQueue<NPCCustomer> customerPool = new();

    [SerializeField] private int size = 5;


    [SerializeField] private Transform[] queuePositions;

    [SerializeField] private Transform exitPoint;
   // private readonly Queue<NPCCustomer> waitingCustomers = new();


    public Action OnCutomerEnter;
    public static Action<NPCCustomer> OnCustomerLeft;
    public Action<NPCCustomer> OnCustomerAttended;

    public Action OnChangeQueue;



    void Start()
    {
        CreatPoolCustomers(size);

    }

    private void OnEnable()
    {
       OnCustomerLeft += ReturnNPCToPool;
    }

    private void OnDisable()
    {
       OnCustomerLeft -= ReturnNPCToPool;
    }

    [Button]

    public NPCCustomer NextCustomer()
    {
        if(customerPool.Count == 0)
            return null;

        return customerPool.Dequeue();
    }

    [Button]
    public void CreatPoolCustomers(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            NPCCustomer npc = Instantiate(CustomerPrefab, transform);

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
    #region Getters

    public CustomerSpawner CustomerSpawner => customerSpawner;
    public CustomerQueue CustomerQueue => customerQueue;


    public NPCCustomer CustomerPrefab => customerPrefab;
    public MyQueue<NPCCustomer> CustomerPool => customerPool;
    public int Size => size;


    public Transform ExitPoint => exitPoint;
    #endregion
}
