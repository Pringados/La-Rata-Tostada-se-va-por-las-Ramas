using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Graph : MonoBehaviour
{
    private class Pair<T1, T2>
    {
        public T1 First { get; set; }
        public T2 Second { get; set; }
        public Pair(T1 first, T2 second)
        {
            First = first;
            Second = second;
        }
    }

    private class Node
    {
        public Pair<int, int> position;
        public List<Pair<int, int>> adyacents;
        public Node(Pair<int, int> pos, List<Pair<int, int>> ady)
        {
            position = pos;
            adyacents = ady;
        }
    }

    private List<Node> graph;


// Start is called before the first frame update
    void Awake()
    {
        graph = new List<Node>();
        PopulateGraph();
        int i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopulateGraph() {

        graph.Add(new Node(new Pair<int, int>(-3, 175), new List<Pair<int, int>> { new Pair<int, int>(3, 1) }));
        graph.Add(new Node(new Pair<int, int>(47, 152), new List<Pair<int, int>> { new Pair<int, int>(3, 2), new Pair<int, int>(4, 3) }));
        graph.Add(new Node(new Pair<int, int>(-70, 137), new List<Pair<int, int>> { new Pair<int, int>(3, 3), new Pair<int, int>(6, 4) }));
        graph.Add(new Node(new Pair<int, int>(-1, 109), new List<Pair<int, int>> { new Pair<int, int>(0, 1), new Pair<int, int>(1, 2), new Pair<int, int>(2, 3), new Pair<int, int>(6, 2), new Pair<int, int>(7, 3) }));
        graph.Add(new Node(new Pair<int, int>(94, 86), new List<Pair<int, int>> { new Pair<int, int>(1, 3), new Pair<int, int>(7, 1) }));
        graph.Add(new Node(new Pair<int, int>(-95, 61), new List<Pair<int, int>> { new Pair<int, int>(6, 3) }));
        graph.Add(new Node(new Pair<int, int>(-26, 45), new List<Pair<int, int>> { new Pair<int, int>(2, 4), new Pair<int, int>(3, 2), new Pair<int, int>(5, 3), new Pair<int, int>(8, 3), new Pair<int, int>(10, 1) }));
        graph.Add(new Node(new Pair<int, int>(40, 45), new List<Pair<int, int>> { new Pair<int, int>(3, 3), new Pair<int, int>(4, 1), new Pair<int, int>(9, 1), new Pair<int, int>(10, 1) }));
        graph.Add(new Node(new Pair<int, int>(-80, 11), new List<Pair<int, int>> { new Pair<int, int>(6, 3) }));
        graph.Add(new Node(new Pair<int, int>(72, 8), new List<Pair<int, int>> { new Pair<int, int>(7, 1) }));
        graph.Add(new Node(new Pair<int, int>(-16, -8), new List<Pair<int, int>> { new Pair<int, int>(6, 1), new Pair<int, int>(7, 1), new Pair<int, int>(11, 1) }));
        graph.Add(new Node(new Pair<int, int>(-4, -72), new List<Pair<int, int>> { new Pair<int, int>(10, 1), new Pair<int, int>(12, 1), new Pair<int, int>(13, 1), new Pair<int, int>(14, 1), new Pair<int, int>(15, 2) }));
        graph.Add(new Node(new Pair<int, int>(-54, -78), new List<Pair<int, int>> { new Pair<int, int>(11, 1), new Pair<int, int>(15, 1) }));
        graph.Add(new Node(new Pair<int, int>(53, 81), new List<Pair<int, int>> { new Pair<int, int>(11, 1), new Pair<int, int>(16, 1) }));
        graph.Add(new Node(new Pair<int, int>(6, -116), new List<Pair<int, int>> { new Pair<int, int>(11, 1), new Pair<int, int>(16, 2), new Pair<int, int>(17, 1) }));
        graph.Add(new Node(new Pair<int, int>(-43, -131), new List<Pair<int, int>> { new Pair<int, int>(11, 2), new Pair<int, int>(12, 1) }));
        graph.Add(new Node(new Pair<int, int>(63, -137), new List<Pair<int, int>> { new Pair<int, int>(13, 1), new Pair<int, int>(14, 2) }));
        graph.Add(new Node(new Pair<int, int>(12, -172), new List<Pair<int, int>> { new Pair<int, int>(14, 1) }));
    }

    public int getXNode(int n) {
        return graph[n].position.First;
    }

    public int getYNode(int n)
    {
        return graph[n].position.Second;
    }
}
