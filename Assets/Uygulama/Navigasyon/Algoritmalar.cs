using System.Collections.Generic;
using UnityEngine;

public class Algoritmalar : MonoBehaviour
{
    int NodeCount;
    public List<int> shortestPath = new List<int>();

    public struct node
    {
        public bool permanent;
        public float distFromSrc;
        public int parent;

    }

    //Dış mekanda kullanılacak en kısa yol algoritması
    public void Dijkstra(float[,] graph, int dest)
    {
        //Şimdilik başlangıç nodesi her zaman 1 yapıldı
        int src = 0;

        NodeCount = graph.GetLength(0);

        node[] nodeSet = new node[NodeCount];
        for (int i = 0; i < NodeCount; i++)
        {
            nodeSet[i].distFromSrc = int.MaxValue;
            nodeSet[i].permanent = false;
        }

        //Kaynak düğümden başlaması için kaynak düğümü 0 yap
        nodeSet[src].distFromSrc = 0;
        nodeSet[src].parent = -1;

        for (int count = 0; count < NodeCount - 1; count++)
        {
            int u = minDistance(nodeSet);

            nodeSet[u].permanent = true;

            if (u == dest)
            {
                break;
            }

            for (int v = 0; v < NodeCount; v++)
            {
                if (!nodeSet[v].permanent &&
                    graph[u, v] != 0 &&
                    nodeSet[u].distFromSrc != int.MaxValue &&
                    nodeSet[u].distFromSrc + graph[u, v] <= nodeSet[v].distFromSrc)
                {

                    nodeSet[v].distFromSrc = nodeSet[u].distFromSrc + graph[u, v];
                    nodeSet[v].parent = u;
                }

            }
        }

        ShortestPath(nodeSet, dest);
        shortestPath.Reverse();

        foreach (int Node in shortestPath)
        {

            Debug.Log("Path: " + Node);
        }
    }

    //İç mekanda kullanılacak en kısa yol algoritması
    public void IcMekan_Dijkstra(float[,] graph, int src, int dest)
    {

        NodeCount = graph.GetLength(0);

        node[] nodeSet = new node[NodeCount];
        for (int i = 0; i < NodeCount; i++)
        {
            nodeSet[i].distFromSrc = int.MaxValue;
            nodeSet[i].permanent = false;
        }

        //Kaynak düğümden başlaması için kaynak düğümü 0 yapıyoruz.
        nodeSet[src].distFromSrc = 0;
        nodeSet[src].parent = -1;

        for (int count = 0; count < NodeCount - 1; count++)
        {
            int u = minDistance(nodeSet);

            nodeSet[u].permanent = true;

            if (u == dest)
            {
                break;
            }

            for (int v = 0; v < NodeCount; v++)
            {
                if (!nodeSet[v].permanent &&
                    graph[u, v] != 0 &&
                    nodeSet[u].distFromSrc != int.MaxValue &&
                    nodeSet[u].distFromSrc + graph[u, v] <= nodeSet[v].distFromSrc)
                {

                    nodeSet[v].distFromSrc = nodeSet[u].distFromSrc + graph[u, v];
                    nodeSet[v].parent = u;
                }
            }
        }

        ShortestPath(nodeSet, dest);
        shortestPath.Reverse();

        foreach (int Node in shortestPath)
        {

            Debug.Log("Path: " + Node);
        }
    }

    //Nodeler arasından en kısa mesafe olanın indexini döner.
    private int minDistance(node[] nodeSet)
    {

        float min = int.MaxValue;
        int min_index = -1;

        for (int i = 0; i < NodeCount; i++)
        {

            if (nodeSet[i].permanent == false && nodeSet[i].distFromSrc <= min)
            {
                min = nodeSet[i].distFromSrc;
                min_index = i;
            }
        }

        return min_index;
    }

    //Rota üzerindeki nodeleri sırayla listeye yazar.
    private void ShortestPath(node[] nodeSet, int dest)
    {
        if (nodeSet[dest].parent != -1)
        {
            shortestPath.Add(dest);
            ShortestPath(nodeSet, nodeSet[dest].parent);
        }

        else
        {
            shortestPath.Add(dest);
            return;
        }
    }
}
