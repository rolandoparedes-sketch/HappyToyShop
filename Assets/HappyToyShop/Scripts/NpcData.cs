using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NpcDataDialogues", menuName = "Happy Toy Shop/NpcDataDialogues")]
public class NpcData : SerializedScriptableObject
{

    public Dictionary<TypeDialogue, List<string>> typeDialogue = new();

    public Dictionary<int, Sprite> NpcIcon = new();
    public string GetDialogue(TypeDialogue type)
    {
        if (typeDialogue.TryGetValue(type, out List<string> entities))
        {
            int n = Random.Range(0, entities.Count);
            return entities[n];
        }
        else
        {
            throw new System.Exception("El tipo de dialogo no ha sido asignado aun o no tiene dialogos disponibles");
        }
    }
    public Sprite GetSpriteNpc(int npc)
    {
        if (NpcIcon.TryGetValue(npc, out Sprite entity))
        {
            int n = Random.Range(0, NpcIcon.Count);
            return entity;
        }
        else
        {
            throw new System.Exception("El Id del Npc no ha sido asignado aun o no tiene Sprites disponibles");
        }
    }

}
