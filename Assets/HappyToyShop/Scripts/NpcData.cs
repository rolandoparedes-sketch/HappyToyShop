using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
//using UnityEditor.Animations;
[CreateAssetMenu(fileName = "NpcData", menuName = "Happy Toy Shop/NpcData")]
[InlineEditor]
public class NpcData : BaseEntityData
{
    //[SerializeField]private AnimatorController anim;
    [SerializeField] private RuntimeAnimatorController anim;


    public RuntimeAnimatorController Anim => anim;
    //public AnimatorController Anim => anim;
}
