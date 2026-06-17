using HappyToyShop.Collections;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using System;
using System.Drawing;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public NPCCustomer customerPrefab;
    public MyQueue<NPCCustomer> CustomerPool = new();


    public static Action<NPCCustomer> OnCustomerLeft;


    public int size = 5;

    void Start()
    {
        CreatPoolCustomers(size);

    }
    private void OnEnable()
    {
        CustomerManager.OnCustomerLeft += AddCustomer;

    }

    public void AddCustomer(NPCCustomer client)
    {
        client.gameObject.SetActive(false);
        CustomerPool.Enqueue(client);
       
    }

    public NPCCustomer NextCustomer()
    {
        if(CustomerPool.Count == 0)
            return null;


        return CustomerPool.Dequeue();
    }

    [Button]
    public void CreatPoolCustomers(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            NPCCustomer npc = Instantiate(customerPrefab, transform);

            npc.gameObject.SetActive(false);
            CustomerPool.Enqueue(npc);

        }
    }
}
