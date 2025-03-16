using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

public class Graph : MonoBehaviour
{
    private class PriorityQueue<T>
    {
        private readonly List<T> _data;
        private readonly Comparison<T> _compare;

        public PriorityQueue()
            : this(Comparer<T>.Default)
        {
        }

        public PriorityQueue(IComparer<T> comparer)
            : this(comparer.Compare)
        {
        }

        public PriorityQueue(Comparison<T> comparison)
        {
            _data = new List<T>();
            _compare = comparison;
        }

        public void Enqueue(T item)
        {
            _data.Add(item);
            var childIndex = _data.Count - 1;

            while (childIndex > 0)
            {
                var parentIndex = (childIndex - 1) / 2;
                if (_compare(_data[childIndex],
                             _data[parentIndex])
                    >= 0)
                    break;

                T tmp = _data[childIndex];
                _data[childIndex] = _data[parentIndex];
                _data[parentIndex] = tmp;

                childIndex = parentIndex;
            }
        }

        public T Dequeue()
        {
            // assumes pq is not empty; up to calling code
            var lastElement = _data.Count - 1;

            var frontItem = _data[0];
            _data[0] = _data[lastElement];
            _data.RemoveAt(lastElement);

            --lastElement;

            var parentIndex = 0;
            while (true)
            {
                var childIndex = parentIndex * 2 + 1;
                if (childIndex > lastElement)
                    break; // End of tree

                var rightChild = childIndex + 1;
                if (rightChild <= lastElement
                    && _compare(_data[rightChild],
                                _data[childIndex])
                           < 0)
                    childIndex = rightChild;

                if (_compare(_data[parentIndex],
                             _data[childIndex])
                    <= 0)
                    break; // Correct position

                T tmp = _data[parentIndex];
                _data[parentIndex] = _data[childIndex];
                _data[childIndex] = tmp;

                parentIndex = childIndex;
            }

            return frontItem;
        }

        public T Peek()
        {
            T frontItem = _data[0];
            return frontItem;
        }

        public int Count
        {
            get { return _data.Count; }
        }
    }

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

        public IEnumerable<Pair<int, int>> adjList()
        {
            for (int i = 0; i < adyacents.Count; i++)
                yield return adyacents[i];
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
        graph.Add(new Node(new Pair<int, int>(53, -81), new List<Pair<int, int>> { new Pair<int, int>(11, 1), new Pair<int, int>(16, 1) }));
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

    // returns shortest path from s to d, dijkstra
    public int distance2time(int s, int d)
    {
        // Create a priority queue to store vertices that
        // are being preprocessed.
        var pq = new PriorityQueue<Tuple<int, int>>();

        // Create a vector for distances and initialize all
        // distances as infinite (INF)
        int V = graph.Count;
        var dist = new int[V];
        for (int i = 0; i < V; i++)
            dist[i] = int.MaxValue;

        // Insert source itself in priority queue and
        // initialize its distance as 0.
        pq.Enqueue(Tuple.Create(0, s));
        dist[s] = 0;

        /* Looping till priority queue becomes empty (or all
        distances are not finalized) */
        while (pq.Count != 0)
        {
            // The first vertex in pair is the minimum
            // distance vertex, extract it from priority
            // queue. vertex label is stored in second of
            // pair (it has to be done this way to keep the
            // vertices sorted distance (distance must be
            // first item in pair)
            var u = pq.Dequeue().Item2;

            // 'i' is used to get all adjacent vertices of a
            // vertex
            foreach (var i in graph[u].adjList())
            {
                // Get vertex label and weight of current
                // adjacent of u.
                int v = i.First;
                int weight = i.Second;

                //  If there is shorted path to v through u.
                if (dist[v] > dist[u] + weight)
                {
                    // Updating distance of v
                    dist[v] = dist[u] + weight;
                    pq.Enqueue(Tuple.Create(dist[v], v));
                }
            }
        }

        // Returns shortest distance from source to destination.
        return dist[d];
    }
}
