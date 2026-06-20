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
        OnCustomerAttended += CustomerAttended;

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

    public void AddToQueue(NPCCustomer customer)
    {

        //waitingCustomers.Enqueue(customer);

        //UpdateQueue();
    }
    [Button]
    private void ReturnNPCToPool(NPCCustomer customer)
    {
        //if (waitingCustomers.Count == 0)
         //   return;

       // waitingCustomers.Dequeue();

       // customer.ResetCustomer();

        customer.gameObject.SetActive(false);

        customerPool.Enqueue(customer);
       // UpdateQueue();

    }
    private void CustomerAttended(NPCCustomer customer)
    {
      //  if (waitingCustomers.Count == 0)
            return;

       // waitingCustomers.Dequeue();
        //customer.ResetCustomer();
        //UpdateQueue();

       // OnChangeQueue?.Invoke();
    }
   /* private void UpdateQueue()
    {
        int index = 0;

        foreach (var customer in waitingCustomers)
        {
            if (index >= queuePositions.Length)
            {
                Debug.LogWarning("Hay mas clientes que posiciones dispoinbles, asigna más o disminuye la cantidad de clientes en la store");
            
                break;
            }
            customer.SetTarget(queuePositions[index]);

            Debug.Log("index: " + index);

            Debug.Log("QueuePostions.Length: " + queuePositions.Length);

            Debug.Log("WaitingCutomers.Count: " + waitingCustomers.Count);

            index++;
        }


    }*/
    #region Getters
    public NPCCustomer CustomerPrefab => customerPrefab;
    public MyQueue<NPCCustomer> CustomerPool => customerPool;
    public int Size => size;


    public Transform ExitPoint => exitPoint;
    #endregion
}
