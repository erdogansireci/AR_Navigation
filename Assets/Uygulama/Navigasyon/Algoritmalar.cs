using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algoritmalar : MonoBehaviour
{
    float[,] graph;
    int NodeCount;
    public List<int> shortestPath = new List<int>();

    public struct node
    {
        public bool permanent;
        public float distFromSrc;
        public int parent;

    }

    //example
    //void Start()
    //{
    //    graph = new int[5, 5]
    //    {

    //     { 0, 3, 5, 0, 0},
    //     { 3, 0, 2, 3, 0},
    //     { 5, 2, 0, 6, 0},
    //     { 0, 9, 6, 0, 2},
    //     { 0, 0, 0, 2, 0}

    //   };

    //    Dijkstra(graph, 0, 3);

    //}

    //src gpsden gelen konum olacak. 
    public void Dijkstra(float[,] graph, int dest)
    {
        //Başlangıç nodesi her zaman 1 yapıldı
        int src = 0;

        NodeCount = graph.GetLength(0);
        Debug.Log("Node Count: " + NodeCount);

        node[] nodeSet = new node[NodeCount];
        for (int i = 0; i < NodeCount; i++)
        {
            nodeSet[i].distFromSrc = int.MaxValue;
            nodeSet[i].permanent = false;
        }

        //kaynak düğümden başlaması için kaynak düğümü 0 yapıyoruz.
        nodeSet[src].distFromSrc = 0;
        nodeSet[src].parent = -1;
        Debug.Log("NodeSet: " + nodeSet);

        for (int count = 0; count < NodeCount - 1; count++)
        {
            Debug.Log("minDistance giriliyor.");
            int u = minDistance(nodeSet);
            Debug.Log("minDistance tamamlandı.");

            nodeSet[u].permanent = true;

            if (u == dest)
            {
                break;
            }

            int i= 0;
            for (int v = 0; v < NodeCount; v++)
            {
                Debug.Log("V'li for girildi:" + (++i));
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
        Debug.Log("shortestpath başarili.");

        foreach (int Node in shortestPath)
        {

            Debug.Log("Path: " + Node);
        }
    }

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

    int j = 0;
    private void ShortestPath(node[] nodeSet, int dest)
    {
        Debug.Log("NodeSet:" + nodeSet +"\nlength "+ nodeSet.Length + "\ndest: "+ dest);
        Debug.Log("İşte burda sıkıntı olabilir moruk: " + (++j));
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
