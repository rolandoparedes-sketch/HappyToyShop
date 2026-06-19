using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

public class PackingTable : MonoBehaviour, IInteractuable
{
    [SerializeField] private float progress;
    [SerializeField] private float timeToPacking = 10f;
    [SerializeField] private bool isPacking;


    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public IEnumerator StartPacking()
    {
        isPacking = true;
        progress = 0f;

        while (progress < timeToPacking)
        {
            progress += Time.deltaTime;

            yield return null;
        }

        isPacking = false;


        var player = PlayerController2D.instance.playerMechanics;

        player.Gift.SetActive(true);

        Debug.Log("Packing completo");

        player.HasGift = true;



        
    }
    public void ReturnGift()
    {

    }

    public void Interact()
    {
        var player = PlayerController2D.instance.playerMechanics;
        if (player.ToyData == null)
        {
            Debug.Log("Primero debes tener un juguete en mano");
            return;
        }
        if (isPacking)
            return;

        StartCoroutine(StartPacking());


    }
}
