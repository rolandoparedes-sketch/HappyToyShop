using Sirenix.OdinInspector;
using Unity.VectorGraphics;
using UnityEngine;

public class EndingSystem : MonoBehaviour
{
    public static EndingSystem Instance;
    public Endings endings;
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {

        endings = Endings.None;
    }
    
    void Update()
    {
        
    }

    public void ActiveEnding(Endings endtype)
    {
        Debug.Log("Activo");
        endings = endtype;

        switch (endtype)
        { 
            case Endings.None: 
                Debug.Log("NingunFinalEscogido");
                break;
            case Endings.Unemployed:
                Debug.Log("Unemployed Ending");
                break;
            case Endings.Normal:
                Debug.Log("Norma Ending");
                break;
            case Endings.MisteryResolved:
                Debug.Log("Mistery Resolved Ending");
                break;
            case Endings.Bankrupt:
                Debug.Log("Bankrupt Ending");
                break;
            case Endings.Fugitive:
                Debug.Log("Fugitive Ending");
                break;
            case Endings.Bad:
                Debug.Log("Bad Ending");
                break;
            case Endings.Secret:
                Debug.Log("Secret Ending");
                break;
            default:
                Debug.Log("Default");
                break;
        }
    }

}
