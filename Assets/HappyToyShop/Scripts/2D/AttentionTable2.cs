using System;
using UnityEngine;

public class AttentionTable2 : MonoBehaviour, IInteractuable
{


    public event Action OnSell2;
    public void Interact()
    {

        var customerQueue2 = GameManager2D.instance.CustomerManager.CustomerQueue;



        if (customerQueue2.CustomerWaiting.Count == 0)
        {

            GameManager2D.instance.UIManager.ChangeDialoguePlayer("There's nobody in the queue");
            Debug.Log("No hay clientes en cola");
            return;
        }

        NPCCustomer nextCustomer = customerQueue2.CustomerWaiting.Peek();

        var player = PlayerController2D.instance.playerMechanics;

        if (!player.ToyData)
        {
            GameManager2D.instance.UIManager.ChangeDialoguePlayer("I have nothing to hand over");
            Debug.Log("No tienes nada en mano para entregar");
            return;
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

        customerQueue2.RemoveWaitingCustomer(nextCustomer);

        Debug.Log("Atendiendo cliente: " + nextCustomer.DataNpc.EntityName);
        Debug.Log("Juguete vendido: " + player.ToyData.EntityName + ", precio: " + player.ToyData.SalePrice);

        player.RemoveToy();


        ShelfStorage.OnTakeToy?.Invoke();


        nextCustomer.CustomerAttended(CustomerExitReason.Served);

        GameManager2D.instance.SoundManager.CheckTypeAudio(SoundType.SFX, 0);

        OnSell2?.Invoke();

    }
}
