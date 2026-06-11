using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NpcDataDialogues", menuName = "Happy Toy Shop/NpcDataDialogues")]
public class NpcDataDialogues : SerializedScriptableObject
{

    public Dictionary<TypeDialogue, List<string>> typeDialogue = new();

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

}
