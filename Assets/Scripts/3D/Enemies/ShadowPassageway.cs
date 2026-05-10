using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;
using static UnityEditor.Rendering.ShadowCascadeGUI;

public class ShadowPassageway : MonoBehaviour
{
    
    [FoldoutGroup("References")]
    public Transform InitialPoint;

    [FoldoutGroup("References")]
    public Transform FinalPoint;

    [FoldoutGroup("Settings")]
    public float moveSpeed = 2f;
    [FoldoutGroup("Settings/Effects")]
    public float fearIncrease = 15f;
    public bool CanScare = true;
    private Transform currentTarget;
    private bool startFromA = true;

    void Start()
    {
    }
    private void OnEnable()
    {
        CanScare = true;
        if (startFromA)
        {
            transform.position = InitialPoint.position;
            currentTarget = FinalPoint;
        }
        else
        {
            transform.position = FinalPoint.position;
            currentTarget = InitialPoint;
        }




    }


    void Update()
    {
        if (InitialPoint != null && FinalPoint != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, moveSpeed * Time.deltaTime);

            transform.LookAt(currentTarget);
        }

        if (Vector3.Distance(transform.position, currentTarget.position) < 0.05f)
        {
            transform.position = currentTarget.position;
            gameObject.SetActive(false);
        }
    }
    public void ShadowDetected()
    {
        Debug.Log(GameManager.instance.paranormalSuccess.Player);
        FirstPersonController player = GameManager.instance.paranormalSuccess.Player.GetComponent<FirstPersonController>();
        if (CanScare)
        {
            player.currentCordure = Mathf.Max(player.currentCordure - fearIncrease, 0);
            CanScare = false;
           
        }
        player.UpdateFearState();

    }
}
