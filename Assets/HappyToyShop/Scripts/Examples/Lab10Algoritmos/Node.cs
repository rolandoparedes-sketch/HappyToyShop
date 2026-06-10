using System.Collections.Generic;
using UnityEngine;

namespace HappyToyShop.Collections.Graphs
{
    public class Node<T>
    {
        #region Properties/Privates
        private T value = default;
        private List<Node<T>> neighbors = new();


        #endregion


        #region  Methods
        public Node(T value)
        {
            this.value = value; 
        }

        public void ConnectUnidirectional(Node<T> node) //Unidireccional
        {
            if (!neighbors.Contains(node))
                neighbors.Add(node);

        }
        public void ConnectBidireccional(Node<T> node) //Bidireccional
        {
            if(!neighbors.Contains(node))
                neighbors.Add(node);

            node.neighbors.Add(this);
        }
        public void DisconnectUnidirectional(Node<T> node) //Unidireccional
        {
            if (neighbors.Contains(node))
                neighbors.Remove(node);

        }
        public void DisconnectBidireccional(Node<T> node) //Bidireccional
        {
            if (neighbors.Contains(node))
                neighbors.Remove(node);

            node.neighbors.Remove(this);
        }
        #endregion

        #region Getters
        public T Value => value;
        public List<Node<T>> Neighbors => neighbors;

        #endregion
    }
}
