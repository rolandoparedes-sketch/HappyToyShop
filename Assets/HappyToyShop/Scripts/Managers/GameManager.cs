using HappyToyShop.Collections;
using HappyToyShop.Collections.Graphs;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [FoldoutGroup("References")]
    public static GameManager instance;
    [FoldoutGroup("References")]
    public ParanormalSuccess3D paranormalSuccess;
    [FoldoutGroup("References")]
    public Transform Player;
    [FoldoutGroup("References")]
    public MusicDatabase musicDatabase;
    [FoldoutGroup("References")]
    public SoundManager soundManager;
    [FoldoutGroup("GameSettings")]
    public MyQueue<int> Day = new();
    [FoldoutGroup("GameSettings")]
    public int NumbersOfDays = 7;
  
    public Action OnNextDay;

    public Action OnWeekComplete;
    [FoldoutGroup("GameSettings")]
    public bool TurnDay = true;
    [FoldoutGroup("GameSettings")]

    //List

    public UnorientedGraph<string> graph = new ();

    //Matrix

    [Button]
    public void AddNode(string Vertex)
    {
       Node<string> a = graph.AddNode(Vertex);
    }

    [Button]
    public void RemoveNode(int VertexPos)
    {

       graph.RemoveNode(VertexPos);
       
    }


    [Button]
    public void AddEdges(int VertexPos, int VertexPos2)
    {
        graph.AddEdges(VertexPos, VertexPos2);
    }


    [Button]
    public void DeleteEdges(int VertexPos, int VertexPos2)
    {
        graph.DeleteEdges(VertexPos, VertexPos2);
    }
    [Button]
    public void PrintAdjacencyList()
    {
        graph.PrintAdjancencyList();
    }
    [Button]
    public void PrintAdjacencyMatrix()
    {
        graph.PrintAdjacencyMatrix();
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
       


    }


    void Update()
    {

    }
}
