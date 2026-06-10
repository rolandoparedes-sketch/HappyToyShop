using HappyToyShop.Collections.Graphs;
using HappyToyShop.Collections.Trees;
using Sirenix.OdinInspector;
using System;
using Unity.VectorGraphics;
using UnityEngine;

public struct Dialog
{
    public string Dialogo;
    public string Opcion1;
    public string Opcion2;
    public Dialog(string value, string op1 = null, string op2 = null, Action action = null)
    {
        Dialogo = value;
        Opcion1 = op1;
        Opcion2 = op2;
    }
}
public class DialogueTreeExample : MonoBehaviour
{
    [Header("Dialogo Actual")]
    [ReadOnly]
    [ShowInInspector]
    private string currentDialogue = "Presiona 'Iniciar Dialogo'";

    [ReadOnly]
    [ShowInInspector]
    private string leftOption = "-";

    [ReadOnly]
    [ShowInInspector]
    private string rightOption = "-";

    [ReadOnly]
    [ShowInInspector]
    private bool isFinished;

    private BinaryTree<Dialog> tree;
    private BinaryTreeNode<Dialog> currentNode;

    //-> construir arbol e iniciar dialogo
    [Button("Iniciar Dialogo")]
    public void StartDialogue()
    {
        BuildSampleTree();
        currentNode = tree.Root;
        UpdateUI();
        isFinished = false;
        Debug.Log("--- Dialogo iniciado ---");
    }

    //-> elegir opcion izquierda (A)
    [Button("Opcion Izquierda")]
    public void ChooseLeft()
    {
        if (tree == null || currentNode == null || isFinished)
        {
            Debug.Log("Primero inicia el dialogo.");
            return;
        }
        if (currentNode.Left == null)
        {
            Debug.Log("No hay opcion izquierda.");
            return;
        }
        currentNode = currentNode.Left;
        UpdateUI();

        
        
    }

    //-> elegir opcion derecha (B)
    [Button("Opcion Derecha")]
    public void ChooseRight()
    {
        if (tree == null || currentNode == null || isFinished)
        {
            Debug.Log("Primero inicia el dialogo.");
            return;
        }
        if (currentNode.Right == null)
        {
            Debug.Log("No hay opcion derecha.");
            return;
        }
        currentNode = currentNode.Right;
        UpdateUI();
    }

    //-> reiniciar desde la raiz
    [Button("Reiniciar")]
    public void ResetDialogue()
    {
        if (tree == null || tree.IsEmpty)
        {
            BuildSampleTree();
        }
        currentNode = tree.Root;
        UpdateUI();
        isFinished = false;
        Debug.Log("--- Dialogo reiniciado ---");
    }

    private void UpdateUI()
    {
        if (currentNode == null)
        {
            currentDialogue = "(fin del dialogo)";
            leftOption = "-";
            rightOption = "-";
            isFinished = true;
            Debug.Log("--- Fin del dialogo ---");
            return;
        }

        currentDialogue = currentNode.Value.Dialogo;
        leftOption = currentNode.Value.Opcion1;
        rightOption =  currentNode.Value.Opcion2;

        Debug.Log($"Dialogo: {currentDialogue}");
    }

    //-> arbol de dialogo de ejemplo (7 nodos)
    //-> izquierda = opcion A, derecha = opcion B
    private void BuildSampleTree()
    {
        /*tree = new BinaryTree<string>();

        //            [Hola aventurero]
        //           /                  \
        //  [Como estas?]          [Quien eres?]
        //      /       \              /       \
        // [Bien!]  [Necesito ayuda] [Mago]  [Adios]

        var n00 = new BinaryTreeNode<string>("Hola aventurero!");
        var n10 = new BinaryTreeNode<string>("Como estas?");
        var n11 = new BinaryTreeNode<string>("Quien eres?");
        var n20 = new BinaryTreeNode<string>("Me alegra oirlo!");
        var n21 = new BinaryTreeNode<string>("Claro, dime que necesitas");
        var n22 = new BinaryTreeNode<string>("Soy el mago Merlin");
        var n23 = new BinaryTreeNode<string>("Hasta pronto, viajero");

        n00.Left = n10;
        n00.Right = n11;
        n10.Left = n20;
        n10.Right = n21;
        n11.Left = n22;
        n11.Right = n23;

        tree.SetRoot(n00);*/

        tree = new BinaryTree<Dialog>();

        //                                           Dialogues
        //                           [Se necesita vendedor en vieja jugueteria](A/B)                    0
        //                                     /                  \
        //      [Bien ahora trabajo en la jugueteria]      [No consegui chamba(UnemployedEnding)]       1
        //                               /                             
        //               [Que fue ese sonido de afuera?]       (A/B)                                    2
        //                          /           \
        //           [Sera mejor revisar] [Pero no le di Importancia(ActivarHorrorDayNocheFinal)]       3
        //                      /                                       \
        //   [Deberia llamar a la policia?]   (A/B)            [Por donde debo escapar](A/B)            4
        //           /             \                                 /                 \   
        //   [Normal Ending]  [Mistery Resolved Ending]        [Fugitive Ending]     [Bad Ending]       5





        var n00 = new BinaryTreeNode<Dialog>(new("Se necesita vendedor en vieja jugueteria","Aceptar Chamba","No me se la de chambear"));

        var n10 = new BinaryTreeNode<Dialog>(new("Bien ahora trabajas en la jugueteria"));

        var n11 = new BinaryTreeNode<Dialog>(new("No consegui chamba", null,null, () => EndingSystem.Instance.ActiveEnding(Endings.Unemployed)));

        var n20 = new BinaryTreeNode<Dialog>(new("Que fue ese sonido de afuera?", "Salir a revisar", "No darle importancia"));
        var n30 = new BinaryTreeNode<Dialog>(new("Sera mejor revisar"));
        var n31 = new BinaryTreeNode<Dialog>(new("Pero no le di Importancia(ActivarHorrorDayNocheFinal)"));
        var n40 = new BinaryTreeNode<Dialog>(new("Deberia llamar a la policia?","Si","No"));
        var n41 = new BinaryTreeNode<Dialog>(new("Por donde debo escapar", "Puerta Trasera", "Puerta Principal"));
        var n50 = new BinaryTreeNode<Dialog>(new("Espero que lleguen rápido",null, null, () => EndingSystem.Instance.ActiveEnding(Endings.Normal)));
        var n51 = new BinaryTreeNode<Dialog>(new("Nah, I´d Win", null, null, () => EndingSystem.Instance.ActiveEnding(Endings.MisteryResolved)));
        var n52 = new BinaryTreeNode<Dialog>(new("No volvere a este lugar jamas", null, null, () => EndingSystem.Instance.ActiveEnding(Endings.Fugitive)));
        var n53 = new BinaryTreeNode<Dialog>(new("Que demon-", null, null, () => EndingSystem.Instance.ActiveEnding(Endings.Bad)));






        n00.Left = n10;
        n00.Right = n11;
        n10.Left = n20;

        n20.Left = n30;
        n20.Right = n31;
        n30.Left = n40;
        n31.Left = n41;
        n40.Left = n50;
        n40.Right = n51;
        n41.Left = n52;
        n50.Left = n53;

        tree.SetRoot(n00);
    }
}