using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;



public class AttentionTable : MonoBehaviour, IInteractuable
{

        
    public void Interact()
    {

        var customerQueue = GameManager2D.instance.CustomerManager.CustomerQueue;



        if (customerQueue.CustomerWaiting.Count == 0)
        {
            Debug.Log("No hay clientes en cola");
            return;
        }

        NPCCustomer nextCustomer = customerQueue.CustomerWaiting.Peek();

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

        GameManager2D.instance.DataGame.money += player.ToyData.SalePrice;




        player.Gift.gameObject.SetActive(false);

        customerQueue.RemoveWaitingCustomer(nextCustomer);

        Debug.Log("Atendiendo cliente: " + nextCustomer.DataNpc.EntityName);
        Debug.Log("Juguete vendido: " +player.ToyData.EntityName + ", precio: " + player.ToyData.SalePrice);

        player.RemoveToy();

        GameManager2D.instance.CustomerManager.OnCustomerAttended?.Invoke(nextCustomer);

        ShelfStorage.OnTakeToy?.Invoke();


        GameManager2D.instance.DataGame.currentAmountInShelfs[nextCustomer.IdPedido]--;

        nextCustomer.CustomerAttended(CustomerExitReason.Served);


        GameManager2D.instance.SoundManager.CheckTypeAudio(SoundType.SFX, 0);

    }
}