using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class AttentionTable : MonoBehaviour, IInteractuable
{
    [SerializeField] private Queue<NPCCustomer> customerWaiting = new();
    [SerializeField] private List<NPCCustomer> customersInQueue = new();

    void Start()
    {
        GameManager2D.instance.CustomerManager.OnCustomerAttended += RemoveCustomerInQueue;

        GameManager2D.instance.CustomerManager.OnCustomerAttended += RemoveWaitingCustomer;
    }

    [Button]
    public void AddWaitingCustomer(NPCCustomer npc)
    {
        customerWaiting.Enqueue(npc);
    }

    [Button]
    private void RemoveWaitingCustomer(NPCCustomer customer)
    {
        if (customerWaiting.Count == 0) return;

        customerWaiting.Dequeue();
    }

    [Button]
    public void AddCustomerInQueue(NPCCustomer npc)
    {
        customersInQueue.Add(npc);
    }

    [Button]
    private void RemoveCustomerInQueue(NPCCustomer customer)
    {
        customersInQueue.Remove(customer);
    }

        
    public void Interact()
    {
        if (customerWaiting.Count == 0)
        {
            Debug.Log("No hay clientes en cola");
            return;
        }

        NPCCustomer nextCustomer = customerWaiting.Peek();

        var player = PlayerController2D.instance.playerMechanics;

        if (!player.ToyData)
        {
            Debug.Log("No tienes nada en mano para entregar");
            return ;
        }
        
        if (player.ToyData.ID != nextCustomer.IdPedido)
        {
            Debug.Log("El cliente quiere otro juguete");
            return;
        }
        if (!player.HasGift)
        {
            Debug.Log("Debes empaquetar el juguete antes de entregarlo");
            return;
        }


        player.HasGift = false;

        GameManager2D.instance.MoneySystem.CurrentMoney += player.ToyData.SalePrice;

        player.Gift.gameObject.SetActive(false);

        RemoveWaitingCustomer(nextCustomer);
        RemoveCustomerInQueue(nextCustomer);

        Debug.Log("Atendiendo cliente: " + nextCustomer.DataNpc.EntityName);
        Debug.Log("Juguete vendido: " +player.ToyData.EntityName + ", precio: " + player.ToyData.SalePrice);

        player.RemoveToy();

        GameManager2D.instance.CustomerManager.OnCustomerAttended?.Invoke(nextCustomer);

        nextCustomer.CustomerAttended();
    }
}