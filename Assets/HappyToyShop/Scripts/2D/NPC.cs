using Sirenix.OdinInspector;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public NpcDataDialogues Dialogues;

    public TypeDialogue typeDialogue;

    public Sprite sprite;
    public Animator animator;
    public float Patience;


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
    }

    void Update()
    {
        
    }
    [Button]
    public void Initializer()
    {
        var DayManager = GameManager2D.instance.DayManager;

        int value = Random.Range(0, 10);


        if (DayManager.LookDay() > 5 && value > 9)
        {

            typeDialogue = TypeDialogue.Disturbing;




            Debug.Log(Dialogues.GetDialogue(typeDialogue));

            return;
        }
        if (DayManager.LookDay() > 3 && value >5)
        {

            typeDialogue = TypeDialogue.Strange;




            Debug.Log(Dialogues.GetDialogue(typeDialogue));

            return;
        }



        typeDialogue = TypeDialogue.Normal;



        Debug.Log(Dialogues.GetDialogue(typeDialogue));
        
    }
}
