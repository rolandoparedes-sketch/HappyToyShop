using HappyToyShop.Collections.Graphs;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

namespace HappyToyShop.Collections.Graphs
{
    public class UnorientedGraph<T>
    {
        private List<Node<T>> nodes = new();

        //-> añadir un nodo
        public Node<T> AddNode(T value)
        {
            Node<T> newNode = new Node<T>(value);
            nodes.Add(newNode);

            return newNode;
        }
        //-> remover un nodo
        /*public void RemoveNode(Node<T> node)
        {

            nodes.Remove(node);

            foreach (var other in nodes)
            {
                other.Disconnect(node);
            }
        }*/
        public void RemoveNode(int pos)
        {
            if(pos < 0 || pos > nodes.Count)
            {

                throw new System.InvalidOperationException("The vertex selected for removal is outside the valid range");


            }

            Node<T> nodeToRemove = nodes[pos]; 

            foreach (var other in nodes)
            {
                other.Disconnect(nodeToRemove);
            }

            nodes.RemoveAt(pos);

        }
        /* public void AddEdges(Node<T> a, Node<T> b)
         {
             a.Connect(b);
         }*/
        public void AddEdges(int posA, int posB)
        {
            if ((posA < 0 || posA > nodes.Count) || (posB < 0 || posB > nodes.Count))
            {

                throw new System.InvalidOperationException("One or more of the vertices selected for connection are outside the valid range");


            }
            nodes[posA].Connect(nodes[posB]);
        }
        /*public void DeleteEdges(Node<T> a, Node<T> b)
        {
            a.Disconnect(b);
        }*/
        public void DeleteEdges(int posA, int posB)
        {

            if ((posA < 0 || posA > nodes.Count) || (posB < 0 || posB > nodes.Count))
            {

                throw new System.InvalidOperationException("One or more of the vertices selected for disconnection are outside the valid range");
            }
                nodes[posA].Disconnect(nodes[posB]);
        }
        public void PrintAdjancencyList()//-> cuadratica
        {
            if (nodes.Count <= 0)
            {
                throw new System.InvalidOperationException("List is empty");
            }

            int pos = -1;
            for (var i = 0; i < nodes.Count; i++)
            {

                pos++;
                //poner posicion
                string nodeList = "Node Pos "+ pos + ": " + nodes[i].Value.ToString() + " => ";

                //-> i => posoicion
                for (var j = 0; j < nodes[i].Neighbors.Count; j++)
                {
                    //-> j cada vecino de i

                    nodeList += nodes[i].Neighbors[j].Value.ToString() + ", ";
                }

                Debug.Log(nodeList);

            }
        }

        public void PrintAdjacencyMatrix()
        {
            if (nodes.Count <= 0)
            {
                throw new System.InvalidOperationException("List is empty");
            }
            Debug.Log("Matriz de adyacencia");
            string context = "         ";

            for (var i = 0; i < nodes.Count; i++)
            {
                context += i + "    ";
            }
            context += "\n";
            for (var i = 0; i < nodes.Count; i++)
            {
                context += i + "    ";//\n
                for (var j = 0; j < nodes.Count; j++)
                {
                    context += nodes[i].Neighbors.Contains(nodes[j]) ? "  1  " : "  0  ";
                }
                context += "\n";
            }
            Debug.Log(context);
        }




        // añadir conexion
        // remover una conexion
    }
}