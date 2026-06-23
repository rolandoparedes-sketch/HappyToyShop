using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CustomerQueue : MonoBehaviour
{
    [SerializeField] private List<Transform> queueTargets = new();

    [SerializeField] private Transform exitTarget;

    [SerializeField] private Queue<NPCCustomer> customerWaiting = new();
    [SerializeField] private List<NPCCustomer> customersInQueue = new();


    public Action<NPCCustomer> OnCustomerReceived;

    void Start()
    {


        

    }

    [Button]
    public void AddWaitingCustomer(NPCCustomer customer)
    {
        customerWaiting.Enqueue(customer);
        
        OnCustomerReceived?.Invoke(customer);

        customer.CustomerReceived();

    }

    [Button]
    public void RemoveWaitingCustomer(NPCCustomer customer)
    {
        if (customerWaiting.Count == 0)
            return;

        if (customerWaiting.Peek() == customer)
        {
            customerWaiting.Dequeue();
            customersInQueue.Remove(customer);

            UpdateQueuePositions();

            //OnCustomerReceived?.Invoke(customer);
        }
    }
    [Button]
    public void AddCustomerInQueue(NPCCustomer customer)
    {
        customersInQueue.Add(customer);
        UpdateQueuePositions();
    
    }
    public void ExtendPatienceToQueue(float amount)
    {
        foreach (var customer in customersInQueue)
        {
            customer.ExpandPatience(amount);
        }
    }

    [Button]
    private void UpdateQueuePositions()
    {
        int count = Mathf.Min(customersInQueue.Count, queueTargets.Count);

        for (int i = 0; i < count; i++)
        {
            customersInQueue[i].SetTarget(queueTargets[i]);
        }
    }
    public List<Transform> QueueTargets => queueTargets;
    public Transform ExitTarget => exitTarget;
    public Queue<NPCCustomer> CustomerWaiting => customerWaiting;
    public List<NPCCustomer> CustomersInQueue => customersInQueue;
}
