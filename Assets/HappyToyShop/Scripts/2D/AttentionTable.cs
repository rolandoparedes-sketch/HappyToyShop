using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class AttentionTable : MonoBehaviour, IInteractuable
{


    public event Action OnSell;
    public void Interact()
    {

        var customerQueue = GameManager2D.instance.CustomerManager.CustomerQueue;



        if (customerQueue.CustomerWaiting.Count == 0)
        {
            
            GameManager2D.instance.UIManager.ChangeDialoguePlayer("There's nobody in the queue");
             Debug.Log("No hay clientes en cola");
            return;
        }

        NPCCustomer nextCustomer = customerQueue.CustomerWaiting.Peek();

        var player = PlayerController2D.instance.playerMechanics;

        if (!player.ToyData)
        {
            GameManager2D.instance.UIManager.ChangeDialoguePlayer("I have nothing to hand over");
            Debug.Log("No tienes nada en mano para entregar");
            return ;
        }


        if (player.ToyData.ID != nextCustomer.IdPedido)
        {
            GameManager2D.instance.UIManager.ChangeDialoguePlayer("This is not the toy the customer is looking for");
            Debug.Log("El cliente quiere otro juguete");
            return;
        }
        if (!player.HasGift)
        {
            GameManager2D.instance.UIManager.ChangeDialoguePlayer("I need to wrap it in a gift first");
            Debug.Log("Debes empaquetar el juguete antes de entregarlo");
            return;
        }


        player.HasGift = false;





        GameManager2D.instance.MoneySystem.CurrentMoney += player.ToyData.SalePrice;
        player.Gift.gameObject.SetActive(false);

        customerQueue.RemoveWaitingCustomer(nextCustomer);

        Debug.Log("Atendiendo cliente: " + nextCustomer.DataNpc.EntityName);
        Debug.Log("Juguete vendido: " +player.ToyData.EntityName + ", precio: " + player.ToyData.SalePrice);

        player.RemoveToy();


        ShelfStorage.OnTakeToy?.Invoke();


        nextCustomer.CustomerAttended(CustomerExitReason.Served);

        GameManager2D.instance.SoundManager.CheckTypeAudio(SoundType.SFX, 0);

        OnSell?.Invoke();

    }
}