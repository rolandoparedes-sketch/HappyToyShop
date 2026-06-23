using System;
using UnityEngine;

public class FurnitureManager : MonoBehaviour
{
    [SerializeField] private PackingTable packingTable;
    [SerializeField] private AttentionTable attentionTable;

    private void Awake()
    {
        GameManager2D.instance.SetFurnitureManager(this);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public PackingTable PackingTable => packingTable;

    public AttentionTable AttentionTable => attentionTable; 
}
