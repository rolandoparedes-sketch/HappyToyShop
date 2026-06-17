using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Splines.ExtrusionShapes;
using UnityEngine.UI;

public class NPCCustomer : MonoBehaviour
{
    [SerializeField] private NpcData data;
    [SerializeField] private TypeDialogue typeDialogue;
    //[SerializeField] private Sprite spriteNpc;

    [SerializeField] private GameObject NpcModel;

    //[SerializeField] private Sprite spriteToy;
    [SerializeField] private GameObject GloboPedido;
    [SerializeField] private int IDPedido;

    [SerializeField] private Animator animator;
    [SerializeField] private float patience = 30;

    public event Action OnCustomerLeft;

    /* public NPC(NpcDataDialogues dialogues, TypeDialogue typeDialogue, Sprite sprite, Animator animator, float patience)
     {
         Dialogues = dialogues;
         this.typeDialogue = typeDialogue;
         this.sprite = sprite;
         this.animator = animator;
         Patience = patience;

     }*/
    void Start()
    {
        Initializer();
    }

    void Update()
    {

    }
    [Button]
    public void Initializer()
    {
        int n = UnityEngine.Random.Range(0, 3);

        var factory = GameManager2D.instance.FactorySystem.toyDataBase;

        NpcModel.GetComponent<SpriteRenderer>().sprite = data.GetSpriteNpc(n);


        int n2 = UnityEngine.Random.Range(0, factory.toyDataBase.Count);

        ToyData toydata = factory.GetToy(n2);

        GloboPedido.GetComponent<SpriteRenderer>().sprite = toydata.Icon;
        IDPedido = toydata.ID;

        var DayManager = GameManager2D.instance.DayManager;

        int value = UnityEngine.Random.Range(0, 10);


        if (DayManager.LookDay() > 5 && value > 9)
        {

            typeDialogue = TypeDialogue.Disturbing;




            Debug.Log(data.GetDialogue(typeDialogue));

            return;
        }
        if (DayManager.LookDay() > 3 && value > 5)
        {

            typeDialogue = TypeDialogue.Strange;




            Debug.Log(data.GetDialogue(typeDialogue));

            return;
        }



        typeDialogue = TypeDialogue.Normal;



        Debug.Log(data.GetDialogue(typeDialogue));

    }
    [Button]
    public void LeaveStore()
    {
        CustomerManager.OnCustomerLeft?.Invoke(this);
    }
}
